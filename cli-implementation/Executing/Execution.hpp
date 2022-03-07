//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_EXECUTION_HPP
#define CLI_IMPLEMENTATION_EXECUTION_HPP

#include <utility>
#include <vector>
#include "Token.hpp"
#include "ICommand.hpp"
#include "ExecutionContext.hpp"


namespace cli {

/*
 * Represents a container for commands and interactions between them.
 * Its only purpose is to be used by the Executor.
 * */
class Execution {
 public:
  explicit Execution(std::vector<ICommandPtr> commands)
      : _commands(std::move(commands)) {}

 private:
  std::vector<ICommandPtr> _commands;

  friend class Executor;
};

}

#endif //CLI_IMPLEMENTATION_EXECUTION_HPP
