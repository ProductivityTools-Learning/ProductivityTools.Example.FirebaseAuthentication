// See https://aka.ms/new-console-template for more information
using ConsoleApp;

Console.WriteLine("Hello, World!");

string custom_token = await CustomToken.GetCustomToken();
Console.WriteLine($"Custom token: {custom_token}");
string access_token = await AccessToken.GetAccesToken(custom_token);
Console.WriteLine($"Access token: {access_token}");
string result = await RestService.GetResource(access_token);
Console.WriteLine(result);
Console.ReadLine();
