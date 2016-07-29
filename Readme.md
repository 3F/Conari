# Conari

[![](https://raw.githubusercontent.com/3F/Conari/master/Conari/Resources/Conari_v1.png)](https://github.com/3F/Conari/fork)

Binder of Unmanaged code for .NET

The Conari represents a flexible platform to work with any exported functions of libraries (a library or executable module). The lightweight and powerful binding from unmanaged code now is much more easier !

*Did you know: The [LunaRoad](https://github.com/3F/LunaRoad) project works over Conari.*

[![Build status](https://ci.appveyor.com/api/projects/status/94y78phdvkoi5oda/branch/master?svg=true)](https://ci.appveyor.com/project/3Fs/conari/branch/master)
[![release-src](https://img.shields.io/github/release/3F/Conari.svg)](https://github.com/3F/Conari/releases/latest)
[![License](https://img.shields.io/badge/License-MIT-74A5C2.svg)](https://github.com/3F/Conari/blob/master/LICENSE)
[![NuGet package](https://img.shields.io/nuget/v/Conari.svg)](https://www.nuget.org/packages/Conari/) 

Easy to start:

```csharp
using(IConari c = new ConariL("Library.dll")) {
    // ...
}
```

Conari is ready for any exported functions, immediately:

```csharp
using(var c = new ConariL("Library.dll"))
{
    c.bind<Action<IntPtr>>("pushnil")(L);
    c.bind<Action<IntPtr, int, int>>("call")(L, 2, 1); 
    double num = c.bind<Func<IntPtr, int, double>>("tonumber")(L, 4);
}
```

Lazy loading:

```csharp
using(var l = new ConariL(
                    new Config("Library.dll") {
                        LazyLoading = true
                    }))
{
    ...
}
```

[Calling Convention](https://msdn.microsoft.com/en-us/library/system.runtime.interopservices.callingconvention.aspx):

```csharp
using(var l = new ConariL("Library.dll", CallingConvention.Cdecl))
{
    //...
    l.Convention = CallingConvention.StdCall;
}
```

Additional types:

```csharp

size_t len;
CharPtr name = c.bind<FuncOut3<int, size_t, IntPtr>>("to")(1, out len);
...
string myName += name; // (IntPtr)name; .Raw; .Ansi; .Utf8; ...
...
uint_t v = dll.getU(2);
```


and more ...

----


## License

The [MIT License (MIT)](https://github.com/3F/Conari/blob/master/LICENSE)

```
Copyright (c) 2016  Denis Kuzmin <entry.reg@gmail.com>
```

##

### How to Get

Available variants:

* NuGet PM: `Install-Package Conari`
* [GetNuTool](https://github.com/3F/GetNuTool): `msbuild gnt.core /p:ngpackages="Conari"` or [gnt](https://github.com/3F/GetNuTool/releases/download/v1.5/gnt.bat) /p:ngpackages="Conari"
* NuGet Commandline: `nuget install Conari`
* [/releases](https://github.com/3F/Conari/releases) ( [latest](https://github.com/3F/Conari/releases/latest) )
* [Nightly builds](https://ci.appveyor.com/project/3Fs/conari/history) (`/artifacts` page). But remember: It can be unstable or not work at all. Use this for tests of latest changes.

