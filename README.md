<!--Category:PowerShell,C#,Python,React,Firebase,Example,WinApp--> 
 <p align="right">
    <a href="http://productivitytools.tech/cloud-blobs/"><img src="Images/Header/ProductivityTools_green_40px_2.png" /><a> 
    <a href="https://github.com/pwujczyk/ProductivityTools.CloudBlobs"><img src="Images/Header/Github_border_40px.png" /></a>
</p>
<p align="center">
    <a href="http://http://productivitytools.tech/">
        <img src="Images/Header/LogoTitle_green_500px.png" />
    </a>
</p>

# FirebaseAuthentication

This example shows how to use Firebase Authentication in two scenarios:
- React application requesting protected resource from backend written in Python using Google authentication
- Windows application requesting protected resource from console application written in C# 

<!--more-->

Diagram below presents all compoonents of the solution. 

- React aplication - web application needs to open google login window to receive bearer (id_token) token, next react will be calling the Cloud run application to get the resource
- Console application - needs to call firebase to generate custom_token, next use this custom token to get id_token and with this id_token request for the resource
- Cloud run - python application needs to validate bearer token and return data to react or to windows app

<!--og-image-->
![Arch](Images/Arch.png)

## CloudRunPythonBackend

We have folow![](../2022-05-21-08-41-10.png)![](../2022-05-21-08-41-11.png)ing resources
- Date - not protected resource which will allow us to always validate if service is working https://localhost:8080/Date
- ProtectedDate - protected resource https://localhost:8080/ProtectedDate
- Token - For the console application we are using custom token which will be exchanged for access token. https://localhost:8080/Token

### Firebase configuration

I choosed only the google provider

![](Images/2022-05-21-08-48-42.png)

After creating project we need to add apps. We add web app

![](Images/2022-05-21-08-51-38.png)

Console provides us snipped which should be added to our application

![](Images/2022-05-21-08-52-51.png)

Next we need to add to our application code responsible for opening google login page it is described [here ](https://firebase.google.com/docs/auth/web/google-signin)

```javascript
import { getAuth, signInWithPopup, GoogleAuthProvider } from "firebase/auth";

const auth = getAuth(app);

const googleProvider = new GoogleAuthProvider();
const signInWithGoogle = async () => {
    try {
        debugger;
        const res = await signInWithPopup(auth, googleProvider);
        console.log(res);
    } catch (err) {
        console.error(err);
        alert(err.message);
    }
};
```


### Service account
To validate token we need to create firebase app. To do it we need to reference service account key. We can create application without any parameters ```default_app=initialize_app()``` then application will look for the file path under environment variable ```GOOGLE_APPLICATION_CREDENTIALS```. In our example I will load service account key from drive.

Download service account key:

![Service account key](Images/2022-05-21-22-10-49.png)

### Token validation
Probably there is more 'pro' way of validating token, I did it quick and dirty: ```auth.verify_id_token()``` returns data if token is correct and throws exception if not.

```python
class ProtectedDateResource(Resource):
    def get(selfs):
        id_token=request.headers.environ["HTTP_AUTHORIZATION"]
        id_token=id_token.replace("Bearer","")
        id_token=id_token.replace(" ","")
        decoded_token=auth.verify_id_token(id_token)
        today=datetime.now()
        tstr=today.strftime('%m/%d/%Y %H:%m:%S')
        return tstr,HTTPStatus.OK
```
Our service should work 

![](Images/2022-05-23-23-08-24.png)

## Console app
Console app do not have an interface in which we could login, so we need to create a customtoken on the server side and return it to the consol app. Then console app will take this custom_token and ask server with it to obtain access_token

### Token service in backend app
It is easy we are creating endpoint which will return us token
```python
class TokenResource(Resource):
    def get(self):
        custom_token=auth.create_custom_token("password123");
        return Response(custom_token, mimetype="text/plain",direct_passthrough=True)
```

### Console app

To call the service first we get custom token, next with this custom token we are calling for the access_token and then for the resource. 

```c#
string custom_token = await CustomToken.GetCustomToken();
Console.WriteLine($"Custom token: {custom_token}");
string access_token = await AccessToken.GetAccesToken(custom_token);
Console.WriteLine($"Access token: {access_token}");
string result = await RestService.GetResource(access_token);
Console.WriteLine(result);
Console.ReadLine();
```

#### 
Access token is retrieved from googleapis. To get it we need to call for the given adddress with Web API Key in the query


```c#
public static async Task<string> GetAccesToken(string custom_token)
{
    var HttpClient = new HttpClient();
    Uri url = new Uri(@"https://identitytoolkit.googleapis.com/v1/accounts:signInWithCustomToken?key=AIzaSyDgSHqUdtL0XQ1i95Y_fMKTHV48Yjo_ZWs");

    HttpClient.DefaultRequestHeaders.Accept.Clear();
    HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

    object obj = new { token = custom_token, returnSecureToken = true };
    var dataAsString = JsonConvert.SerializeObject(obj);
    var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

    HttpResponseMessage response = await HttpClient.PostAsync(url, content);
    if (response.IsSuccessStatusCode)
    {
        var resultAsString = await response.Content.ReadAsStringAsync();
        AccessToken result = JsonConvert.DeserializeObject<AccessToken>(resultAsString);
        return result.idToken;
    }
    throw new Exception(response.ReasonPhrase);
}
```
![](Images/2022-05-24-07-01-13.png)

Working application:
![](Images/2022-05-24-07-21-15.png)