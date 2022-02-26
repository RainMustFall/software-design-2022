//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_ASSIGNMENTREORDERPREPROCESSOR_HPP
#define CLI_IMPLEMENTATION_ASSIGNMENTREORDERPREPROCESSOR_HPP

#include <vector>
#include "IPreprocessor.hpp"
#include "PreprocessingException.hpp"

class AssignmentReorderPreprocessor : public IPreprocessor {
public:
    std::vector<Token> Preprocess (std::vector<Token>& tokens) override {
        std::vector<Token> newTokens;
        if (!tokens.empty())
            newTokens.push_back(tokens[0]);
        for (auto i = 1; i < tokens.size() - 1; ++i) {
            auto& prev = tokens[i - 1];
            auto& cur = tokens[i];
            auto& next = tokens[i + 1];
            if (cur.GetType() == TokenType::assignment) {
                if (next.GetType() != TokenType::text || prev.GetType() != TokenType::text)
                    throw PreprocessingException("Invalid assignment");
                newTokens.pop_back();
                newTokens.emplace_back(TokenType::text, "=");
                newTokens.push_back(prev);
                if (i != tokens.size() - 2)
                    newTokens.push_back(next);
                i++;
            }
            else {
                newTokens.push_back(cur);
            }
        }
        if (tokens.size() > 1)
            newTokens.push_back(tokens.back());
        return newTokens;
    }
};

#endif //CLI_IMPLEMENTATION_ASSIGNMENTREORDERPREPROCESSOR_HPP
