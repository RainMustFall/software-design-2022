//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_INTERPRETATIONEXCEPTION_HPP
#define CLI_IMPLEMENTATION_INTERPRETATIONEXCEPTION_HPP

#include <exception>
#include <string>

class InterpretationException : public std::exception {
public:
    explicit InterpretationException(std::string msg) : _message(std::move(msg)) {}

    [[nodiscard]]
    const char* what() const noexcept override{
        return _message.c_str();
    }
private:
    std::string _message;
};

#endif //CLI_IMPLEMENTATION_INTERPRETATIONEXCEPTION_HPP
