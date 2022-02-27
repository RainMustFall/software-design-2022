//
// Created by Zeliboba on 27.02.2022.
//

#include <gtest/gtest.h>

#include <utility>
#include "CliHandler.hpp"

std::vector<IProcess*> RunPipeline(std::vector<std::string> lines) {
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

TEST(PipelineTest, EmptyInput) {
    ShouldBe({""}, {""});
}


// TODO: Add more scenarios...
