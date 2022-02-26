//
// Created by sergbelom on 2/21/22.
//
#ifndef CLI_EXECUTIONBUILDER_HPP
#define CLI_EXECUTIONBUILDER_HPP

#include <vector>
#include "Executor.hpp"
#include "Token.hpp"

class ExecutionBuilder {
public:
    static std::vector<Execution*> BuildExecutions (const std::vector<Token> & tokens) {
        std::vector<Execution*> executions;
        Execution * execution = nullptr;
        //WIP: simple test implementation for echo cmd
        for (std::size_t i = 0; i < tokens.size(); ++i) {
            auto token = tokens[i];
            if (token.GetType() == TokenType::echoCmdToken) {
                execution = new Execution(token);
            }
            if (token.GetType() == TokenType::stringArgToken) {
                if (execution != nullptr) {
                    execution->SetExecutionContext(token.GetArgument());
                }
            }
            // add execution if operator or end of tokens
            if (token.GetType() == TokenType::pipelineOperatorToken || i == tokens.size() - 1) {
                if (execution != nullptr) {
                    executions.push_back(execution);
                }
            }
        }
        return executions;
    }
};

#endif //CLI_EXECUTIONBUILDER_HPP
