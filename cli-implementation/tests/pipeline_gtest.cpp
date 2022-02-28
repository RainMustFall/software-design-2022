//
// Created by Zeliboba on 27.02.2022.
//

#include <gtest/gtest.h>
#include <utility>
#include "CliHandler.hpp"
#include <cstring>

std::vector<IProcessPtr> RunPipeline(std::vector<std::string> lines) {
    DefaultConfiguration configuration;
    return handle(lines, configuration);
}

std::vector<std::string> RunPipelineAndExtract(std::vector<std::string> lines) {
    DefaultConfiguration configuration;
    std::vector<std::string> stdout_only;
    stdout_only.reserve(lines.size());
    for (const auto&[std_out, std_err]:
        handleAndExtract(lines, configuration)) {
        stdout_only.push_back(std_out);
    }
    return stdout_only;
}

void ShouldBe(std::vector<std::string> input, std::vector<std::string> output) {
    auto results = RunPipelineAndExtract(std::move(input));
    EXPECT_EQ(results.size(), output.size());
    for (auto i = 0; i < results.size(); ++i) {
        EXPECT_EQ(results[i], output[i]);
    }
}

TEST(PipelineTest, SimpleCommand) {
    ShouldBe({"echo hello"}, {"hello"});
}

TEST(PipelineTest, Substitution) {
    ShouldBe({"X=Y", "echo $X"}, {"", "Y"});
}

TEST(PipelineTest, ComplexSubstitution) {
    ShouldBe({"X=echo", "Y=hello", "$X $Y"}, {"", "", "hello"});
}

TEST(PipelineTest, DoubleQuotes) {
    ShouldBe({"echo \"Hello, world!\""}, {"Hello, world!"});
}

TEST(PipelineTest, DoubleQuotesWithSubstitution) {
    ShouldBe({"X=\"Hello, world!\"", "echo $X"}, {"", "Hello, world!"});
}

TEST(PipelineTest, UnknownCommandsDontFall) {
    ASSERT_NO_THROW(RunPipelineAndExtract({"gcc", "dimon", "kek"}));
}

TEST(PipelineTest, EmptyInput) {
    ShouldBe({""}, {""});
}

void testCommandOnFiles(const std::string & command, const std::string & correctAnswer) {
    // get the path of the current project use /proc/self/exe and look for test files
    char buff[1000];
    size_t length = ::readlink("/proc/self/exe", buff, sizeof(buff)-1);
    if (length != -1) {
        buff[length] = '\0';
        std::string self_path(buff);
        auto cli_project_path = self_path.substr(0, self_path.find("/cmake"));
        std::ifstream test_file(cli_project_path + "/tests/test_file_1.txt");
        if (test_file.good()) {
            auto input = command + ' ' + cli_project_path + "/tests/test_file_1.txt " + cli_project_path + "/tests/test_file_2.txt";
            ShouldBe({input}, {correctAnswer});
        }
    }
}

TEST(PipelineTest, CatCommand) {
    testCommandOnFiles("cat","test1 data line 1\ntest1 data line 2\ntest2 data line 1\ntest2 data line 2\n");
}

TEST(PipelineTest, WordCountCommand) {
    testCommandOnFiles("wc","2 8 34 test_file_1.txt\n2 8 34 test_file_2.txt\n");
}

TEST(PipelineTest, PwdCommand) {
    std::vector<std::string>input {"pwd"};
    auto results = RunPipelineAndExtract(input);
}

// TODO: Add more scenarios...
