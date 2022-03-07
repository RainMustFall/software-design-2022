//
// Created by Zeliboba on 26.02.2022.
//

#include <string>
#include <utility>
#include <vector>
#include <unordered_map>
#include <functional>
#include "ICommand.hpp"
#include "InterpretationException.hpp"
#include "SystemCommand.hpp"

#ifndef CLI_IMPLEMENTATION_COMMANDFACTORY_HPP
#define CLI_IMPLEMENTATION_COMMANDFACTORY_HPP


namespace cli {

using CommandFactory = std::function<ICommandPtr(std::vector<std::string>&)>;

/*
 * Abstracts the creation of commands.
 * */
class CommandRegistry {
public:
    ICommandPtr Build(const std::string& commandName, std::vector<std::string>&
        arguments) const {
        if (!_factories.count(commandName) != 0)
          return std::make_shared<SystemCommand>(commandName, arguments);
        return _factories.at(commandName)(arguments);
    }

    CommandRegistry& WithFactory(const std::string& commandName,
                                 CommandFactory factory) {
        _factories[commandName] = std::move(factory);
        return *this;
    }
private:
    std::unordered_map<std::string, CommandFactory> _factories;
};

}

#endif //CLI_IMPLEMENTATION_COMMANDFACTORY_HPP
