//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_SYNCPROCESS_HPP
#define CLI_IMPLEMENTATION_SYNCPROCESS_HPP

#include <istream>
#include "IManageableProcess.hpp"

class SyncProcess : public IManageableProcess {
public:
    SyncProcess() {
        _stdout = new std::stringstream();
        _stderr = new std::stringstream();
    }

    explicit SyncProcess(std::iostream* outStream, std::iostream* errStream) : _stdout(outStream), _stderr(errStream) {}

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

    std::ostream& GetWritableStdout() override {
        return *_stdout;
    }

    std::ostream& GetWritableStderr() override {
        return *_stderr;
    }
private:
    ReturnCode _returnCode = ReturnCode::notFinishedYet;
    std::iostream* _stdout;
    std::iostream* _stderr;
};

#endif //CLI_IMPLEMENTATION_SYNCPROCESS_HPP
