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

#ifndef CLI_IMPLEMENTATION_COMMANDFACTORY_HPP
#define CLI_IMPLEMENTATION_COMMANDFACTORY_HPP

using CommandFactory = std::function<ICommand*(std::vector<std::string>&)>;

/*
 * Abstracts the creation of commands.
 * */
class CommandRegistry {
public:
    ICommand* Build(const std::string& commandName, std::vector<std::string>& arguments){
        if (!_factories.contains(commandName))
            throw InterpretationException("Unknown command. Please check input.");
        return _factories[commandName](arguments);
    }

    CommandRegistry* WithFactory(const std::string& commandName, CommandFactory factory) {
        _factories[commandName] = std::move(factory);
        return this;
    }
private:
    std::unordered_map<std::string, CommandFactory> _factories;
};

#endif //CLI_IMPLEMENTATION_COMMANDFACTORY_HPP
