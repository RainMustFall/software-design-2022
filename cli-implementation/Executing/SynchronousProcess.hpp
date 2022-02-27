//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_SYNCHRONOUSPROCESS_HPP
#define CLI_IMPLEMENTATION_SYNCHRONOUSPROCESS_HPP

#include <istream>
#include "IManageableProcess.hpp"


/*
 * Represents synchronous process with internal buffers.
 * It's only purpose is to be returned only after completed execution.
 * See 'EchoCommand' for an example of usage.
 * */
class SynchronousProcess : public IManageableProcess {
public:
    SynchronousProcess() {
        _stdout = new std::stringstream();
        _stderr = new std::stringstream();
    }

    explicit SynchronousProcess(std::iostream* outStream, std::iostream* errStream) : _stdout(outStream), _stderr(errStream) {}

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

#endif //CLI_IMPLEMENTATION_SYNCHRONOUSPROCESS_HPP
