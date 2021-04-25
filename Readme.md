# [Conari](https://github.com/3F/Conari)

[![](https://raw.githubusercontent.com/3F/Conari/master/Conari/Resources/Conari_v1.png)](https://github.com/3F/Conari)

🧬 An unmanaged memory, modules, and raw data in *one-touch*.

Conari engine represents most flexible platform for working with unmanaged memory, modules, related P/Invoke features, and more around libraries, executable modules, runtime dynamic use of the unmanaged native C/C++ in .NET world and other raw data just in a few easy steps without configuring something, and... Even accessing to complex types like structures without their declaration at all.

[![Build status](https://ci.appveyor.com/api/projects/status/xbb5imyn9lr8dxbb/branch/master?svg=true)](https://ci.appveyor.com/project/3Fs/conari-wkygr/branch/master)
[![release-src](https://img.shields.io/github/release/3F/Conari.svg)](https://github.com/3F/Conari/releases/latest)
[![License](https://img.shields.io/badge/License-MIT-74A5C2.svg)](https://github.com/3F/Conari/blob/master/LICENSE)
[![NuGet package](https://img.shields.io/nuget/v/Conari.svg)](https://www.nuget.org/packages/Conari/) 
[![Tests](https://img.shields.io/appveyor/tests/3Fs/conari-wkygr/master.svg)](https://ci.appveyor.com/project/3Fs/conari-wkygr/build/tests)

[![Build history](https://buildstats.info/appveyor/chart/3Fs/conari-wkygr?buildCount=15&includeBuildsFromPullRequest=true&showStats=true)](https://ci.appveyor.com/project/3Fs/conari-wkygr/history)

> 1:[ ***[Quick start](https://github.com/3F/Conari/wiki/Quick-start)*** ] 2:[ [Basic examples for C++ and C#](https://www.youtube.com/watch?v=9Hyg3_WE9Ks) ] 3:[ [Complex types and Strings](https://www.youtube.com/watch?v=QXMj9-8XJnY) ]
> -> { **[Wiki](https://github.com/3F/Conari/wiki)** }


[![](https://raw.githubusercontent.com/3F/Conari/master/Conari/Resources/screencast_Complex_types.jpg)](https://www.youtube.com/watch?v=QXMj9-8XJnY)


## Why Conari ?

It was designed to be loyal to your needs on the fly.

🔍 Easy to start

```csharp
using ConariL l = new("...");
```

🧰 Powerful types

Forget about the type conversions and memory management complexities. Because nothing easier than just use it,

```csharp
using ConariL l = new("regXwild");
l._.replace<bool>
(
    l._T("number = 888;", out CharPtr result), 
    l._T("+??;"), l._T("2034;")
);
// result: number = 2034;
```

🚀 Awesome speed

Optional caching of 0x29 opcodes (Calli) and more.

test of regXwild's algorithms [[340x10000 Unicode](https://github.com/3F/regXwild#speed)]   | +icase [x32]| +icase [x64]         | `
----------------------------------------------|--------------|-----------------|---
regXwild **native C++** `EXT` algorithm       | **~50ms**    | **~26ms**       | `<<`
regexp-c++11(regex_search)                    | ~59309ms     | ~53334ms        |
regexp-c++11(regex_match with endings .*)     | ~59503ms     | ~53817ms        |
.NET Regex engine [Compiled]                  | ~38310ms     | ~37242ms        |
.NET Regex engine                             | ~31565ms     | ~30975ms        |
regXwild via **Conari** v1.3 (Lambda) - `EXT` | **~54ms**    | **~35ms**       | `<<`
regXwild via **Conari** v1.3 (DLR) - `EXT`    | ~214ms       | ~226ms          |

🔨 Its amazing DLR features

```csharp
using(dynamic l = new ConariX("..."))
{
    // just everything is yours ~
    l.curl_easy_setopt(curl, 10002, "https://example.com");
}
```

🔧 The easiest (most ever) access to any data in unmanaged memory

```csharp
// Everything will be generated at runtime
memory.Native()
    .f<WORD>("Machine", "NumberOfSections") // IMAGE_FILE_HEADER (0xF4)
    .align<DWORD>(3)
    .t<WORD>("SizeOfOptionalHeader")
    .t<WORD>("Characteristics")
    .region()
    .t<WORD>("Magic") // IMAGE_OPTIONAL_HEADER (0x108)
    .build(out dynamic ifh);

if(ifh.SizeOfOptionalHeader == 0xF0) { // IMAGE_OPTIONAL_HEADER64
    memory.move(0x6C);
}

// Use it !

ifh.NumberOfSections // 6
ifh.Characteristics  // IMAGE_FILE_EXECUTABLE_IMAGE | IMAGE_FILE_LARGE_ADDRESS_AWARE | IMAGE_FILE_DLL
ifh.Machine          // IMAGE_FILE_MACHINE_AMD64
ifh.Magic            // PE64
```

```csharp
dynamic l = ptr.Native().f<int>("x", "y").build();
l.x // 17
l.y // -23
```

🏄 Most powerful PInvoke and even most convenient use of WinAPI without preparing something

Conari will generate and adapt everything at runtime! Specially for you! For example, below we don't provide neither *user32.ShowWindow()* nor *user32.MessageBoxA(),* even no *kernel32.GetModuleHandleA/W()*

```csharp
dynamic user32 = new User32();

    user32.ShowWindow(0x000A0A28, 3);
    user32.MessageBoxA(0, "Conari in action", "Hello!", 0);
```

```csharp
dynamic kernel32 = new Kernel32();

    kernel32.GetModuleHandleA<IntPtr>("libcurl-x64");
    kernel32.GetModuleHandleW<IntPtr>((WCharPtr)ustr);
```

Because our recipe is simple, *Just use it!* and have fun.

🔖 Modern **.NET Core**

Conari is ready for .NET Core starting from 1.4. Even for [.NET Standard **2.0**](https://github.com/3F/Conari/issues/13) which does not cover unmanaged *EmitCalli* due to missing implementation for *System.Private.CoreLib.* Now this is another one of independent solution for everyone as https://github.com/3F/UnmanagedEmitCalli

🍰 **Open and Free**

Open Source project; MIT License; *Fork! Star! Contribute! Share! Enjoy!*

Conari is available for everyone from 2016 🎉

## 🗸 License

The [MIT License (MIT)](https://github.com/3F/Conari/blob/master/LICENSE)

```
Copyright (c) 2016-2021  Denis Kuzmin <x-3F@outlook.com> github/3F
```

[ [ ☕ Make a donation ](https://3F.github.io/Donation/) ]

Conari contributors https://github.com/3F/Conari/graphs/contributors

We're waiting for your awesome contributions!

## Take a look closer

**Dynamic features** (**DLR**, *fully automatic way*) when using of *unmanaged* code:

```csharp
var ptr     = d.test<IntPtr>(); //lambda ~ bind<Func<IntPtr>>("test")();
var codec   = d.avcodec_find_encoder<IntPtr>(AV_CODEC_ID_MP2); //lambda ~ bind<Func<ulong, IntPtr>>("avcodec_find_encoder")(AV_CODEC_ID_MP2);
              d.push(); //lambda ~ bind<Action>("push")();
              d.create<int>(ref cid, out data); //lambda ~ bind<MyFunc<Guid, object>>("create")(ref cid, out data);
```

It does not require the any configuration from you, because Conari will do it **automatically**. *Works perfectly for most popular libraries like: Lua, 7-zip, FFmpeg, ...*

Custom **Lambda expressions** (*semi-automatic way*) when using of *unmanaged* code:

```csharp
using(var l = new ConariL("Library.dll"))
{
    l.bind<Action<int, int>>("call")(2, 1); 
    double num = l.bind<Func<IntPtr, int, double>>("tonumber")(L, 4);
}
```

This also does not require the creation of any additional delegates. Just use `bind<>` methods with additional types and have fun!

```csharp
l.bind<...>("function")
```

```csharp
// you already may invoke it immediately as above:
l.bind<Action<int, string>>("set")(-1, "Hello from Conari !");

// or later:
var set = l.bind<Action<int, string>>("set");
...
set(-1, "Hello from Conari !");
```

**Native C/C++ structures without declaration** **[[?](https://github.com/3F/Conari/issues/2)]**:

```csharp

// IMAGE_FILE_HEADER: https://msdn.microsoft.com/en-us/library/windows/desktop/ms680313.aspx
dynamic ifh = binaryData.Native()
            .t<WORD, WORD>(null, "NumberOfSections")
            .align<DWORD>(3)
            .t<WORD, WORD>("SizeOfOptionalHeader")
            .build();
                
if(ifh.SizeOfOptionalHeader == 0xF0) { // IMAGE_OPTIONAL_HEADER64
    ... 
}

// IMAGE_DATA_DIRECTORY: https://msdn.microsoft.com/en-us/library/windows/desktop/ms680305.aspx
binaryData.Native()
    .t<DWORD>("VirtualAddress")
    .t<DWORD>("Size")
    .build(out dynamic idd);

DWORD offset = rva2Offset(idd.VirtualAddress);
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

**Calling Convention** & **Name-Decoration** **[[?](https://github.com/3F/Conari/issues/3)]**

```csharp
using(var l = new ConariL("Library.dll", CallingConvention.StdCall))
{
    //...
    l.Mangling = true; // _get_SevenStdCall@0 <-> get_SevenStdCall
    l.Convention = CallingConvention.Cdecl;
}
```

**Exported Variables & Raw access [[?](https://github.com/3F/Conari/issues/7#issuecomment-269123650)]**

```csharp
// v1.3+
l._.ADDR_SPEC // DLR, 0x00001CE8
l.V.get<UInt32>("ADDR_SPEC"); // lambda, 0x00001CE8
//v1.0+: Use Provider or ConariL frontend via your custom wrapper.
```

**Aliases for exported-functions and variables [[?](https://github.com/3F/Conari/issues/9#issuecomment-273855381)]**

```csharp
// v1.3+
l.Aliases["Flag"] = l.Aliases["getFlag"] = l.Aliases["xFunc"]; //Flag() -> getFlag() -> xFunc()->...
// ...
l._.getFlag<bool>();
```

**Additional types**

* TCharPtr, CharPtr, WCharPtr, float_t, int_t, ptrdiff_t, size_t, uint_t,
* NativeString (+NativeStringManager), UnmanagedStructure,
* ...

```csharp
CharPtr name = c.bind<FuncOut3<int, size_t, IntPtr>>("to")(1, out size_t len);
string myName += name; // 8 bit C-string and managed string (UTF-16)
```

```csharp
using var a = new NativeString<WCharPtr>("Hello");
using var b = a + " world!"; // unmanaged C-string, a Unicode characters
```

and more ...


### [✏ Examples](https://github.com/3F/Conari/wiki/Examples)

How about [regXwild](https://github.com/3F/regXwild) (⏱ Superfast ^Advanced wildcards++? on native unmanaged C++) in your C# code?

```csharp
using dynamic l = new ConariX("regXwild.dll");
if(l.match<bool>(input, "'+.#?'")) {
    // ... '1.4', '1.04', ...
}
```
Yes, you don't need to do anything else! Conari will prepare everything for binding with the following native method instead of you:

```cpp
REGXWILD_API_L bool match
(
    const rxwtypes::TCHAR* input,
    const rxwtypes::TCHAR* pattern,
    rxwtypes::flagcfg_t options = 0,
    EssRxW::MatchResult* result = nullptr
);
```

## How to get Conari

* NuGet: [![NuGet package](https://img.shields.io/nuget/v/Conari.svg)](https://www.nuget.org/packages/Conari/)
* [**`gnt`**](https://3f.github.io/GetNuTool/releases/latest/gnt/)` /p:ngpackages="Conari"` [[?](https://github.com/3F/GetNuTool)]
* [GitHub Releases](https://github.com/3F/Conari/releases) [ [latest](https://github.com/3F/Conari/releases/latest) ]
* CI builds at your own risk. Find latest binaries in [`/artifacts`](https://ci.appveyor.com/project/3Fs/conari-wkygr/history) page.

