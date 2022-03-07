//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_ECHOCOMMAND_HPP
#define CLI_IMPLEMENTATION_ECHOCOMMAND_HPP

#include <vector>
#include "ICommand.hpp"
#include "SynchronousProcess.hpp"

namespace cli {

/*
 * Concatenates given arguments and returns them.
 * */
class EchoCommand : public ICommand {
public:
    explicit EchoCommand(const std::vector<std::string>& arguments) {
        for (const auto& arg : arguments) {
            _toEcho += arg;
        }
    }

    IProcessPtr Execute(ExecutionContext& context) override {
        auto process = std::make_shared<SynchronousProcess>();
        process->GetWritableStdout() << _toEcho;
        process->SetReturnCode(ReturnCode::ok);
        return process;
    }
private:
    std::string _toEcho;
};

}

#endif //CLI_IMPLEMENTATION_ECHOCOMMAND_HPP
