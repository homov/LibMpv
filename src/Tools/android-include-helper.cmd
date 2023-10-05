@echo off

call :abi armeabi-v7a android-arm
call :abi arm64-v8a android-arm64
call :abi x86 android-arm android-x86
call :abi x86_64 android-x64
goto :EOF

:abi
echo ^<ItemGroup Condition="$(AndroidSupportedAbis.Contains('%1')) or $(RuntimeIdentifiers.Contains('%2'))"^>
for %%f in (D:\src\LibMpv\natives\android\x86\*) do (
	call :link %1 %%~nxf
)
echo ^</ItemGroup^>
goto :EOF

:link
echo ^<AndroidNativeLibrary Include="$(NativePath)\%1\%2"^>
echo	^<Link^>%1\%2^</Link^>
echo	^<Abi^>%1^</Abi^>
echo ^</AndroidNativeLibrary^>
goto :EOF