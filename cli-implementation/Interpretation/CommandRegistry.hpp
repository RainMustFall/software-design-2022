//
// Created by Zeliboba on 26.02.2022.
//

#include <string>
#include <vector>
#include <unordered_map>
#include <functional>
#include "ICommand.hpp"

#ifndef CLI_IMPLEMENTATION_COMMANDFACTORY_HPP
#define CLI_IMPLEMENTATION_COMMANDFACTORY_HPP

using CommandFactory = std::function<ICommand*(std::vector<std::string>&)>;

class CommandRegistry {
public:
    ICommand* Build(const std::string& commandName, std::vector<std::string>& arguments){
        return _factories[commandName](arguments);
    }

    CommandRegistry* WithFactory(std::string& commandName, CommandFactory& factory) {
        _factories[commandName] = factory;
        return this;
    }
private:
    std::unordered_map<std::string, CommandFactory> _factories;
};

#endif //CLI_IMPLEMENTATION_COMMANDFACTORY_HPP
