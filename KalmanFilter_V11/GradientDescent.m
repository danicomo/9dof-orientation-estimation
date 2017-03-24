function [q_result] = GradientDescent(Acc,Magn,q,mu)
    Magn(1,1)=Magn(1,1)+0.38;
    Magn(2,1)=Magn(2,1)*1.1;
    Magn(3,1)=Magn(3,1)-0.08;
    q1=q(1,1);
    q2=q(2,1);
    q3=q(3,1);
    q4=q(4,1);
    i=1;
    while(i<=10)
        fg1=2*(q2*q4-q1*q3)-Acc(1,1);
        fg2=2*(q1*q2+q3*q4)-Acc(2,1);
        fg3=2*(0.5-q2^2-q3^2)-Acc(3,1);
        fg=[fg1;fg2;fg3];

        Jg1=[-2*q3 2*q4 -2*q1 2*q2];
        Jg2=[2*q2 2*q1 2*q4 2*q3];
        Jg3=[0 -4*q2 -4*q3 0];

        Jg=[Jg1;Jg2;Jg3];

        m=Magn/norm(Magn);

        q_coniug=[q(1,1); -q(2:4,1)];

        hTemp=QuaternionProduct(q,[0;m]);
        h=QuaternionProduct(hTemp,q_coniug);

        b=[sqrt(h(2,1)^2+h(3,1)^2) 0 h(4,1)]';

        b=b/norm(b);
        
        %b=[-0.0318 -0.1715 -0.9847]';


        fb1=2*b(1,1)*(0.5-q3^2-q4^2)+2*b(3,1)*(q2*q4-q1*q3)-m(1,1);
        fb2=2*b(1,1)*(q2*q3-q1*q4)+2*b(3,1)*(q1*q2+q3*q4)-m(2,1);
        fb3=2*b(1,1)*(q1*q3+q2*q4)+2*b(3,1)*(0.5-q2^2-q3^2)-m(3,1);
        fb=[fb1;fb2;fb3];

        Jb1=[-2*b(3,1)*q3 2*b(3,1)*q4 -4*b(1,1)*q3-2*b(3,1)*q1 -4*b(1,1)*q4+2*b(3,1)*q2];
        Jb2=[-2*b(1,1)*q4+2*b(3,1)*q2 2*b(1,1)*q3+2*b(3,1)*q1 2*b(1,1)*q2+2*b(3,1)*q4 -2*b(1,1)*q1+2*b(3,1)*q3];
        Jb3=[2*b(1,1)*q3 2*b(1,1)*q4-4*b(3,1)*q2 2*b(1,1)*q1-4*b(3,1)*q3 2*b(1,1)*q2];
        Jb=[Jb1;Jb2;Jb3];

        fgb=[fg;fb];
        Jgb=[Jg;Jb];

        Df=Jgb'*fgb;

        q_Temp=q-mu*Df/norm(Df);

        q_result=[q_Temp(1,1);q_Temp(2:4,1);];
        
        q_result=q_result/norm(q_result);
        
        q1=q_result(1,1);
        q2=q_result(2,1);
        q3=q_result(3,1);
        q4=q_result(4,1);
        q=[q1 q2 q3 q4]';
        i=i+1;
    end
end

