#include <sstream>
#include <cassert>

#include "Parser.hpp"
#include "ExecutionBuilder.hpp"
#include "ReturnCode.h"

std::string start(const std::string& inputString) {
    std::string output;
    //1. input
    std::istringstream inputStream(inputString);
    //2. parsing
    std::vector<Token> tokens;
    auto parseResultCode = Parser::TryParse(inputStream, tokens);
    if (parseResultCode == ReturnCode::ok)
    {
        //3. preprocessor
        //TODO:
        //4. execution builder
        auto executions = ExecutionBuilder::BuildExecutions(tokens);
        //5. executor
        //TODO:
        //WIP: simple test implementation for echo cmd
        if (executions[0] != nullptr && executions[0]->GetTokenType() == TokenType::echoCmdToken)
        {
            return Executor::Run(executions[0]);
        }
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
