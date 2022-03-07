//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_DOUBLEQUOTEMERGEPREPROCESSOR_HPP
#define CLI_IMPLEMENTATION_DOUBLEQUOTEMERGEPREPROCESSOR_HPP

#include <vector>
#include "Token.hpp"
#include "PreprocessingException.hpp"
#include "IPreprocessor.hpp"


namespace cli {

/*
    Greedily tries to pair all double quotes in an input sequence.
    Throws exception if some double quote was left without a pair.
*/
class DoubleQuoteMergePreprocessor : public IPreprocessor {
public:
    std::vector<Token> Preprocess (std::vector<Token>& tokens) const override {
        std::vector<Token> newTokens;
        auto i = 0;
        while (i < tokens.size()) {
            auto cur = tokens[i++];
            if (cur.GetType() == TokenType::doubleQuote) {
                Token merged(TokenType::pairedDoubleQuote);
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
                newTokens.push_back(merged);
            }
            else {
                newTokens.push_back(cur);
            }
        }
        return newTokens;
    }
};

}

#endif //CLI_IMPLEMENTATION_DOUBLEQUOTEMERGEPREPROCESSOR_HPP
