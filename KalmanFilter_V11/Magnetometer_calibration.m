clear all;
close all;
%Connect to the INEMO device
[handle_dev pFD]=INEMO_Connection();
%End connection
acqSize=500;

m_x=zeros(1,acqSize);
m_y=zeros(1,acqSize);
m_z=zeros(1,acqSize);

i=1;
while (i<acqSize)
    [errre pFD]=calllib('iNEMO2_SDK','INEMO2_GetDataSample',handle_dev,pFD);
    
    norma=norm([pFD.Magnetometer.X pFD.Magnetometer.Y pFD.Magnetometer.Z]);
    m_x(1,i)=pFD.Magnetometer.X/norma;
    m_y(1,i)=pFD.Magnetometer.Y/norma;
    pause(0.01);
    i=i+1;
end

figure;
plot(m_x,m_y,'-o');

if ((max(m_y)-min(m_y))/(max(m_x)-min(m_x))>1)
    Xsf1=(max(m_y)-min(m_y))/(max(m_x)-min(m_x));
else
    Xsf1=1;
end

if ((max(m_x)-min(m_x))/(max(m_y)-min(m_y))>1)
    Ysf1=(max(m_x)-min(m_x))/(max(m_y)-min(m_y));
else
    Ysf1=1;
end

Xoff1 = ((max(m_x) - min(m_x))/2  - max(m_x)) * Xsf1;         
Yoff1 = ((max(m_y) - min(m_y))/2  - max(m_y)) * Ysf1;

m_x1=Xsf1*m_x+Xoff1;
m_y1=Ysf1*m_y+Yoff1;

figure;
plot(m_x1,m_y1,'-o');

INEMO_Disconnection(handle_dev);
pause(5);
[handle_dev pFD]=INEMO_Connection();

%rotazione 360 con asse x parallelo a G
i=1;
while (i<acqSize)
    [errre pFD]=calllib('iNEMO2_SDK','INEMO2_GetDataSample',handle_dev,pFD);
    
    norma=norm([pFD.Magnetometer.X pFD.Magnetometer.Y pFD.Magnetometer.Z]);
    m_z(1,i)=pFD.Magnetometer.Z/norma;
    m_y(1,i)=pFD.Magnetometer.Y/norma;
    pause(0.01);
    i=i+1;
end

figure;
plot(m_z,m_y,'-o');

if ((max(m_y)-min(m_y))/(max(m_z)-min(m_z))>1)
    Zsf2=(max(m_y)-min(m_y))/(max(m_z)-min(m_z));
else
    Zsf2=1;
end

if ((max(m_z)-min(m_z))/(max(m_y)-min(m_y))>1)
    Ysf2=(max(m_z)-min(m_z))/(max(m_y)-min(m_y));
else
    Ysf2=1;
end

Zoff2 = ((max(m_z) - min(m_z))/2  - max(m_z)) * Zsf2;         
Yoff2 = ((max(m_y) - min(m_y))/2  - max(m_y)) * Ysf1;

m_z2=Zsf2*m_z+Zoff2;
m_y2=Ysf2*m_y+Yoff2;

figure;
plot(m_z2,m_y2,'-o');

INEMO_Disconnection(handle_dev);
pause(5);
[handle_dev pFD]=INEMO_Connection();

%rotazione 360 con asse y parallelo a G
i=1;
while (i<acqSize)
    [errre pFD]=calllib('iNEMO2_SDK','INEMO2_GetDataSample',handle_dev,pFD);
    
    norma=norm([pFD.Magnetometer.X pFD.Magnetometer.Y pFD.Magnetometer.Z]);
    m_z(1,i)=pFD.Magnetometer.Z/norma;
    m_x(1,i)=pFD.Magnetometer.X/norma;
    pause(0.01);
    i=i+1;
end

figure;
plot(m_z,m_x,'-o');

if ((max(m_x)-min(m_x))/(max(m_z)-min(m_z))>1)
    Zsf3=(max(m_x)-min(m_x))/(max(m_z)-min(m_z));
else
    Zsf3=1;
end

if ((max(m_z)-min(m_z))/(max(m_x)-min(m_x))>1)
    Xsf3=(max(m_z)-min(m_z))/(max(m_x)-min(m_x));
else
    Xsf3=1;
end

Zoff3 = ((max(m_z) - min(m_z))/2  - max(m_z)) * Zsf3;         
Xoff3 = ((max(m_x) - min(m_x))/2  - max(m_x)) * Xsf3;

m_z3=Zsf3*m_z+Zoff3;
m_x3=Xsf3*m_x+Xoff3;

figure;
plot(m_z3,m_x3,'-o');



INEMO_Disconnection(handle_dev);