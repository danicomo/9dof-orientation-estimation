%Connect to the INEMO device
[handle_dev pFD]=INEMO_Connection();
%End connection
acqSize=200;

Offset=[-3.6982,-3.3570,-2.5909]';
%Acquisition variables
GyroRate=zeros(3,acqSize);

i=1;
k=1;
n=10;
stdDev=zeros(6,n);

while(k<=n)
    while(i<acqSize)
        [errre pFD]=calllib('iNEMO2_SDK','INEMO2_GetDataSample',handle_dev,pFD);
        %----------
        pause(0.01)
        %---------
        GyroRate(1,i)=(pFD.Gyroscope.X-Offset(1,1));
        GyroRate(2,i)=(pFD.Gyroscope.Y-Offset(2,1));
        GyroRate(3,i)=(pFD.Gyroscope.Z-Offset(3,1));
        i=i+1;
    end
    INEMO_Disconnection(handle_dev);
    pause(7);
    stdDev(1,k)=mean(GyroRate(1,:));
    stdDev(2,k)=std(GyroRate(1,:));
    
    stdDev(3,k)=mean(GyroRate(2,:));
    stdDev(4,k)=std(GyroRate(2,:));
    
    stdDev(5,k)=mean(GyroRate(3,:));
    stdDev(6,k)=std(GyroRate(3,:));
    [handle_dev pFD]=INEMO_Connection();
    i=1;
    k=k+1;
    
end

stdResult(1,1)=mean(stdDev(1,:));
stdResult(2,1)=mean(stdDev(2,:));
stdResult(3,1)=mean(stdDev(3,:));
stdResult(4,1)=mean(stdDev(4,:));
stdResult(5,1)=mean(stdDev(5,:));
stdResult(6,1)=mean(stdDev(6,:));
INEMO_Disconnection(handle_dev);