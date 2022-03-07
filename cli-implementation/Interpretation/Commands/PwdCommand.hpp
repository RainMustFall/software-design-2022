//
// Created by sergbelom on 2/28/22.
//

#ifndef CLI_IMPLEMENTATION_PWDCOMMAND_HPP
#define CLI_IMPLEMENTATION_PWDCOMMAND_HPP

#include <vector>
#include <cstring>
#include <filesystem>
#include "ICommand.hpp"
#include "SynchronousProcess.hpp"

/*
 * Implementation of pwd command.
 * Display the present working directory.
 * */
class PwdCommand : public ICommand {
public:
    explicit PwdCommand(std::vector<std::string>& args)
    { }

    IProcessPtr Execute(ExecutionContext& context) override {
        auto process = std::make_shared<SynchronousProcess>();
        process->GetWritableStdout() << std::filesystem::current_path().string() <<'\n';
        process->SetReturnCode(ReturnCode::ok);
        return process;
    }
private:
};

#endif //CLI_IMPLEMENTATION_PWDCOMMAND_HPP
