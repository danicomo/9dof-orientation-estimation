function [ q ] = GetQuaternionFromAngle(Angles)
    
    q=zeros(4,1);
    %c1=cosd(Angles(1,1)/2);
    %c2=cosd(Angles(2,1)/2);
    %c3=cosd(Angles(3,1)/2);
    %s1=sind(Angles(1,1)/2);
    %s2=sind(Angles(2,1)/2);
    %s3=sind(Angles(3,1)/2);
    
    q(1,1)=cosd(Angles(1,1)/2)*cosd(Angles(2,1)/2)*cosd(Angles(3,1)/2)+sind(Angles(1,1)/2)*sind(Angles(2,1)/2)*sind(Angles(3,1)/2);
    q(2,1)=sind(Angles(1,1)/2)*cosd(Angles(2,1)/2)*cosd(Angles(3,1)/2)-cosd(Angles(1,1)/2)*sind(Angles(2,1)/2)*sind(Angles(3,1)/2);
    q(3,1)=cosd(Angles(1,1)/2)*sind(Angles(2,1)/2)*cosd(Angles(3,1)/2)+sind(Angles(1,1)/2)*cosd(Angles(2,1)/2)*sind(Angles(3,1)/2);
    q(4,1)=cosd(Angles(1,1)/2)*cosd(Angles(2,1)/2)*sind(Angles(3,1)/2)-sind(Angles(1,1)/2)*sind(Angles(2,1)/2)*cosd(Angles(3,1)/2);
    
    %q(1,1)=c1*c2*c3-s1*s2*s3;
    %q(2,1)=c1*c2*s3+s1*s2*c3;
    %q(3,1)=s1*c2*c3+c1*s2*s3;
    %q(4,1)=c1*s2*c3-s1*c2*s3;
end

