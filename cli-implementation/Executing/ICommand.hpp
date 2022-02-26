//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_COMMAND_HPP
#define CLI_COMMAND_HPP

#include "IProcess.hpp"

class ICommand {
public:
    virtual IProcess* Execute(std::istream& input, ExecutionContext context) = 0;
};

#endif //CLI_COMMAND_HPP
