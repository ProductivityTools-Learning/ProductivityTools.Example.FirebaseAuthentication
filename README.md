# FirebaseAuthentication

This example shows how to use Firebase Authentication in two scenarios:
- React application requesting protected resource from backend written in Python using Google authentication
- Windows application requesting protected resource from console application written in C# using secret key

<!--more-->

Diagram below presents all compoonents of the solution. 

- React aplication - web application needs to open google login window to receive bearer (id_token) token, next react will be calling the Cloud run application to get the resource
- Console application - needs to call firebase to generate custom_token, next use this custom token to get id_token and with this id_token request for the resource
- Cloud run - python application needs to validate bearer token and return data to react or to windows app

![Arch](Images/Arch.png)

## CloudRunPythonBackend

We have folowing resources
- Date - not protected resource which will allow us to always validate if service is working https://localhost:8080/Date
- ProtectedDate - protected resource https://localhost:8080/Date