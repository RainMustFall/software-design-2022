//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_SYNCPROCESS_HPP
#define CLI_IMPLEMENTATION_SYNCPROCESS_HPP

#include <istream>
#include "IManageableProcess.hpp"

class SyncProcess : public IManageableProcess {
public:
    ReturnCode GetReturnCode() override {
        return _returnCode;
    }

    std::istream& GetStdout() override {
        return *_stdout;
    }

    std::istream& GetStderr() override {
        return *_stderr;
    }

    void SetReturnCode(ReturnCode returnCode) override {
        _returnCode = returnCode;
    }

    void SetStdout(std::istream& istream) override {
        _stdout = &istream;
    }

    void SetStderr(std::istream& istream) override {
        _stderr = &istream;
    }
private:
    ReturnCode _returnCode = ReturnCode::notFinishedYet;
    std::istream* _stdout = nullptr;
    std::istream* _stderr = nullptr;
};

#endif //CLI_IMPLEMENTATION_SYNCPROCESS_HPP
