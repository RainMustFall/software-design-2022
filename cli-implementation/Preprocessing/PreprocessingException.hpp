//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_PREPROCESSINGEXCEPTION_HPP
#define CLI_IMPLEMENTATION_PREPROCESSINGEXCEPTION_HPP

#include <exception>
#include <string>

/*
    Indicator of appeared error during preprocessing.
*/
class PreprocessingException : public std::exception {
public:
    explicit PreprocessingException(std::string msg) : _message(std::move(msg)) {}

    [[nodiscard]]
    const char* what() const noexcept override{
        return _message.c_str();
    }
private:
    std::string _message;
};

#endif //CLI_IMPLEMENTATION_PREPROCESSINGEXCEPTION_HPP
