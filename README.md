# Parrot Wings
This application is designed for Parrot Wings (PW, virtual money) transfer between system users.

Technology stack:
- Server side
  - C#
  - EF Code First
  - Web API
  - SignalR
  - Bearer Token Authentication for Web API and SignalR
- Client side
  - SignalR
  - AngularJS
  - Bootstrap

To run application:

In Solution Explorer right click on Solution "ParrotWings", choose "Properties".
Common Properties -> Startup Project.

Set Multiple startup projects, for Project "PW.Client" set "Start", for Project "PW.WebAPI" set "Start".
Click "OK".

In Solution Explorer right click on Project "PW.WebAPI", choose "Properties".
Web -> Start Action set "Don't open a page. Wait for a request from an external application".

Save properties.
