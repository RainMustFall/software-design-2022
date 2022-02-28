//
// Created by sergbelom on 2/28/22.
//

#ifndef CLI_IMPLEMENTATION_PWDCOMMAND_HPP
#define CLI_IMPLEMENTATION_PWDCOMMAND_HPP

#include <vector>
#include <cstring>
#include <unistd.h>
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
        char buffer[1000];
        size_t length = ::readlink("/proc/self/exe", buffer, sizeof(buffer)-1);
        if (length != -1) {
            buffer[length] = '\0';
            std::string selfPath(buffer);
            process->GetWritableStdout() << selfPath <<'\n';
        }
        process->SetReturnCode(ReturnCode::ok);
        return process;
    }
private:
};

#endif //CLI_IMPLEMENTATION_PWDCOMMAND_HPP
