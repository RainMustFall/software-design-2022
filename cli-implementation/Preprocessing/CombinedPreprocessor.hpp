//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_COMBINEDPREPROCESSOR_HPP
#define CLI_IMPLEMENTATION_COMBINEDPREPROCESSOR_HPP

#include <utility>

#include "IPreprocessor.hpp"

class CombinedPreprocessor : IPreprocessor {
public:
    explicit CombinedPreprocessor(std::vector<IPreprocessor*> preprocessors) : _preprocessors(std::move(preprocessors)) {}
    ~CombinedPreprocessor() override {
        for (auto p : _preprocessors)
            delete p;
    }

    std::vector<Token> Preprocess(std::vector<Token>& tokens) override {
        for (auto p : _preprocessors) {
            tokens = p->Preprocess(tokens);
        }
        return tokens;
    }

    // TODO: Error type?
    bool TryPreprocess(OUT std::vector<Token>& tokens) {
        try {
            tokens = Preprocess(tokens);
            return true;
        }
        catch (std::exception& err) {
            return false;
        }
    }
private:
    std::vector<IPreprocessor*> _preprocessors;
};

#endif //CLI_IMPLEMENTATION_COMBINEDPREPROCESSOR_HPP
