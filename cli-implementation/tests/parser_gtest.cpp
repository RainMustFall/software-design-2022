//
// Created by Zeliboba on 27.02.2022.
//

#include <gtest/gtest.h>
#include "Token.hpp"
#include "Parser.hpp"

void ShouldBe(std::string input, std::vector<Token> expected) {
    std::istringstream inputStream(input);
    auto actual = Parser::Parse(inputStream);
    EXPECT_EQ(actual.size(), expected.size());
    for (auto i = 0; i < actual.size(); ++i) {
        EXPECT_EQ(actual[i], expected[i]);
    }
}

TEST(Parser, SimpleScenario) {
    ShouldBe("echo test", {Token("echo"), Token(TokenType::space, " "), Token("test")});
}

TEST(Parser, EmptyInput) {
    ShouldBe("", {});
}
