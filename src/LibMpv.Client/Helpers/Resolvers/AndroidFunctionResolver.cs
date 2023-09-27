﻿using System;
using System.Runtime.InteropServices;

namespace LibMpv.Client;

public class AndroidFunctionResolver : FunctionResolverBase
{
    private const string Libdl = "libdl.so";

    private const int RTLD_NOW = 0x002;

    protected override string GetNativeLibraryName(string libraryName, int version)
    {
        return version > 0 ? $"{libraryName}.so.{version}" : $"{libraryName}.so";
    } 

    protected override IntPtr LoadNativeLibrary(string libraryName) => dlopen(libraryName, RTLD_NOW);

    protected override IntPtr FindFunctionPointer(IntPtr nativeLibraryHandle, string functionName) => dlsym(nativeLibraryHandle, functionName);


    [DllImport(Libdl)]
    public static extern IntPtr dlsym(IntPtr handle, string symbol);

    [DllImport(Libdl)]
    public static extern IntPtr dlopen(string fileName, int flag);
}
