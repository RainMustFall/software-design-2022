//
// Created by Zeliboba on 27.02.2022.
//

#ifndef CLI_IMPLEMENTATION_ASSIGNCOMMAND_HPP
#define CLI_IMPLEMENTATION_ASSIGNCOMMAND_HPP

#include <vector>
#include "ICommand.hpp"
#include "SyncProcess.hpp"

class AssignCommand : public ICommand {
public:
    explicit AssignCommand(std::vector<std::string>& args) {
        key = args[0];
        value = args[1];
    }

    IProcess* Execute(ExecutionContext& context) override {
        auto process = new SyncProcess();
        context.GetStorage()->Set(key, value);
        process->SetReturnCode(ReturnCode::ok);
        return process;
    }
private:
    std::string key;
    std::string value;
};

#endif //CLI_IMPLEMENTATION_ASSIGNCOMMAND_HPP
