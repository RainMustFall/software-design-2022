//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_SUBSTITUTIONPREPROCESSOR_HPP
#define CLI_IMPLEMENTATION_SUBSTITUTIONPREPROCESSOR_HPP

#include "IPreprocessor.hpp"
#include "IStorage.hpp"
#include "PreprocessingException.hpp"

/*
    Replaces 'substitute' tokens with text tokens from underlying storage.
    Not existing tokens are replaced with empty strings by default.
*/
class SubstitutionPreprocessor : public IPreprocessor {
public:
    explicit SubstitutionPreprocessor(IStorage* storage, bool throwOnMissing = false) : _storage(storage), _throwOnMissing(throwOnMissing) {}

    std::vector<Token> Preprocess (std::vector<Token>& tokens) override {
        for (auto& token : tokens) {
            if (token.GetType() == TokenType::substitute) {
                std::string key = token.GetArgument();
                std::string value;
                if (!_storage->TryGet(key, value) && _throwOnMissing)
                    throw PreprocessingException("Unable to substitute " + key + ". No such value in underlying storage.");
                token = TokenType::text;
                token.SetArgument(value);
            }
        }
        return tokens;
    }

private:
    IStorage* _storage;
    bool _throwOnMissing;
};

#endif //CLI_IMPLEMENTATION_SUBSTITUTIONPREPROCESSOR_HPP
