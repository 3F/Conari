# [Conari](https://github.com/3F/Conari)

[![](https://raw.githubusercontent.com/3F/Conari/master/Conari/Resources/Conari_v1.png)](https://github.com/3F/Conari)

Binder of Unmanaged code for .NET

The Conari engine represents flexible platform for work with unmanaged code (native C/C++ etc.): Libraries, Executable Modules, other native and binary data.
Lightweight and powerful binding with any exported-functions/variables, and much more.

*[List of Projects via Conari engine](https://github.com/3F/Conari/wiki/Projects)*

[![Build status](https://ci.appveyor.com/api/projects/status/qc1d3ofsso8fd67t/branch/master?svg=true)](https://ci.appveyor.com/project/3Fs/conari/branch/master)
[![release-src](https://img.shields.io/github/release/3F/Conari.svg)](https://github.com/3F/Conari/releases/latest)
[![License](https://img.shields.io/badge/License-MIT-74A5C2.svg)](https://github.com/3F/Conari/blob/master/LICENSE)
[![NuGet package](https://img.shields.io/nuget/v/Conari.svg)](https://www.nuget.org/packages/Conari/) 

[ **[Wiki](https://github.com/3F/Conari/wiki)** ]

**Easy to start:**

```csharp
using(IConari l = new ConariL("Library.dll")) {
    // ...
}
```

Conari is ready for any exported functions immediately via:

**Dynamic features** / **DLR** - *fully automatic way*:

```csharp
var ptr     = d.test<IntPtr>(); //Lambda variant: l.bind<Func<IntPtr>>("test")();
var codec   = d.avcodec_find_encoder<IntPtr>(AV_CODEC_ID_MP2); //Lambda variant: l.bind<Func<ulong, IntPtr>>("avcodec_find_encoder")(AV_CODEC_ID_MP2);
              d.push(); //Lambda variant: l.bind<Action>("push")();
              d.create<int>(ref cid, out data); //Lambda variant: l.bind<MyFunc<Guid, object>>("create")(ref cid, out data);
```

It does not require the any configuration from you, because Conari will do it **automatically**. *Easy and works well.*

*This works perfectly for most popular libraries like: Lua, 7-zip, FFmpeg, ...*

**Lambda expressions** - *semi-automatic way*:

```csharp
using(var c = new ConariL("Library.dll"))
{
    c.bind<Action<int, int>>("call")(2, 1); 
    double num = c.bind<Func<IntPtr, int, double>>("tonumber")(L, 4);
}
```

This also does not require the creation of any additional **delegate**. Just use `bind<>` methods with additional types and have fun !

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
IntPtr ptr ...
Raw mt = ptr.Native()
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
        [9]    0x78    byte   |_ pointer to allocated string: (CharPtr)name
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

**Exported Variables & Raw access [[?](https://github.com/3F/Conari/issues/7#issuecomment-269123650)]:**

```csharp
// v1.3+
l.ExVar.DLR.ADDR_SPEC // 0x00001CE8
l.ExVar.get<UInt32>("ADDR_SPEC"); // 0x00001CE8
l.ExVar.getField(typeof(UInt32).NativeSize(), "ADDR_SPEC"); // Native.Core.Field via raw size
l.Svc.native("lpProcName"); // Raw access via NativeData & Native.Core !
//v1.0+: Use Provider or ConariL frontend via your custom wrapper.
```

**Aliases for exported-functions and variables [[?](https://github.com/3F/Conari/issues/9#issuecomment-273855381)]:**

```csharp
// v1.3+
l.Aliases["Flag"] = l.Aliases["getFlag"] = l.Aliases["xFunc"]; //Flag() -> getFlag() -> xFunc()->...
// ...
l.DLR.getFlag<bool>();
```

**Additional types:**

* BSTR, CharPtr, WCharPtr, float_t, int_t, ptrdiff_t, size_t, uint_t
* UnmanagedString - to allocation of new unmanaged strings for your unmanaged code.
* ...

```csharp
size_t len;
CharPtr name = c.bind<FuncOut3<int, size_t, IntPtr>>("to")(1, out len);
string myName += name; // (IntPtr)name; .Raw; .Ansi; .Utf8; ...
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


and more !


### [Samples](https://github.com/3F/Conari/wiki/Projects)

How about to use [regXwild](https://github.com/3F/regXwild) (Fast and powerful wildcards on C++) in your C# code ? It easy:

```csharp
using(var l = new ConariL("regXwild.dll")) {
...
    if(l.DLR.searchEssC<bool>((WCharPtr)data, (WCharPtr)filter, false)) {
        // ...
    }
}
```
yes, you don't need to do anything else ! The Conari will prepare all required operations and binding with native method instead of you:

```cpp
REGXWILD_API bool searchEssC(const TCHAR* data, const TCHAR* filter, bool ignoreCase);
```

have fun !

----


## License

The [MIT License (MIT)](https://github.com/3F/Conari/blob/master/LICENSE)

```
Copyright (c) 2016-2017  Denis Kuzmin <entry.reg@gmail.com>
```

##

### How to Get

Available variants:

* NuGet PM: `Install-Package Conari`
* [GetNuTool](https://github.com/3F/GetNuTool): `msbuild gnt.core /p:ngpackages="Conari"` or **[gnt](https://github.com/3F/GetNuTool/releases/download/v1.6/gnt.bat)** /p:ngpackages="Conari"
* NuGet Commandline: `nuget install Conari`
* [/releases](https://github.com/3F/Conari/releases) ( [latest](https://github.com/3F/Conari/releases/latest) )
* [Nightly builds](https://ci.appveyor.com/project/3Fs/conari/history) (`/artifacts` page). But remember: It can be unstable or not work at all. Use this for tests of latest changes.


[![Donate](https://www.paypalobjects.com/en_US/i/btn/btn_donate_SM.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=entry%2ereg%40gmail%2ecom&lc=US&item_name=3F%2dOpenSource%20%5b%20github%2ecom%2f3F&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_SM%2egif%3aNonHosted)
