# FabRaylib.Template

FabRaylib.Template is a cross-platform C# Raylib-based application structured into three distinct projects:

1. **FabRaylib-Desktop** - A console/desktop application that includes Avalonia UI for a graphical user interface (GUI), including file dialogs for selecting and saving files.
2. **FabRaylib-Wasm** - A WebAssembly (WASM) browser application that includes raylib.a static bindings and uses JavaScript interop for handling file input/output operations.
3. **FabRaylib-App** - The core Raylib application, providing a sample game or app with functionality for uploading an image and downloading the logo.

This structure allows for shared logic while providing platform-specific implementations where needed.

### Setup

> [!IMPORTANT]
> Please read the instructions below for building and publishing the project, as this may affect its functionality and cause unexpected errors.

Before building, ensure you have .NET 8.0 installed and then install the required WASM tools:

```
dotnet workload install wasm-tools
```

Install dotnet-serve (if not installed)

```
dotnet tool install --global dotnet-serve
```

## FabRaylib-Desktop

### Overview

FabRaylib-Desktop is the starting point for building a console or desktop application using Raylib.
It integrates Avalonia UI, which provides a cross-platform GUI. In this project,
Avalonia is used primarily for file dialogs, allowing users to open and save files through
a native-looking interface.

### Build

To build the desktop version, simply run:

```
dotnet build FabRaylib-Desktop
```

### Run

To execute the desktop version, use:

```
dotnet run --project FabRaylib-Desktop
```

## FabRaylib-Wasm

### Overview

FabRaylib-Wasm is the WebAssembly (WASM) version of the application, designed to run in the browser.
It includes a prebuilt raylib.a static library and uses JavaScript interop to handle file input and output.
Since WebAssembly does not have direct access to the local file system, JavaScript is used to trigger file
upload dialogs, read file contents, and handle file downloads.

I've followed [RaylibWasm](https://github.com/Kiriller12/RaylibWasm) example project and some official Microsoft documentation.

### Build

> Warning: Do not use Visual Studio's publish feature, as it may cause unexpected errors.

To build the WASM version, run:

```
dotnet publish -c Release --project FabRaylib-Wasm
```

### Run

To serve the published WASM files, you can use any web server. Alternatively, you can use dotnet-serve:

**Start the Web Server**

```
dotnet serve --mime .wasm=application/wasm --mime .js=text/javascript --mime .json=application/json --directory FabRaylib-Wasm/bin/Release/net8.0/browser-wasm/AppBundle/
```

Once the server is running, you can update the files with a new publish command without restarting the server.

## FabRaylib-App

### Overview

FabRaylib-App contains the actual Raylib-based application/game logic.
It includes features for image uploading and logo downloading,
demonstrating interaction with the file system across both WASM and Desktop environments.

- **In FabRaylib-Desktop**, file interactions are handled using Avalonia UI file dialogs.
- **In FabRaylib-Wasm**, file interactions are managed through JavaScript interop, which enables uploading and downloading files via the browser.

### Build & Run

This project is included in both FabRaylib-Desktop and FabRaylib-Wasm,
so it does not need to be built separately. Running either of the two platform projects will execute
FabRaylib-App within that environment.

## Notes

This project includes webassembly build of raylib native 5.5 (`raylib.a` file), because it is not included with raylib-cs nuget.

Raylib-cs may still have some webassembly compatibility issues that have been mentioned [here](https://github.com/stanoddly/DotnetRaylibWasm/issues/11) and [here](https://github.com/stanoddly/DotnetRaylibWasm/issues/4).

## Thanks

- to [Ray](https://github.com/raysan5) and all [raylib](https://github.com/raysan5/raylib) contributors for such a wonderful lib
- to [ChrisDill](https://github.com/ChrisDill) for [raylib C# bindings](https://github.com/ChrisDill/Raylib-cs)
- to [Kiriller12](https://github.com/Kiriller12) for the dotnet webassembly setup [RaylibWasm](https://github.com/Kiriller12/RaylibWasm)
- to [stanoddly](https://github.com/stanoddly) for dotnet webassembly [example project](https://github.com/stanoddly/DotnetRaylibWasm)
- [AvaloniaUI](https://github.com/AvaloniaUI/Avalonia) for providing a powerful cross-platform UI framework used in the desktop version.
