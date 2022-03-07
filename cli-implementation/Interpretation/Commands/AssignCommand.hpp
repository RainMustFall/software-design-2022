//
// Created by Zeliboba on 27.02.2022.
//

#ifndef CLI_IMPLEMENTATION_ASSIGNCOMMAND_HPP
#define CLI_IMPLEMENTATION_ASSIGNCOMMAND_HPP

#include <vector>
#include "ICommand.hpp"
#include "SynchronousProcess.hpp"


namespace cli {

/*
 * Assigns a value to a key in the storage of the context.
 * */
class AssignCommand : public ICommand {
public:
    explicit AssignCommand(std::vector<std::string>& args) {
        // TODO: Exception may occur during building of command instead of during the execution. Fix.
        key = args[0];
        value = args[1];
    }

    IProcessPtr Execute(ExecutionContext& context) override {
        auto process = std::make_shared<SynchronousProcess>();
        context.GetStorage().Set(key, value);
        process->SetReturnCode(ReturnCode::ok);
        return process;
    }
private:
    std::string key;
    std::string value;
};

}

#endif //CLI_IMPLEMENTATION_ASSIGNCOMMAND_HPP
