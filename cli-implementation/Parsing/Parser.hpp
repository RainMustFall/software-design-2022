//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_PARSER_HPP
#define CLI_PARSER_HPP

#include <iostream>
#include <vector>

#include "Defines.hpp"
#include "ReturnCode.hpp"
#include "Token.hpp"

class Parser {
public:
    static ReturnCode TryParse(std::istream& stream, OUT std::vector<Token>& tokens) {
        while(!stream.eof()) {
            if ((char) stream.peek() == ' ') {
                //processing for space character
                stream.get(); // extract space character
                tokens.emplace_back(TokenType::spaceCharToken);
            }
            if ((char) stream.peek() == '"') {
                //processing for concatenating double quotes into text stringArgToken
                std::string stringArgData;
                stream.get(); // extract first quote character
                tokens.emplace_back(TokenType::doubleQuoteCharToken);
                while((char) stream.peek() != '"') {
                    auto c = stream.get();
                    stringArgData += c;
                }
                stream.get(); // extract second quote character
                tokens.emplace_back(TokenType::stringArgToken, stringArgData);
                tokens.emplace_back(TokenType::doubleQuoteCharToken);
            } else {
                //processing for command token
                std::string stringCmdData;
                stream >> stringCmdData;
                if (!stringCmdData.empty())
                {
                    tokens.emplace_back(TokenManager::getToken(stringCmdData));
                }
            }
        }
        return ReturnCode::ok;
    }
private:
    static bool _aggregateSingleQuoteToken(std::istream& stream, OUT Token& singleQuoteToken) {

    }
};

#endif //CLI_PARSER_HPP
