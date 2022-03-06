//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_EXECUTION_HPP
#define CLI_IMPLEMENTATION_EXECUTION_HPP

#include <utility>
#include <vector>
#include "Token.hpp"
#include "ICommand.hpp"
#include "ExecutionContext.hpp"
#include "ExecutionEdge.hpp"

/*
 * Represents a container for commands and interactions between them.
 * Its only purpose is to be used by the Executor.
 * */
class Execution {
 public:
    explicit Execution(std::vector<ICommandPtr> commands, std::vector<ExecutionEdge> edges)
        : _commands(std::move(commands)), _edges(std::move(edges)) {}

 private:
    std::vector<ICommandPtr> _commands;
    std::vector<ExecutionEdge> _edges;

    friend class Executor;
};

#include <utility>

#endif //CLI_IMPLEMENTATION_EXECUTION_HPP
