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

We have folow![](../2022-05-21-08-41-10.png)![](../2022-05-21-08-41-11.png)ing resources
- Date - not protected resource which will allow us to always validate if service is working https://localhost:8080/Date
- ProtectedDate - protected resource https://localhost:8080/Date
- Token - For the console application we are using custom token which will be exchanged for access token. 

### Firebase configuration

I choosed only the google provider

![](Images/2022-05-21-08-48-42.png)

After creating project we need to add apps. We add web app

![](Images/2022-05-21-08-51-38.png)

Console provides us snipped which should be added to our application

![](Images/2022-05-21-08-52-51.png)

Next we need to add to our application code responsible for opening google login page it is described [here ](https://firebase.google.com/docs/auth/web/google-signin)

```
import { getAuth, signInWithPopup, GoogleAuthProvider } from "firebase/auth";

const auth = getAuth();
signInWithPopup(auth, provider)
  .then((result) => {
    // This gives you a Google Access Token. You can use it to access the Google API.
    const credential = GoogleAuthProvider.credentialFromResult(result);
    const token = credential.accessToken;
    // The signed-in user info.
    const user = result.user;
    // ...
  }).catch((error) => {
    // Handle Errors here.
    const errorCode = error.code;
    const errorMessage = error.message;
    // The email of the user's account used.
    const email = error.customData.email;
    // The AuthCredential type that was used.
    const credential = GoogleAuthProvider.credentialFromError(error);
    // ...
  });
  ```