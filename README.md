# REST Caller

[![NuGet Version](http://img.shields.io/nuget/v/Agero.Core.RestCaller.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.RestCaller/) 
[![NuGet Downloads](http://img.shields.io/nuget/dt/Agero.Core.RestCaller.svg?style=flat)](https://www.nuget.org/packages/Agero.Core.RestCaller/)

Library to make RESTful calls in .NET applications. 

* Create REST caller
```csharp
var caller = new RESTCaller();
```

* Make GET RESTfull call
```csharp
// Making GET call to https://jsonplaceholder.typicode.com/posts?userId=1 
var getResponse = await caller.GetAsync(
    uri: new Uri("https://jsonplaceholder.typicode.com/posts"),
    parameters: new Dictionary<string, string>
    {
        {"userId", "1"}
    });
```

* Make POST RESTfull call
```csharp
// Making GET call to https://jsonplaceholder.typicode.com/posts?userId=1 
var postResponse = await caller.PostAsync(
    uri: new Uri("https://jsonplaceholder.typicode.com/posts"),
    body: @"{ ""title"": ""foo"", ""body"": ""bar"", ""userId"": 1 }");
```

Please refer to [`IRESTCaller`](./Agero.Core.RestCaller/IRESTCaller.cs) and [`RESTCallerExtensions`](./Agero.Core.RestCaller/Extensions/RESTCallerExtensions.cs) for more details.