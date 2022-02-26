#include <sstream>
#include <cassert>

#include "Parser.hpp"
#include "ExecutionBuilder.hpp"
#include "ReturnCode.hpp"
#include "CombinedPreprocessor.hpp"
#include "DefaultConfiguration.hpp"


std::string start(const std::string& inputString, const DefaultConfiguration& configuration) {
    std::istringstream inputStream(inputString);
    std::vector<Token> tokens;
    if (!Parser::TryParse(inputStream, tokens)) {
        return "Unable to parse";
    }
    if (!configuration.GetCombinedPreprocessor()->TryPreprocess(tokens)) {
        return "Unable to preprocess";
    }
    auto executions = ExecutionBuilder::BuildExecutions(tokens);
    //5. executor
    //TODO:
    //WIP: simple test implementation for echo cmd
    if (executions[0] != nullptr && executions[0]->GetTokenType() == TokenType::echoCmdToken)
    {
        return Executor::Run(executions[0]);
    }
    return output;
}

void smoke_test() {
    DefaultConfiguration configuration;
    auto helloWorldTest = start("echo \"Hello, world!\"", configuration);
    assert(helloWorldTest == "Hello, world!");
}

int main() {
    smoke_test();
    return 0;
}
