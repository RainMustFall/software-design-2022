//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_PREPROCESSOR_HPP
#define CLI_PREPROCESSOR_HPP

#include <vector>
#include "Token.hpp"

/*
    Abstracts an arbitrary transform of sequence of tokens.
    It is allowed to transform tokens as well as filter / add / reorder them.
*/
class IPreprocessor {
public:
    virtual ~IPreprocessor()= default;

    virtual std::vector<Token> Preprocess(std::vector<Token>& tokens) = 0;
};

#endif //CLI_PREPROCESSOR_HPP
