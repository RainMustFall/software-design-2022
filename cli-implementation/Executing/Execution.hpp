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

class Execution {
public:
    explicit Execution(std::vector<ICommand*> commands) : _commands(std::move(commands)) { }
private:
    std::vector<ICommand*> _commands;

    friend class Executor;
};

#include <utility>

#endif //CLI_IMPLEMENTATION_EXECUTION_HPP
