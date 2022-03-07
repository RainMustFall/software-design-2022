//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_PARSINGEXCEPTION_HPP
#define CLI_IMPLEMENTATION_PARSINGEXCEPTION_HPP

#include <exception>
#include <string>
#include <utility>

namespace cli {

/*
    Indicator of appeared error during parsing.
*/
class ParsingException : public std::exception {
public:
    explicit ParsingException(std::string msg) : _message(std::move(msg)) {}

    [[nodiscard]]
    const char* what() const noexcept override{
        return _message.c_str();
    }
private:
    std::string _message;
};

}

#endif //CLI_IMPLEMENTATION_PARSINGEXCEPTION_HPP
