//
// Created by Zeliboba on 06.03.2022.
//

#ifndef CLI_IMPLEMENTATION_EXECUTIONEXCEPTION_HPP
#define CLI_IMPLEMENTATION_EXECUTIONEXCEPTION_HPP

#include <exception>
#include <string>

/*
    Indicator of appeared error during command execution.
*/
class ExecutionException : public std::exception {
public:
    explicit ExecutionException(std::string msg) : _message(std::move(msg)) {}

    [[nodiscard]]
    const char* what() const noexcept override{
        return _message.c_str();
    }
private:
    std::string _message;
};

#endif //CLI_IMPLEMENTATION_EXECUTIONEXCEPTION_HPP
