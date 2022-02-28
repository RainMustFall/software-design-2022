//
// Created by sergbelom on 2/28/22.
//

#ifndef CLI_IMPLEMENTATION_CATCOMMAND_HPP
#define CLI_IMPLEMENTATION_CATCOMMAND_HPP

#include <fstream>
#include <sstream>
#include <string>
#include <utility>
#include <vector>
#include "ICommand.hpp"
#include "SynchronousProcess.hpp"

/*
 * implementation of command to display the content of file
 * */
class CatCommand : public ICommand {
public:
    explicit CatCommand(std::vector<std::string>& args) : _file_paths(args)
    { }

    IProcessPtr Execute(ExecutionContext& context) override {
        auto process = std::make_shared<SynchronousProcess>();
        for (auto & path : _file_paths) {
                std::ifstream textFile(path);
                if (textFile.good()) {
                    while(!textFile.eof()) {
                        std::string textLine;
                        std::getline(textFile, textLine);
                        process->GetWritableStdout() << textLine << '\n';
                    }
                }
        }
        process->SetReturnCode(ReturnCode::ok);
        return process;
    }
private:
    std::vector<std::string> _file_paths;
};

#endif //CLI_IMPLEMENTATION_CATCOMMAND_HPP
