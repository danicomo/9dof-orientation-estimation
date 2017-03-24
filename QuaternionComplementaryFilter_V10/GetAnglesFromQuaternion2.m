function [Angles] = GetAnglesFromQuaternion2(q)
    q0=q(1,1);
    q1=q(2,1);
    q2=q(3,1);
    q3=q(4,1);
    
    Angles(1,1)=atan2(2*q2*q3+2*q0*q1,1-2*q1^2-2*q2^2)*180/pi;
    Angles(2,1)=asind(-2*q1*q3+2*q0*q2);
    Angles(3,1)=atan2(2*q1*q2+2*q0*q3,1-2*(q2^2+q3^2))*180/pi;

end

