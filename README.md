# OtiumActio

OtiumActio is a platform that enables users to organize social events in different categories and provide the chance to make people gather together and socialize with each other. This project has been built in the framework of distance course Databasteknik och webbaserade System at University of Umeå under autumn semester 2021, in swedish language. Final report of project can be found here: https://github.com/niloufar-rashedi/OtiumActio/blob/master/5TF048_Projekt_NiloufarRashediB.pdf 

## Usage

The project is a MVC application written in C#, ASP.NET Core 3.1, which comes along authentication mechanism using IdentityServer4 and EmailSending services using MimeKit. The EmailSending Service can handle sending emails to the users of OtiumActio when it comes to recovery of forgotten passwords and getting confirmation links after first registration. 
In order to use this service set "From" value in "EmailConfiguration" in appsettings.json of web project to the gmail account which will send messages to the user.

```C#
  "EmailConfiguration": {
    "From": "....",
    "SmtpServer": "smtp.gmail.com",
    "Port": "",
    "Username": "....",
    "Password": "****"
  },
```
IdentityServer has been setup in OtiumActio.IdentityServer project which is referred to in API project in OtiumActio.Api. In this case, MVC controllers in the main web project should call API project to get authentication tokens for the given user. 


## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.


## License
 GNU GENERAL PUBLIC LICENSE Version 2, June 1991
