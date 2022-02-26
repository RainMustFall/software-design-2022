//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_PREPROCESSOR_HPP
#define CLI_PREPROCESSOR_HPP

#include <vector>
#include "Token.hpp"

class IPreprocessor {
public:
    virtual ~IPreprocessor()= default;

    virtual std::vector<Token> Preprocess(std::vector<Token>& tokens) = 0;
};

#endif //CLI_PREPROCESSOR_HPP
