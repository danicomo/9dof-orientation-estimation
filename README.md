## Summary
Various kind of 9 Degrees of freedom IMU orientation estimation algorithm.

The aim of this project is to provide efficient orientation estimation algorithms using a 9 DOF IMU. The project started back in 2011 in the framework of an academic course (Microelectronics), as a google-code hosting. We used the iNEMO V2 as IMU, released at that time by ST Microelectronics (official site [iNemo](http://www.st.com/internet/evalboard/product/250367.jsp)). This project was carreied out in collaboration with ST Microelectronics, R&D group, AST Advanced System Technologies / Remote Monitoring.

## Content

We developed the following algorithms:

- Complementary filter: the simplest way to estimate orientation using a 9DOF IMU. Attached in the "Download" section Matlab and C# sources.

- Kalman filter: implementation of Kalman filter using quaternion as system state. Attached in the "Download" section Matlab and C# sources.

- AHRS estimation algorithm: this implementation is provided by the project [imumargalgorithm30042010sohm](http://code.google.com/p/imumargalgorithm30042010sohm/). We inserted the source as a class into our C# project, fitting it to our IMU (downloadable in the "Download" section).

The "docs" folder contains the following documents:
- "ProgettoMicroelettronica_Relazione_V16.pdf ", the first report written in 2011. ITALIAN LANGUAGE.
- "MicroelectronicsProject_Report_V11.pdf", a brief translation of the italian report mentioned before.
- "GaussNewton_QuaternionComplemFilter_V13.pdf", a document explaining the implementation of a quaternion based complementary filter using a Gauss-Newton method for the observation of quaternions.

## Updates

Here you can find a demonstration video of the very first implementation of the quaternion based Kalman filter (March, 2011):
[EKF demo video](http://www.youtube.com/watch?v=p8H2-vkUM0I)

Demonstration video of a very basic (2 sensors only, wired connection, bulky) prototype of a motion tracking system (December, 2011):
[First mocap demo](http://www.youtube.com/watch?v=mn8vfYt1U1I&context=C3e2c6f5ADOEgsToPDskKHH6ybFsuhwhsC-CkazIrX)

Demonstration video of our first AHRS developed in the Microelectronics Lab of University of Bergamo (August, 2012):
[iNemo M1 BT demo video](http://www.youtube.com/watch?v=RRKzzHHReRA)

Demonstration video of a basic prototype of a motion tracking system based on a network of iNemoM1-BT (November, 2012):
[Mocap iNemoM1-BT](http://www.youtube.com/watch?v=UT7Rg108syk&feature=plcp)

Miniaturized AHRS developed in the Microelectronics Lab of University of Bergamo in collaboration with STMicroelectronics (July, 2013):
[neMEMSi demo](https://www.youtube.com/watch?v=hQidXh_ohIU)

Since 2015 we are running our own business with inertial systems within 221e SRL, always in collaboration with the University of Bergamo.

## Contacts
- Github personal email 
- [221e web-site](http://www.221e.it/)

