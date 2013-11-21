# HelloSign.Net - HelloSign API Client Library for .NET

HelloSign.Net is a client library targeting .NET 4.5 and above that provides an easy
way to interact with the [HelloSign API](http://www.hellosign.com/api/gettingStarted).

## Usage examples

Get account information

```c#
var helloSign = new HelloSignClient("username", "password");
Account account = await helloSign.Account.Get(); 
Console.WriteLine("Your current callback: " + account.CallbackUrl);
```

## Supported Platforms

* .NET 4.5 (Desktop / Server)

## Getting Started


## Build


##NuGet Packages
  
* Newtonsoft.Json
* Ninject

## Integration Tests

HelloSign.Net has integration tests that access the HelloSign API, but they must be configured before they can be executed. 
To configure the tests, create a HelloSign account and then set the following two user environment variables:

- `HELLOSIGN_USERNAME` (set this to the account's username)
- `HELLOSIGN_PASSWORD` (set this to the account's password)

## Problems?


## Contribute


## Copyright and License

Copyright 2013 GitHub, Inc.

Licensed under the [MIT License](https://github.com/realcrowd/hellosign.net/blob/master/LICENSE.txt)

## Deploying a new release

