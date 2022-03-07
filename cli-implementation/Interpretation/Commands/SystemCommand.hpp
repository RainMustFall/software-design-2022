//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_SYSTEMCOMMAND_HPP
#define CLI_IMPLEMENTATION_SYSTEMCOMMAND_HPP

#include <fstream>
#include <vector>

#include "Auxillary/TempFile.hpp"
#include "ICommand.hpp"
#include "SynchronousProcess.hpp"

namespace cli {
/*
 * Starts the specified child process
 * */
class SystemCommand : public ICommand {
 public:
    explicit SystemCommand(const std::string& command,
                           const std::vector<std::string>& arguments)
        : _command_line(AccumulateCommand(command, arguments)) {}

    IProcessPtr Execute(ExecutionContext& context) override {
        auto process = std::make_shared<SynchronousProcess>();
        TempFile stdoutFile;
        TempFile stderrFile;
        TempFile stdinFile;

        std::ofstream(stdinFile.GetName()) << context.GetStdin().rdbuf();

        // XXX: std::system makes it impossible to collect program output normally,
        // and other solutions require either using boost or using different
        // system calls for *NIX and Windows
        int result = std::system(
            (_command_line
                + " >" + stdoutFile.GetName()
                + " 2>" + stderrFile.GetName()
                + " <" + stdinFile.GetName())
                .c_str());

        PrintFile(stdoutFile.GetName(), process->GetWritableStdout());
        PrintFile(stderrFile.GetName(), process->GetWritableStderr());
        process->SetReturnCode(
            result == 0 ? ReturnCode::ok : ReturnCode::executionError);
        return process;
    }

 private:
    std::string _command_line;

    static std::string
    AccumulateCommand(const std::string& command,
                      const std::vector<std::string>& arguments) {
        std::string result = command + " ";
        for (const auto& token: arguments) {
            result += token + " ";
        }
        return result;
    }

    static void PrintFile(const std::string& filename, std::ostream& out) {
        std::ifstream file(filename);
        out << file.rdbuf();
    }
};

}

#endif // CLI_IMPLEMENTATION_SYSTEMCOMMAND_HPP
