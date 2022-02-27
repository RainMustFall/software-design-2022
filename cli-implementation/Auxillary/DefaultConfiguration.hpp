//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_DEFAULTCONFIGURATION_HPP
#define CLI_IMPLEMENTATION_DEFAULTCONFIGURATION_HPP

#include "IStorage.hpp"
#include "InMemoryStorage.hpp"
#include "CombinedPreprocessor.hpp"
#include "AssignmentReorderPreprocessor.hpp"
#include "SubstitutionPreprocessor.hpp"
#include "DoubleQuoteMergePreprocessor.hpp"
#include "SpaceFilterPreprocessor.hpp"
#include "CommandRegistry.hpp"
#include "EchoCommand.hpp"
#include "QuoteToTextPreprocessor.hpp"
#include "AssignCommand.hpp"

/*
 * Represents a default configuration for a whole pipeline of CLI.
 * */
class DefaultConfiguration {
public:
    DefaultConfiguration() {
        _storage = reinterpret_cast<IStorage *>(new InMemoryStorage());
        _combined = SetUpPreprocessor(_storage);
        _registry = SetUpRegistry();
    }

    ~DefaultConfiguration(){
        delete _storage;
        delete _combined;
    }

    [[nodiscard]] IStorage* GetStorage() const {
        return _storage;
    }

    [[nodiscard]] CombinedPreprocessor* GetCombinedPreprocessor() const {
        return _combined;
    }

    [[nodiscard]] CommandRegistry* GetCommandRegistry() const {
        return _registry;
    }
private:
    IStorage* _storage;
    CombinedPreprocessor* _combined;
    CommandRegistry* _registry;

    static CommandRegistry* SetUpRegistry(){
        auto registry = new CommandRegistry();
        return registry
            ->WithFactory("echo",
                          [](std::vector<std::string>& args) { return dynamic_cast<ICommand *>(new EchoCommand(args)); })
            ->WithFactory("=",
                          [](std::vector<std::string>& args) { return dynamic_cast<ICommand *>(new AssignCommand(args)); });
    }

    static CombinedPreprocessor* SetUpPreprocessor(IStorage* storage){
        std::vector<IPreprocessor*> preprocessors = {
                new SubstitutionPreprocessor(storage),
                new DoubleQuoteMergePreprocessor(),
                new SpaceFilterPreprocessor(),
                new QuoteToTextPreprocessor(),
                new AssignmentReorderPreprocessor(),
        };
        return new CombinedPreprocessor(preprocessors);
    }
};

#endif //CLI_IMPLEMENTATION_DEFAULTCONFIGURATION_HPP
