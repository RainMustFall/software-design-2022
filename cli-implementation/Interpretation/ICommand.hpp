//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_COMMAND_HPP
#define CLI_COMMAND_HPP

#include <memory>
#include "IProcess.hpp"
#include "ExecutionContext.hpp"

namespace cli {

/*
    Abstracts a command execution. 
    Every inheritor is responsible for creating an 'IProcess' instance as well as pass stdout/stderr.
*/
class ICommand {
public:
    virtual IProcessPtr Execute(ExecutionContext& context) = 0;
};

typedef std::shared_ptr<ICommand> ICommandPtr;

}

#endif //CLI_COMMAND_HPP
