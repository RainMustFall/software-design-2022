#include <sstream>
#include <cassert>

#include "Parser.hpp"
#include "Interpretation/ExecutionBuilder.hpp"
#include "ReturnCode.hpp"
#include "CombinedPreprocessor.hpp"
#include "DefaultConfiguration.hpp"


std::string handle(const std::string& inputString, const DefaultConfiguration& configuration) {
    std::istringstream inputStream(inputString);
    std::vector<Token> tokens;
    auto parsed = Parser::Parse(inputStream);
//    if (!Parser::TryParse(inputStream, tokens)) {
//        return "Unable to parse";
//    }
    auto preprocessed = configuration.GetCombinedPreprocessor()->Preprocess(parsed);
//    if (!configuration.GetCombinedPreprocessor()->TryPreprocess(tokens)) {
//        return "Unable to preprocess";
//    }
    auto executionBuilder = ExecutionBuilder(configuration.GetCommandRegistry());
    auto executions = executionBuilder.BuildExecutions(preprocessed);
    auto executor = Executor(configuration.GetStorage());
    std::string output; // TODO: Temporary stub
    for (auto execution : executions) {
        auto result = executor.Run(execution);
        std::ostringstream os;
        os << result->GetStdout().rdbuf();
        output += os.str();
    }
    return output;
}

void smoke_test() {
    DefaultConfiguration configuration;
    auto helloWorldTest = handle("echo \"Hello, world!\"", configuration);
    assert(helloWorldTest == "Hello, world!");
}

void start_cli() {
    DefaultConfiguration configuration;
    std::string line;
    std::getline(std::cin, line);
    while (line != "exit") {
        try{
            std::cout << handle(line, configuration) << std::endl;
        }
        catch (std::exception& err) {
            auto what = err.what();
            std::cerr << what << std::endl;
        }
        std::getline(std::cin, line);
    }
}

int main() {
    start_cli();
    return 0;
}
