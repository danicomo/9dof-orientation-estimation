function [] = INEMO_Disconnection(handle_dev)
%handle_dev: the device number, obtained by the iNemo connection function

%Switch down the led (if it had been switched ON)
[erroristrtr]=calllib('iNEMO2_SDK','INEMO2_Command',handle_dev,8);
%Stop acquisition
[erroristrtr]=calllib('iNEMO2_SDK','INEMO2_Command',handle_dev,2);
%Disconnect iNemo
[error]=calllib('iNEMO2_SDK','INEMO2_Disconnect',handle_dev);

unloadlibrary('iNEMO2_SDK');
end

