clear all;
close all;
%Connect to the INEMO device
[handle_dev pFD]=INEMO_Connection();
%End connection
acqSize=500;

%Gyroscope statistics
Offset=[-3.6982,-3.3570,-2.5909]';

%Acquisition variables
GyroRate=zeros(3,acqSize);
Acc=zeros(3,acqSize);
Magn=zeros(3,acqSize);
AnglesGrad=zeros(3,acqSize);
AnglesGN=zeros(3,acqSize);
AccF=zeros(3,acqSize);
MagnF=zeros(3,acqSize);
mu=zeros(1,acqSize);
dqnorm=zeros(1,acqSize);
dq=zeros(4,acqSize);

%Observation vector - Gradient Descent
qOsservGrad=zeros(4,acqSize);
qOsservGrad(:,1)=[1 0 0 0]';
%Observation vector - Gauss Newton
qOsservGN=zeros(4,acqSize);
qOsservGN(:,1)=[1 0 0 0]';

t=[0];

i=1;
dt=0;

[bAcc,aAcc] = butter(3,0.0075,'low');
[bMagn,aMagn] = butter(2,0.06,'low');

magnF_Length=13;
accF_Length=13;

%Bring up the filters
while(i<=accF_Length+4)
    if(i>1)
        dt = toc(t0);
        t=[t t(length(t))+dt];
    end
    
    [errre pFD]=calllib('iNEMO2_SDK','INEMO2_GetDataSample',handle_dev,pFD);
    t0 = tic;

        %----------
        pause(0.01)
        %---------
        
    Acc(1,i)=pFD.Accelerometer.X;
    Acc(2,i)=pFD.Accelerometer.Y;
    Acc(3,i)=pFD.Accelerometer.Z;
    Magn(1,i)=pFD.Magnetometer.X;
    Magn(2,i)=pFD.Magnetometer.Y;
    Magn(3,i)=pFD.Magnetometer.Z;
    GyroRate(1,i)=((pFD.Gyroscope.X-Offset(1,1))/180)*pi;
    GyroRate(2,i)=((pFD.Gyroscope.Y-Offset(2,1))/180)*pi;
    GyroRate(3,i)=((pFD.Gyroscope.Z-Offset(3,1))/180)*pi;
    
    Acc(:,i)=Acc(:,i)/norm(Acc(:,i));
    Magn(:,i)=Magn(:,i)/norm(Magn(:,i));
    if(i<=accF_Length)
        AccF(:,i)=MyFilter(bAcc,aAcc,Acc(:,:));
    else
        AccF(:,i)=MyFilter(bAcc,aAcc,Acc(:,i-accF_Length:i));
    end
    if(i<=magnF_Length)
        MagnF(:,i)=MyFilter(bMagn,aMagn,Magn(:,:));
    else
        MagnF(:,i)=MyFilter(bMagn,aMagn,Magn(:,i-magnF_Length:i));
    end
    MagnF(:,i)=MagnF(:,i)/norm(MagnF(:,i));
    AccF(:,i)=AccF(:,i)/norm(AccF(:,i));
    i=i+1;
    qOsservGrad(:,i)=qOsservGrad(:,i-1);
    qOsservGN(:,i)=qOsservGN(:,i-1);
end

while(i<=acqSize)
    if(i>2)
        dt = toc(t0);
        t=[t t(length(t))+dt];
    end
    %----Acquisition
        [errre pFD]=calllib('iNEMO2_SDK','INEMO2_GetDataSample',handle_dev,pFD);
        t0 = tic;

        %----------
        pause(0.01)
        %---------
        
    Acc(1,i)=pFD.Accelerometer.X;
    Acc(2,i)=pFD.Accelerometer.Y;
    Acc(3,i)=pFD.Accelerometer.Z;
    Magn(1,i)=pFD.Magnetometer.X;
    Magn(2,i)=pFD.Magnetometer.Y;
    Magn(3,i)=pFD.Magnetometer.Z;
    GyroRate(1,i)=((pFD.Gyroscope.X-Offset(1,1))/180)*pi;
    GyroRate(2,i)=((-pFD.Gyroscope.Y+Offset(2,1))/180)*pi;
    GyroRate(3,i)=((pFD.Gyroscope.Z-Offset(3,1))/180)*pi;
    
    GyroRate(1,i)=(GyroRate(1,i)+GyroRate(1,i-1))/2;
    GyroRate(2,i)=(GyroRate(2,i)+GyroRate(2,i-1))/2;
    GyroRate(3,i)=(GyroRate(3,i)+GyroRate(3,i-1))/2;
    
    %Normalization and filtering
    Acc(:,i)=Acc(:,i)/norm(Acc(:,i));
    Magn(:,i)=Magn(:,i)/norm(Magn(:,i));
    
    AccF(:,i)=MyFilter(bAcc,aAcc,Acc(:,i-accF_Length:i));
    MagnF(:,i)=MyFilter(bMagn,aMagn,Magn(:,i-magnF_Length:i));
    
    MagnF(:,i)=MagnF(:,i)/norm(MagnF(:,i));
    AccF(:,i)=AccF(:,i)/norm(AccF(:,i));
    %----End Acquisition
    
    %OBSERVATION COMPUTING
    %Gauss Newton step 
    qOsservGN(:,i)=GaussNewtonMethod(qOsservGN(:,i-1),AccF(:,i),MagnF(:,i));
    qOsservGN(:,i)=qOsservGN(:,i)/norm(qOsservGN(:,i));
    
    %Gradient Descent Step
    dq(:,i)=0.5*(QuaternionProduct(qOsservGrad(:,i-1),[0 GyroRate(1,i) GyroRate(2,i) GyroRate(3,i)]'));
    dqnorm(1,i)=norm(dq(:,i));
    mu(1,i)=10*dqnorm(1,i)*dt;
    qOsservGrad(:,i)=GradientDescent(AccF(:,i),MagnF(:,i),qOsservGrad(:,i-1),mu(1,i));
    qOsservGrad(:,i)=qOsservGrad(:,i)/norm(qOsservGrad(:,i));
    %Angles Computation
    AnglesGrad(:,i)=GetAnglesFromQuaternion(qOsservGrad(:,i));
    AnglesGN(:,i)=GetAnglesFromQuaternion(qOsservGN(:,i));   
    %END OSSERVATION COMPUTING
    i=i+1;
end

INEMO_Disconnection(handle_dev);


figure;
    subplot(4,1,1);plot(t,qOsservGrad(1,1:acqSize),'b',t,qOsservGN(1,:),'r');grid;legend('q0 Gradient Descent','q0 Gauss Newton');
    subplot(4,1,2);plot(t,qOsservGrad(2,1:acqSize),'b',t,qOsservGN(2,:),'r');grid;legend('q1 Gradient Descent','q1 Gauss Newton');
    subplot(4,1,3);plot(t,qOsservGrad(3,1:acqSize),'b',t,qOsservGN(3,:),'r');grid;legend('q2 Gradient Descent','q2 Gauss Newton');
    subplot(4,1,4);plot(t,qOsservGrad(4,1:acqSize),'b',t,qOsservGN(4,:),'r');grid;legend('q3 Gradient Descent','q3 Gauss Newton');
    
figure;
    subplot(3,1,1);plot(t,AnglesGrad(1,1:acqSize),'b',t,AnglesGN(1,:),'r');grid;xlabel('time (sec)');ylabel('Angle (deg)');
    subplot(3,1,2);plot(t,AnglesGrad(2,1:acqSize),'b',t,AnglesGN(2,:),'r');grid;xlabel('time (sec)');ylabel('Angle (deg)');
    subplot(3,1,3);plot(t,AnglesGrad(3,1:acqSize),'b',t,AnglesGN(3,:),'r');grid;xlabel('time (sec)');ylabel('Angle (deg)');
    



