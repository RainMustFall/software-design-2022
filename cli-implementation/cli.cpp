#include <sstream>
#include <cassert>

#include "Parser.hpp"
#include "ExecutionBuilder.hpp"
#include "ReturnCode.hpp"
#include "DefaultPreprocessor.hpp"

std::string start(const std::string& inputString) {
    std::istringstream inputStream(inputString);
    std::vector<Token> tokens;
    if (!Parser::TryParse(inputStream, tokens)) {
        return "Unable to parse";
    }
    if (!DefaultPreprocessor::TryPreprocess(tokens)) {
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
    auto helloWorldTest = start("echo \"Hello, world!\"");
    assert(helloWorldTest == "Hello, world!");
}

int main() {
    smoke_test();
    return 0;
}
