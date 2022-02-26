//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_TOKEN_MANAGER
#define CLI_TOKEN_MANAGER

#include <map>
#include <list>
#include <utility>

enum TokenType {
    undefined,
    text,
    substitute,
    assignment,
    _operator,
    pairedSingleQuote,
    doubleQuote,
    pairedDoubleQuote,
    space
};

class Token {
public:
    explicit Token() : _tokenType(TokenType::undefined) {}

    explicit Token(TokenType tokenType) : _tokenType(tokenType)
    {}

    Token(TokenType tokenType, std::string argument) : _tokenType(tokenType), _argument(std::move(argument))
    {}

    TokenType GetType() {
        return _tokenType;
    }

    std::string GetArgument() {
        return _argument;
    }

    void SetType(TokenType newType) {
        _tokenType = newType;
    }

    void SetArgument(std::string newArg) {
        _argument = newArg;
    }

    Token& operator=(const TokenType other) {
        _tokenType = other;
        _argument = "";
        return *this;
    }

private:
    TokenType _tokenType;
    std::string  _argument;
};

#endif //CLI_TOKEN_MANAGER
