#include <sstream>
#include <cassert>

#include "Parser.hpp"
#include "Interpretation/ExecutionBuilder.hpp"
#include "DefaultConfiguration.hpp"

// TODO: Make use of return code; Make use of clearer 'Try' statements and error handling.
std::string handle(const std::string& inputString, const DefaultConfiguration& configuration) {
    std::istringstream inputStream(inputString);
    auto parsed = Parser::Parse(inputStream); // try parse also available
    auto preprocessed = configuration.GetCombinedPreprocessor()->Preprocess(parsed); // try preprocess also available
    auto executionBuilder = ExecutionBuilder(configuration.GetCommandRegistry());
    auto executions = executionBuilder.BuildExecutions(preprocessed);
    auto executor = Executor(configuration.GetStorage());
    std::string output; // TODO: Temporary stub for testing purposes, should instead redirect stdout and stderr directly to cout and cerr
    for (auto execution : executions) {
        auto result = executor.Run(execution);
        std::ostringstream os;
        os << result->GetStdout().rdbuf();
        output += os.str();
    }
    return output;
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
