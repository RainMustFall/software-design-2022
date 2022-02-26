//
// Created by sergbelom on 2/21/22.
//
#ifndef CLI_EXECUTIONBUILDER_HPP
#define CLI_EXECUTIONBUILDER_HPP

#include <vector>
#include "Executor.hpp"
#include "Token.hpp"
#include "ICommand.hpp"
#include "IProcess.hpp"
#include "Execution.hpp"
#include "InterpretationException.hpp"
#include "CommandRegistry.hpp"


class ExecutionBuilder {
public:
    explicit ExecutionBuilder(CommandRegistry* registry) : _registry(registry) {}

    std::vector<Execution> BuildExecutions (const std::vector<Token> & tokens) {
        std::vector<Execution> executions;
        std::vector<ICommand*> currentCommands;
        auto i = 0;
        while (i < tokens.size()) {
            auto command = tokens[i++];
            if (command.GetType() != TokenType::text)
                throw InterpretationException("Expected text token, received token type of id " + std::to_string(command.GetType()));
            std::vector<std::string> args;
            while (i < tokens.size()) {
                auto cur = tokens[i];
                if (cur.GetType() == TokenType::_operator) {
                    // TODO: Add edge
                    // TODO: Stop building current execution and begin a next one if faced parallel operator (&)
                    break;
                }
                args.push_back(cur.GetArgument());
            }
            currentCommands.push_back(_registry->Build(command.GetArgument(), args));
        }
        executions.emplace_back(currentCommands);
        return executions;
    }
private:
    CommandRegistry* _registry;
};

#endif //CLI_EXECUTIONBUILDER_HPP
