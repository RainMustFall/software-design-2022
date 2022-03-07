//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_COMBINEDPREPROCESSOR_HPP
#define CLI_IMPLEMENTATION_COMBINEDPREPROCESSOR_HPP

#include <utility>

#include "IPreprocessor.hpp"

namespace cli {

/*
    Abstracts out a list of preprocessors and applies them all at the same time.
*/
class CombinedPreprocessor : IPreprocessor {
 public:
    explicit CombinedPreprocessor(std::vector<IPreprocessorPtr> preprocessors)
        : _preprocessors(std::move(preprocessors)) {}

    std::vector<Token> Preprocess(std::vector<Token>& tokens) const override {
        for (const auto& p: _preprocessors) {
            tokens = p->Preprocess(tokens);
        }
        return tokens;
    }

    [[maybe_unused]] bool TryPreprocess(OUT std::vector<Token>& tokens) {
        try {
            tokens = Preprocess(tokens);
            return true;
        }
        catch (std::exception& err) {
            return false;
        }
    }
 private:
    std::vector<IPreprocessorPtr> _preprocessors;
};

}

#endif //CLI_IMPLEMENTATION_COMBINEDPREPROCESSOR_HPP
