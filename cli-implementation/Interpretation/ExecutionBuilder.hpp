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

/*
    Transforms a sequence of tokens into a list of executions.
*/
class ExecutionBuilder {
 public:
    explicit ExecutionBuilder(const CommandRegistry& registry)
        : _registry(registry) {}

    std::vector<Execution> BuildExecutions(const std::vector<Token>& tokens) {
        std::vector<Execution> executions;
        std::vector<ICommandPtr> currentCommands;
        std::vector<ExecutionEdge> currentEdges;
        auto i = 0;
        while (i < tokens.size()) {
            auto command = tokens[i++];
            if (command.GetType() != TokenType::text)
                throw InterpretationException(
                    "Expected text token, received token type of id "
                        + std::to_string(command.GetType()));
            std::vector<std::string> args;
            auto addedEdge = false;
            while (i < tokens.size()) {
                auto cur = tokens[i++];
                if (cur.GetType() == TokenType::_operator) {
                    if (cur.GetArgument() == "|") {
                        currentEdges.push_back(ExecutionEdge::piping);
                    }
                    else if (cur.GetArgument() == ";") {
                        currentEdges.push_back(ExecutionEdge::ignoring);
                    }
                    else {
                        throw InterpretationException("Received unsupported type of operator: " + cur.GetArgument());
                    }
                    addedEdge = true;
                    // TODO: Stop building current execution and begin a next one if faced parallel operator (&)
                    break;
                }
                args.push_back(cur.GetArgument());
            }
            currentCommands.push_back(
                _registry.Build(command.GetArgument(), args));
            if (!addedEdge) {
                currentEdges.push_back(ExecutionEdge::ignoring);
            }
        }
        executions.emplace_back(currentCommands, currentEdges);
        return executions;
    }
 private:
    const CommandRegistry& _registry;
};

#endif //CLI_EXECUTIONBUILDER_HPP
