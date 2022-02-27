//
// Created by Zeliboba on 27.02.2022.
//

#ifndef CLI_IMPLEMENTATION_CLIHANDLER_HPP
#define CLI_IMPLEMENTATION_CLIHANDLER_HPP

#include "IProcess.hpp"
#include "DefaultConfiguration.hpp"
#include "Parser.hpp"
#include "ExecutionBuilder.hpp"

// TODO: Make use of return code; Make use of clearer 'Try' statements and error handling.
std::vector<IProcess*> handle(std::istream& istream, const DefaultConfiguration& configuration) {
    auto parsed = Parser::Parse(istream); // try parse also available
    auto preprocessed = configuration.GetCombinedPreprocessor()->Preprocess(parsed); // try preprocess also available
    auto executionBuilder = ExecutionBuilder(configuration.GetCommandRegistry());
    auto executions = executionBuilder.BuildExecutions(preprocessed);
    auto executor = Executor(configuration.GetStorage());
    std::vector<IProcess*> results;
    for (auto execution : executions) {
        auto result = executor.Run(execution);
        results.push_back(result);
    }
    return results;
}

std::vector<IProcess*> handle(std::string& line, const DefaultConfiguration& configuration) {
    std::istringstream inputStream(line);
    return handle(inputStream, configuration);
}

std::vector<IProcess*> handle(std::vector<std::string>& lines, const DefaultConfiguration& configuration) {
    std::vector<IProcess*> results;
    for (auto line : lines) {
        for (auto result : handle(line, configuration)) {
            results.push_back(result);
        }
    }
    return results;
}

std::vector<std::string> handleAndExtract(std::istream& input, const DefaultConfiguration& configuration) {
    auto results = handle(input, configuration);
    std::vector<std::string> extracted;
    for (auto result : results) {
        std::ostringstream os;
        os << result->GetStdout().rdbuf();
        extracted.push_back(os.str());
    }
    return extracted;
}

std::vector<std::string> handleAndExtract(std::string& line, const DefaultConfiguration& configuration) {
    std::istringstream inputStream(line);
    return handleAndExtract(inputStream, configuration);
}

std::vector<std::string> handleAndExtract(std::vector<std::string>& lines, const DefaultConfiguration& configuration) {
    std::vector<std::string> results;
    for (auto line : lines) {
        for (const auto& result : handleAndExtract(line, configuration)) {
            results.push_back(result);
        }
    }
    return results;
}

#endif //CLI_IMPLEMENTATION_CLIHANDLER_HPP
