//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_ECHOCOMMAND_HPP
#define CLI_IMPLEMENTATION_ECHOCOMMAND_HPP

#include <vector>
#include "ICommand.hpp"
#include "SynchronousProcess.hpp"

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
        _flush(context.GetStdin(), process->GetWritableStdout());
        if (!_toEcho.empty()) {
            process->GetWritableStdout() << _toEcho << std::endl;
        }
        process->SetReturnCode(ReturnCode::ok);
        return process;
    }
private:
    std::string _toEcho;

    static void _flush(std::istream& from, std::ostream& to){
        int ch;
        while ((ch = from.get()) != EOF) {
            to << (char) ch;
        }
    }
};

#endif //CLI_IMPLEMENTATION_ECHOCOMMAND_HPP
