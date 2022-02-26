//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_QUOTETOTEXTPREPROCESSOR_HPP
#define CLI_IMPLEMENTATION_QUOTETOTEXTPREPROCESSOR_HPP

#include <vector>
#include "IPreprocessor.hpp"

class QuoteToTextPreprocessor : public IPreprocessor {
public:
    std::vector<Token> Preprocess (std::vector<Token>& tokens) override {
        for (auto& token : tokens) {
            if (token.GetType() == TokenType::pairedSingleQuote || token.GetType() == TokenType::pairedDoubleQuote) {
                token.SetType(TokenType::text);
                token.SetArgument(token.GetArgument().substr(1, token.GetArgument().size() - 2));
            }
        }
        return tokens;
    }
};

#endif //CLI_IMPLEMENTATION_QUOTETOTEXTPREPROCESSOR_HPP
