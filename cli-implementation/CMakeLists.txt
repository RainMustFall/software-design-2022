cmake_minimum_required(VERSION 3.20)
project(cli_implementation)

set(CMAKE_CXX_STANDARD 20)

include(FetchContent)

FetchContent_Declare(
        googletest
        GIT_REPOSITORY https://github.com/google/googletest.git
        GIT_TAG        release-1.8.0
)

FetchContent_GetProperties(googletest)
if(NOT googletest_POPULATED)
    FetchContent_Populate(googletest)
    add_subdirectory(${googletest_SOURCE_DIR} ${googletest_BINARY_DIR})
endif()

enable_testing()

add_executable(
        test_gtest
        tests/test_gtest.cpp
        main.cpp
        Parser.hpp
        TokenManager.hpp
        ReturnCode.h
        Preprocessor.hpp
        ExecutionBuilder.hpp
        Executor.hpp
        Command.hpp
        Process.hpp
)

target_link_libraries(
        test_gtest
        gtest_main
)

include(GoogleTest)
gtest_discover_tests(test_gtest)