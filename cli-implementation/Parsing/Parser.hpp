//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_PARSER_HPP
#define CLI_PARSER_HPP

#include <iostream>
#include <vector>
#include <functional>
#include <unordered_set>

#include "Defines.hpp"
#include "Token.hpp"
#include "ParsingException.hpp"

class Parser {
public:
    static std::vector<Token> Parse(std::istream& stream) {
        std::vector<Token> tokens;
        while (!stream.eof()) {
            Token token;
            for (const auto& aggregator : _aggregators) {
                if (aggregator(stream, token)) {
                    tokens.push_back(token);
                    break;
                }
            }
            if (!stream.eof() && token.GetType() == undefined)
                throw ParsingException("Unable to parse input string. None of the possible tokens were sufficient.");
        }
        return tokens;
    }

    static bool TryParse(std::istream& stream, OUT std::vector<Token>& tokens) {
        try {
            tokens = Parse(stream);
            return true;
        }
        catch (std::exception& err) {
            return false;
        }
    }
private:
    static bool _aggregateSpaceToken(std::istream& stream, OUT Token& token) {
        if (std::isspace(stream.peek())) {
            token = TokenType::space;
            token.SetArgument(std::string{(char) stream.get()});
            return true;
        }
        return false;
    }

    static bool _aggregateSubstituteToken(std::istream& stream, OUT Token& token) {
        if (stream.peek() == '$') {
            stream.get();
            token = TokenType::substitute;
            if (_aggregateTextToken(stream, token)) {
                token.SetType(TokenType::substitute);
                return true;
            }
            throw ParsingException("Unable to parse substitute token. No text token after '$'.");
        }
        return false;
    }

    static bool _aggregateAssignmentToken(std::istream& stream, OUT Token& token) {
        if (stream.peek() == '=') {
            stream.get();
            token = TokenType::assignment;
            return true;
        }
        return false;
    }

    static bool _aggregateOperatorToken(std::istream& stream, OUT Token& token) {
        if (_operators.contains((char) stream.peek())) {
            token = TokenType::_operator;
            token.SetArgument(std::string{(char) stream.get()});
            return true;
        }
        return false;
    }

    static bool _aggregateDoubleQuoteToken(std::istream& stream, OUT Token& token) {
        if (stream.peek() == '"') {
            stream.get();
            token = TokenType::doubleQuote;
            return true;
        }
        return false;
    }

    static bool _aggregatePairedSingleQuoteToken(std::istream& stream, OUT Token& token) {
        if (stream.peek() == '\'') {
            stream.get();
            std::string argument;
            while (!stream.eof() && stream.peek() != '\'') {
                argument += (char) stream.get();
            }
            if (!stream.eof()) {
                stream.get();
                token = TokenType::pairedSingleQuote;
                token.SetArgument('\'' + argument + '\'');
                return true;
            }
            throw ParsingException("Unable to parse single quote token. Input stream ended prematurely.");
        }
        return false;
    }

    static bool _aggregateTextToken(std::istream& stream, OUT Token& token) {
        std::string argument;
        // TODO: Punct characters can be used in text tokens -> they can also be used as assignments and command names which is kinda bad?
        while (!stream.eof() && (std::isalnum(stream.peek()) ||
        std::ispunct(stream.peek()) && !_forbiddenTextTokenCharacters.contains((char) stream.peek()))) {
            argument += (char) stream.get();
        }
        if (!argument.empty()){
            token = TokenType::text;
            token.SetArgument(argument);
            return true;
        }
        return false;
    }

    static inline std::vector<std::function<decltype(_aggregateSpaceToken)>> _aggregators = { // NOLINT(cert-err58-cpp)
            _aggregateSpaceToken,
            _aggregateSubstituteToken,
            _aggregateAssignmentToken,
            _aggregateDoubleQuoteToken,
            _aggregateOperatorToken,
            _aggregatePairedSingleQuoteToken,
            _aggregateTextToken,
    };

    static inline std::unordered_set<char> _operators{ // NOLINT(cert-err58-cpp)
        '|',
        ';',
        '&',
        '>',
    };

    static inline std::unordered_set<char> _forbiddenTextTokenCharacters{ // NOLINT(cert-err58-cpp)
        '"',
        '\'',
        ';',
        ';',
        '&',
        '>',
        '|',
        '='
    };
};

#endif //CLI_PARSER_HPP
