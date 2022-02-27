//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_COMMAND_HPP
#define CLI_COMMAND_HPP

#include "IProcess.hpp"
#include "ExecutionContext.hpp"

/*
    Abstracts a command execution. 
    Every inheritor is responsible for creating an 'IProcess' instance as well as pass stdout/stderr.
*/
class ICommand {
public:
    virtual IProcess* Execute(ExecutionContext& context) = 0;
};

#endif //CLI_COMMAND_HPP
