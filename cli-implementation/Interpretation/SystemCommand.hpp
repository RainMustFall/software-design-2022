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

/*
 * Starts the specified child process
 * */
class SystemCommand : public ICommand {
public:
  explicit SystemCommand(const std::string &command,
                         const std::vector<std::string> &arguments)
      : _command_line(AccumulateCommand(command, arguments)) {}

  IProcess *Execute(ExecutionContext &context) override {
    auto process = new SynchronousProcess();
    TempFile stdoutFile;
    TempFile stderrFile;

    // XXX: std::system makes it impossible to collect program output normally,
    // and other solutions require either using boost or using different
    // system calls for *NIX and Windows
    std::system(
        (_command_line + ">" + stdoutFile.GetName() + " 2>" + stderrFile.GetName())
            .c_str());

    PrintFile(stdoutFile.GetName(), process->GetWritableStdout());
    PrintFile(stderrFile.GetName(), process->GetWritableStderr());
    return process;
  }

private:
  std::string _command_line;

  std::string
  AccumulateCommand(const std::string &command,
                    const std::vector<std::string> &arguments) const {
    std::string result = command + " ";
    for (const auto &token : arguments) {
      result += token + " ";
    }
    return result;
  }

  void PrintFile(const std::string &filename, std::ostream &out) {
    std::ifstream file(filename);
    out << file.rdbuf();
  }
};

#endif // CLI_IMPLEMENTATION_SYSTEMCOMMAND_HPP
