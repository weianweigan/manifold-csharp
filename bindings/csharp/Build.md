# Windows

> Release

```shell
cmake . -DCMAKE_BUILD_TYPE=Release -DBUILD_SHARED_LIBS=OFF -DMANIFOLD_DEBUG=ON -DMANIFOLD_PAR=NONE -A x64 -B build
cd build
cmake --build . --target ALL_BUILD --config Release
```

\build\bin\Release\manifoldc.dll

> Debug

```shell
cmake . -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=OFF -DMANIFOLD_DEBUG=ON -DMANIFOLD_PAR=NONE -A x64 -B build
cd build
cmake --build . --target ALL_BUILD --config Debug
```

# Windows-MeshExport

## Config vspkg

```shell
git clone https://github.com/microsoft/vcpkg.git
cd vcpkg; .\bootstrap-vcpkg.bat
$env:VCPKG_ROOT = "C:\path\to\vcpkg"
$env:PATH = "$env:VCPKG_ROOT;$env:PATH"
vcpkg install assimp
```


> Release

```shell
cmake . -DCMAKE_BUILD_TYPE=Release -DBUILD_SHARED_LIBS=OFF -DMANIFOLD_DEBUG=ON -DMANIFOLD_PAR=NONE -DMANIFOLD_EXPORT=ON -DCMAKE_PREFIX_PATH=${CMAKE_CURRENT_SOURCE_DIR}/vcpkg/installed/x64-windows/share -A x64 -B build
cd build
cmake --build . --target ALL_BUILD --config Release
```

\build\bin\Release\manifoldc.dll

> Debug

```shell
cmake . -DCMAKE_BUILD_TYPE=Debug -DBUILD_SHARED_LIBS=OFF -DMANIFOLD_DEBUG=ON -DMANIFOLD_PAR=NONE -DMANIFOLD_EXPORT=ON -DCMAKE_PREFIX_PATH=${CMAKE_CURRENT_SOURCE_DIR}/vcpkg/installed/x64-windows/share -A x64 -B build
cd build
cmake --build . --target ALL_BUILD --config Debug
```

\build\bin\Debug\manifoldc.dll