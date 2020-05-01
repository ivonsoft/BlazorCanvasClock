# Canvas Clock Blazor

This is implementation of simple canvas clock in Blazor technology (TargetFramework: netcoreapp3.1).
Blazor communicate via javascript with html5 canvas to draw elements. Project uses simple endless loop running in C# invoking methods from JSInterop class. It should be rewritten to use more efficient drawing techniques by internal browser mechanisms like requestAnimationFrames.  
This project include only very simple controlling functions in JS to draw on canvas. My intentions was not making another canvas library for Blazor because there are some other efficient options like Blazor Canvas extensions to run drawing on canvas. 


## Getting Started

Using this project is simple... 
You can run it on console or IDE of your choice like Vscode by typing 
```
dotnet run
```

### Prerequisites

Running this blazor project needs installed latest dotnet core SDK like 3.1 edition, it can be found at: https://dotnet.microsoft.com/download/dotnet-core/3.1


### Installing

1. Install latest dotnetcore sdk, like 3.1, this page: https://dotnet.microsoft.com/download/dotnet-core/3.1

2. Download source file from github, unzzip package
3. Open console or advanced editor like Visual Studio Code
4. move to directory where BlazorClockCanvas.csproj is located
5. and type
```
    dotnet run
```

## Running the tests

This project does not contain test folder

## Deployment, Debbuging

This scenario does not include docker file to test and debug project in container


## Contributing

Please feel free to download and test source code, if someone rewrite this code to use requestAnimationFrame and introduce any amendments, I would be appreciate for feedback

## Authors

* **Krzysztof Szczerbowski** - *Initial work* 

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

Javascript code was based on:
https://jsbin.com/cidike/edit?html,css,js,output

Moreover, I found inspiration based on fragments of code:
https://github.com/SQL-MisterMagoo/BlazorTest/tree/master/BlazorClock
https://dev.to/azure/creating-dev-s-offline-page-using-blazor-29dl



