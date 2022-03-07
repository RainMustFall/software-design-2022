# Реализация СLI

В этой директории находится реализация СLI, выполненная в рамках курса 
"Проектирование программного обеспечения" в Школе анализа данных.
                                                                       
## Как собрать

### Linux

Необходим CMake версии 3.16 и выше, а также GNU C++ компилятор с поддержкой 
C++17.

В папке `cli-implementation` выполните команды

```
mkdir build
cmake -B build
cmake --build build
```

Для запуска выполните

```
build/cli
```

### Windows

Необходим CMake версии 3.16 и выше, а также компилятор MinGW с поддержкой 
C++17.

В папке `cli-implementation` выполните команды

```
mkdir build
cmake -B build -G "MinGW Makefiles"
cmake --build build --target cli
```

Подозреваю, что Windows не даст вам так просто собрать с первого раза, 
поэтому вот несколько подводных камней, на которые можно наткнуться.

[CMake пытается компилировать не MinGW, а вижлой](https://stackoverflow.com/questions/45933732/how-to-specify-a-compiler-in-cmake)

[Not able to compile a simple test program](https://stackoverflow.com/questions/59355908/mingw-c-compiler-not-able-to-compile-a-simple-test-program)

[Не компилится GTest: 'AutoHandle' does not name a type](https://github.com/google/googletest/issues/606)

[sed: command not found при попытке забрать GTest с github](https://github.com/desktop/desktop/issues/10425)


Если вы прошли этот путь и не сошли с ума, то для запуска выполните

```
build\cli.exe
```
