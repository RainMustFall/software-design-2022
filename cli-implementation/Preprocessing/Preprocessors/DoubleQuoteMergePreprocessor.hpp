//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_DOUBLEQUOTEMERGEPREPROCESSOR_HPP
#define CLI_IMPLEMENTATION_DOUBLEQUOTEMERGEPREPROCESSOR_HPP

#include <vector>
#include "Token.hpp"
#include "PreprocessingException.hpp"
#include "IPreprocessor.hpp"

class DoubleQuoteMergePreprocessor : public IPreprocessor {
public:
    std::vector<Token> Preprocess (std::vector<Token>& tokens) override {
        std::vector<Token> newTokens;
        auto i = 0;
        while (i < tokens.size()) {
            auto cur = tokens[i++];
            if (cur.GetType() == TokenType::doubleQuote) {
                Token merged(TokenType::text);
                std::string mergedArgument;
                auto closed = false;
                while (i < tokens.size()) {
                    cur = tokens[i++];
                    if (cur.GetType() == TokenType::doubleQuote) {
                        merged.SetArgument("\"" + mergedArgument + "\"");
                        closed = true;
                        break;
                    }
                    mergedArgument += cur.GetArgument();
                }
                if (!closed)
                    throw PreprocessingException("Unable to close double quotes");
            }
            else {
                newTokens.push_back(cur);
            }
        }
        return newTokens;
    }
};

#endif //CLI_IMPLEMENTATION_DOUBLEQUOTEMERGEPREPROCESSOR_HPP
