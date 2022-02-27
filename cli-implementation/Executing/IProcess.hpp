//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_PROCESS_HPP
#define CLI_PROCESS_HPP

#include <memory>

#include "ReturnCode.hpp"

/*
 * Represents readonly side of a process: getting return codes and input streams.
 * */
class IProcess {
public:
    virtual ReturnCode GetReturnCode() = 0;
    virtual std::istream& GetStdout() = 0;
    virtual std::istream& GetStderr() = 0;
};

typedef std::shared_ptr<IProcess> IProcessPtr;

#endif //CLI_PROCESS_HPP
