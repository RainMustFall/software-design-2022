//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_IMANAGEABLEPROCESS_HPP
#define CLI_IMPLEMENTATION_IMANAGEABLEPROCESS_HPP

#include "IProcess.hpp"

class IManageableProcess : public IProcess {
public:
    virtual void SetReturnCode(ReturnCode returnCode) = 0;
    virtual void SetStdout(std::istream& istream) = 0;
    virtual void SetStderr(std::istream& istream) = 0;
};

#endif //CLI_IMPLEMENTATION_IMANAGEABLEPROCESS_HPP
