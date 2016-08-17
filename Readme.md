# Conari

[![](https://raw.githubusercontent.com/3F/Conari/master/Conari/Resources/Conari_v1.png)](https://github.com/3F/Conari/fork)

Binder of Unmanaged code for .NET

The Conari represents a flexible platform to work with unmanaged code, other native and binary data.
The lightweight and powerful binding from any exported functions of libraries (a library or executable module), and more...

*Did you know: The [LunaRoad](https://github.com/3F/LunaRoad) project works over Conari.*

[![Build status](https://ci.appveyor.com/api/projects/status/qc1d3ofsso8fd67t/branch/master?svg=true)](https://ci.appveyor.com/project/3Fs/conari/branch/master)
[![release-src](https://img.shields.io/github/release/3F/Conari.svg)](https://github.com/3F/Conari/releases/latest)
[![License](https://img.shields.io/badge/License-MIT-74A5C2.svg)](https://github.com/3F/Conari/blob/master/LICENSE)
[![NuGet package](https://img.shields.io/nuget/v/Conari.svg)](https://www.nuget.org/packages/Conari/) 

**Easy to start:**

```csharp
using(IConari c = new ConariL("Library.dll")) {
    // ...
}
```

Conari is ready for any exported functions immediately via:

* **Dynamic features** / **DLR**:

```csharp
var ptr     = d.test<IntPtr>(); // eqv: l.bind<Func<IntPtr>>("test")();
var codec   = d.avcodec_find_encoder<IntPtr>(AV_CODEC_ID_MP2); // eqv: l.bind<Func<ulong, IntPtr>>("avcodec_find_encoder")(AV_CODEC_ID_MP2);
              d.push(); // eqv: l.bind<Action>("push")();
```

*It does not require the any configuration from you, we will do it* ***automatically.*** *Easy and works well.*

* **Lambda expressions**:

```csharp
using(var c = new ConariL("Library.dll"))
{
    c.bind<Action<int, int>>("call")(2, 1); 
    double num = c.bind<Func<IntPtr, int, double>>("tonumber")(L, 4);
}
```

This also does not require the creation of any additional **delegate**. The Conari will do it **automatically** instead of you. 
Just use `bind<>` methods and have fun !

```csharp
c.bind<...>("function")
```

```csharp
// you already may invoke it immediately as above:
c.bind<Action<int, string>>("set")(-1, "Hello from Conari !");

// or later:
var set = c.bind<Action<int, string>>("set");
...
set(-1, "Hello from Conari !");
```

**Lazy loading:**

```csharp
using(var l = new ConariL(
                    new Config("Library.dll") {
                        LazyLoading = true
                    }))
{
    ...
}
```

**Native C/C++ structures without declaration** **[[?](https://github.com/3F/Conari/issues/2)]**:

```csharp

// IMAGE_FILE_HEADER: https://msdn.microsoft.com/en-us/library/windows/desktop/ms680313.aspx
dynamic ifh = NativeData
                ._(data)
                .t<WORD, WORD>(null, "NumberOfSections")
                .align<DWORD>(3)
                .t<WORD, WORD>("SizeOfOptionalHeader")
                .Raw.Type;
                
if(ifh.SizeOfOptionalHeader == 0xE0) { // IMAGE_OPTIONAL_HEADER32
    ... 
}

// IMAGE_DATA_DIRECTORY: https://msdn.microsoft.com/en-us/library/windows/desktop/ms680305.aspx
dynamic idd = (new NativeData(data))
                    .t<DWORD>("VirtualAddress") // idd.VirtualAddress
                    .t<DWORD>("Size")           // idd.Size
                    .Raw.Type;
```

```csharp
Raw mt = NativeData
            ._(ptr)
            .align<int>(2, "a", "b")
            .t<IntPtr>("name")
            .Raw;
            
-     {byte[0x0000000c]} byte[]
        [0]    0x05    byte --
        [1]    0x00    byte   |
        [2]    0x00    byte   |
        [3]    0x00    byte --^ a = 5
        [4]    0x07    byte --
        [5]    0x00    byte   |
        [6]    0x00    byte   |
        [7]    0x00    byte --^ b = 7
        [8]    0x20    byte --
        [9]    0x78    byte   |_ pointer to allocated string: (CharPtr)z.name
        [10]   0xf0    byte   |
        [11]   0x56    byte --
...
```

**Calling Convention** & **Name-Decoration** **[[?](https://github.com/3F/Conari/issues/3)]**:

```csharp
using(var l = new ConariL("Library.dll", CallingConvention.StdCall))
{
    //...
    l.Mangling = true; // _get_SevenStdCall@0 <-> get_SevenStdCall
    l.Convention = CallingConvention.Cdecl;
}
```

**Additional types:**

* BSTR, CharPtr, float_t, int_t, ptrdiff_t, size_t, uint_t, WCharPtr
* UnmanagedString - to allocation of new unmanaged strings for your unmanaged code.
* ...

```csharp

size_t len;
CharPtr name = c.bind<FuncOut3<int, size_t, IntPtr>>("to")(1, out len);
...
string myName += name; // (IntPtr)name; .Raw; .Ansi; .Utf8; ...
...
uint_t v = dll.getU(2);
```

**Events**:

```csharp
l.ConventionChanged += (object sender, DataArgs<CallingConvention> e) =>
{
    DLR = newDLR(e.Data);
    LSender.Send(sender, $"DLR has been updated with new CallingConvention: {e.Data}", Message.Level.Info);
};

l.BeforeUnload += (object sender, DataArgs<Link> e) =>
{
    // Do not forget to do something before unloading a library
};

...
```


**and more ...** 

*you wish, we doing*

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


[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_SM.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=entry%2ereg%40gmail%2ecom&lc=US&item_name=3F%2dOpenSource%20%5b%20github%2ecom%2f3F&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_SM%2egif%3aNonHosted)
