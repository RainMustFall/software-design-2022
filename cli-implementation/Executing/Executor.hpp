#include <utility>

//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_EXECUTOR_HPP
#define CLI_EXECUTOR_HPP

class Execution {
public:
    explicit Execution(Token token) : _token(std::move(token))
    { }

    TokenType GetTokenType() {
        return _token.GetType();
    }

    std::string GetExecutionContext() {
        return _executionContext;
    }

    void SetExecutionContext(const std::string & context) {
        _executionContext = context;
    }

private:
    Token _token;
    std::string _executionContext;
};

class Executor {
public:
    static std::string Run(Execution * execution) {

        // TODO: need implementation use ICommand, CommandRegistry etc.
        //WIP: simple test implementation for echo cmd
        if (execution->GetTokenType() == TokenType::echoCmdToken) {
            return execution->GetExecutionContext();
        }
    }
};

#endif //CLI_EXECUTOR_HPP
