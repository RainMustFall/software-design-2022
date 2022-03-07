//
// Created by sergbelom on 2/28/22.
//

#ifndef CLI_IMPLEMENTATION_WORDCOUNTCOMMAND_HPP
#define CLI_IMPLEMENTATION_WORDCOUNTCOMMAND_HPP

#include <fstream>
#include <sstream>
#include <string>
#include <utility>
#include <vector>
#include <algorithm>
#include <iterator>
#include "ICommand.hpp"
#include "SynchronousProcess.hpp"

namespace cli {

/*
 * Implementation of word count command.
 * Display the number of lines, words and bytes in the file.
 * */
class WordCountCommand : public ICommand {
public:
    explicit WordCountCommand(std::vector<std::string>& args) : _file_paths(args)
    { }

    IProcessPtr Execute(ExecutionContext& context) override {
        auto process = std::make_shared<SynchronousProcess>();
        for (auto & path : _file_paths) {
            std::ifstream textFile(path);
            std::string filename = path.substr(path.find_last_of("/\\") + 1);
            if (textFile.good()) {
                size_t countLines = 0;
                size_t countWords = 0;
                size_t sizeLines = 0;
                while(!textFile.eof()) {
                    std::string textLine;
                    std::getline(textFile, textLine);
                    countLines++;
                    sizeLines += textLine.length();
                    countWords += countWordsInString(textLine);;
                }
                process->GetWritableStdout() << countLines << ' ' << countWords << ' ' << sizeLines << ' ' << filename <<'\n';
            }
        }
        process->SetReturnCode(ReturnCode::ok);
        return process;
    }
private:
    std::vector<std::string> _file_paths;

    static unsigned int countWordsInString(std::string const& str)
    {
        std::stringstream stream(str);
        return std::distance(std::istream_iterator<std::string>(stream), std::istream_iterator<std::string>());
    }
};

}

#endif //CLI_IMPLEMENTATION_WORDCOUNTCOMMAND_HPP
