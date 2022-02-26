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
    space
};

class Token {
public:
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

private:
    TokenType _tokenType;
    std::string  _argument;
};

// TODO: Remove?
//
//class TokenManager {
//    using TokenMap = std::map<std::string, TokenType>;
//
//public:
//    TokenManager() = default;;
//
//    static TokenType getToken(std::string & tokenStringRepresentation) {
//        return tokenMap[tokenStringRepresentation];
//    }
//
//private:
//    static TokenMap tokenMap;
//};
//
//TokenManager::TokenMap TokenManager::tokenMap({{"echo", echoCmdToken},
//                                                 {"cat", catCmdToken},
//                                                 {"wc", wcCmdToken},
//                                                 {"pwd", pwdCmdToken},
//                                                 {"exit", exitCmdToken},
//                                                 {"|", pipelineOperatorToken}});

#endif //CLI_TOKEN_MANAGER
