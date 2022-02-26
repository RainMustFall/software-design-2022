//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_SPACEFILTERPREPROCESSOR_HPP
#define CLI_IMPLEMENTATION_SPACEFILTERPREPROCESSOR_HPP

#include <vector>
#include "IPreprocessor.hpp"

class SubstitutionPreprocessor : public IPreprocessor {
public:
    std::vector<Token> Preprocess (std::vector<Token>& tokens) override {
        std::vector<Token> newTokens;
        for (auto token : tokens) {
            if (token.GetType() != TokenType::space)
                newTokens.push_back(token);
        }
        return newTokens;
    }
};

#endif //CLI_IMPLEMENTATION_SPACEFILTERPREPROCESSOR_HPP
