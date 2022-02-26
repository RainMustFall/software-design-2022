//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_PROCESS_HPP
#define CLI_PROCESS_HPP

#include "ReturnCode.hpp"

class IProcess {
public:
    virtual ReturnCode GetReturnCode() = 0;
    virtual std::ostream& GetStdout() = 0;
    virtual std::ostream& GetStderr() = 0;
};

#endif //CLI_PROCESS_HPP
