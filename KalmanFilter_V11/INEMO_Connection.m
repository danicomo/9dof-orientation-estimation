function [handle_dev pFD] = INEMO_Connection()
%returned values:
% handle_dev: the device number;
% pFD: data structure containing data acquired from the iNemo

%import libraries
[notfound warnings] = loadlibrary('iNEMO2_SDK.dll', 'iNEMO2_SDK.h');

%connect to the iNemo2 - Pay attention to modify the COM number according
%to the correct one
sCOM='PL=PL_001{PN=COM5,SENDMODE=B}';
[handle_dev]=calllib('iNEMO2_SDK','INEMO2_Connect',sCOM);

%SET OUTPUT
%mode 0 (default)
%data 31 (all data)
%freq 50 (50 samples per second)
%type 0 (usb)
%samples 0 (continuos)
outmode = struct('Mode',0,'Data',31,'Frequency',50,'Type',0,'Samples', 0);
[error1]=calllib('iNEMO2_SDK','INEMO2_SetOutput',handle_dev,outmode);

%start acquisition
[error2]=calllib('iNEMO2_SDK','INEMO2_Command',handle_dev,1);

%uncomment the line below if you want to switch one the led (suggested)
[error4]=calllib('iNEMO2_SDK','INEMO2_Command',handle_dev,7);

%Initialise data structure storing data from iNemo
pFD = struct('Size',100,'ValidFields',127,'SampleTimeStamp',0, ...
'Accelerometer',struct('X',1,'Y',4,'Z',0), ...
'Gyroscope',struct('X',0,'Y',0,'Z',0), ...
'Magnetometer', struct('X',0,'Y',0,'Z',0), ...
'Pressure', 0, 'Temperature',0,'Rotation',struct('Roll',0,'Pitch',0,'Yaw',0),'Quaternion',struct('Q1',0,'Q2',0,'Q3',0,'Q4',0));

%in the main matlab algorithm just copy and paste the line below to get
%data from the device
[error3 pFD]=calllib('iNEMO2_SDK','INEMO2_GetDataSample',handle_dev,pFD);

end
