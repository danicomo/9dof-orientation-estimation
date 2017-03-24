function [M] = ComputeM_Matrix(a,b,c,d)
%Compute the rotation transformation matrix based on quaternions

R11=d^2+a^2-b^2-c^2;
R12=2*(a*b-c*d);
R13=2*(a*c+b*d);
R21=2*(a*b+c*d);
R22=d^2+b^2-a^2-c^2;
R23=2*(b*c-a*d);
R31=2*(a*c-b*d);
R32=2*(b*c+a*d);
R33=d^2+c^2-b^2-a^2;

R=[R11 R12 R13;R21 R22 R23;R31 R32 R33];

M=[R zeros(3,3);zeros(3,3) R];
end

