//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_SYNCHRONOUSPROCESS_HPP
#define CLI_IMPLEMENTATION_SYNCHRONOUSPROCESS_HPP

#include <istream>
#include <utility>
#include "IManageableProcess.hpp"

namespace cli {

/*
 * Represents synchronous process with internal buffers.
 * It's only purpose is to be returned only after completed execution.
 * See 'EchoCommand' for an example of usage.
 * */
class SynchronousProcess : public IManageableProcess {
 public:
    SynchronousProcess()
        : _stdout(std::make_shared<std::stringstream>()),
          _stderr(std::make_shared<std::stringstream>()) {
    }

    explicit SynchronousProcess(std::shared_ptr<std::iostream> outStream,
                                std::shared_ptr<std::iostream> errStream)
        : _stdout(std::move(outStream)), _stderr(std::move(errStream)) {}

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
    std::shared_ptr<std::iostream> _stdout;
    std::shared_ptr<std::iostream> _stderr;
};

}

#endif //CLI_IMPLEMENTATION_SYNCHRONOUSPROCESS_HPP
