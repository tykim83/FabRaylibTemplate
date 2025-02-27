# FabRaylibTemplate

RaylibWasm is a cross-platform C# raylib application that runs both as a WebAssembly (WASM) app in the browser and as a native console/desktop app. In WASM mode, it uses JavaScript interop for file operations, while in console mode, it uses Avalonia for modern file dialogs—all through a single file service interface.

I've followed [RaylibWasm](https://github.com/Kiriller12/RaylibWasm) example project and some official Microsoft documentation.

> [!IMPORTANT]
> Please read the instructions below for building and publishing the project, as this may affect its functionality and cause unexpected errors.

## WebAssembly (Browser) Mode

### Setup

You must have .Net 8.0 installed before start.

Then install wasm toolset:

```
dotnet workload install wasm-tools
```

### Build

> [!WARNING]
> Do not use Visual Studio publication, it may cause some strange errors!

Just call this command from the root directory of the solution:

```
dotnet publish -c Release
```

### Run

You could use whatever web-server you want to serve published files.

OR

You could also use `dotnet serve` for this purpose:

If it's not installed, you need to install it with this command:

```
dotnet tool install --global dotnet-serve
```

And then just call this command to start web server for your build:

```
dotnet serve --mime .wasm=application/wasm --mime .js=text/javascript --mime .json=application/json --directory bin\Debug\net8.0\browser-wasm\AppBundle\
```

While server is running you can use publish command to update your files without any need to restart server.

## Console (Desktop) Mode

### Run

```
dotnet run -c Console
```

## Notes

This project includes webassembly build of raylib native 5.5 (`raylib.a` file), because it is not included with raylib-cs nuget.

Raylib-cs may still have some webassembly compatibility issues that have been mentioned [here](https://github.com/stanoddly/DotnetRaylibWasm/issues/11) and [here](https://github.com/stanoddly/DotnetRaylibWasm/issues/4).

This project is not perfect, so I would welcome your suggestions and PR requests.

## Thanks

- to [Ray](https://github.com/raysan5) and all [raylib](https://github.com/raysan5/raylib) contributors for such a wonderful lib
- to [ChrisDill](https://github.com/ChrisDill) for [raylib C# bindings](https://github.com/ChrisDill/Raylib-cs)
- to [Kiriller12](https://github.com/Kiriller12) for the dotnet webassembly setup [RaylibWasm](https://github.com/Kiriller12/RaylibWasm)
- to [stanoddly](https://github.com/stanoddly) for dotnet webassembly [example project](https://github.com/stanoddly/DotnetRaylibWasm)
