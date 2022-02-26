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

include_directories(
        Executing
        Parsing
        Preprocessing
        Preprocessing/Preprocessors
        Auxillary
)

add_executable(test_gtest tests/test_gtest.cpp)
add_executable(cli cli.cpp Auxillary/Defines.hpp Auxillary/IStorage.hpp Parsing/ParsingException.hpp Preprocessing/DefaultPreprocessor.hpp Preprocessing/Preprocessors/SubstitutionPreprocessor.hpp Auxillary/Storages/InMemoryStorage.hpp Preprocessing/PreprocessingException.hpp Preprocessing/Preprocessors/DoubleQuoteMergePreprocessor.hpp Preprocessing/Preprocessors/SpaceFilterPreprocessor.hpp Preprocessing/Preprocessors/AssignmentReorderPreprocessor.hpp)

target_link_libraries(
        test_gtest
        gtest_main
)

include(GoogleTest)
gtest_discover_tests(test_gtest)