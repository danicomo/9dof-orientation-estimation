function [StateF] = MyFilter(b, a, State)
    %This function filters the State param, according to a filter (b,a)
    % accF_Length: length of the IIR low pass filtering
    % i: iteration step
    
    %Initialize the return param
    StateF=zeros(3,1);
    
    %Compute the filtering step
    length=size(State);
    length=length(1,2);
    
    %Filtering
    FiltTemp=filter(b,a,State(1,:));
    StateF(1,1)=FiltTemp(1,length);
    FiltTemp=filter(b,a,State(2,:));
    StateF(2,1)=FiltTemp(1,length);
    FiltTemp=filter(b,a,State(3,:));       
    StateF(3,1)=FiltTemp(1,length);
    
end

