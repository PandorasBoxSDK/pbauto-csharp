# pbauto-csharp
This project is part of the Pandoras Box SDK. It implements an interface to access the Pandoras Box Automation.

## Requirements
* .Net 4.0 (may work with 2/3)

## Installation
The code is currently distributed as a single file. Either copy the code right into your project files or add **PandorasBox.cs** to your project

## Usage
The PBAuto class expects a connector in the constructor. Currently there is only the TCP connector.

```csharp
ip = "127.0.0.1"
domain = 0

connector = Tcp(ip, domain)
pb = PBAuto(connector)

// alternatively use the convenience function
pb = PBAuto.ConnectTcp(ip, domain)
```

You can then proceed to use all api functions. All functions return a struct that contains the members **ok**, **code** and **error**.

Use **ok** to check if the request was successful. **error** will return the error id if something went wrong.

If there are values to be returned then the struct will contain them as members as well.

```csharp
pb.GetSelectedDeviceCount()
// returns a GetSelectedDeviceCountResult
// .selectedDevicesCount = 2
// .ok = true
// .code = 81
// .error = undefined (because ok is true)
```

## Versioning
The version consists of a major, a minor and a revision field. (major.minor.revision)
If the major version changes, then these changes are incompatible with prior versions. Changes to the minor version indicate backwards compatibility. The revision field is reserved for the Pandoras Box revision that is required to use all features. It might be possible to use the SDK with an older revision of Pandoras Box, but not all the commands will work.

## Contributing
Most of the files in this repository are generated. Please contribute to the template files instead.
https://github.com/coolux/pbauto-generator

v0.1.12086, generated on 2016-02-22