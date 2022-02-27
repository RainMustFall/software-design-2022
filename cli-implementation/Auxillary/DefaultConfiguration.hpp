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
    DefaultConfiguration()
        : _storage(std::make_shared<InMemoryStorage>()),
          _combined(SetUpPreprocessor(_storage)),
          _registry(SetUpRegistry()) {
    }

    [[nodiscard]] auto GetStorage() const {
        return _storage;
    }

    [[nodiscard]] const CombinedPreprocessor& GetCombinedPreprocessor() const {
        return _combined;
    }

    [[nodiscard]] const CommandRegistry& GetCommandRegistry() const {
        return _registry;
    }
 private:
    std::shared_ptr<IStorage> _storage;
    CombinedPreprocessor _combined;
    CommandRegistry _registry;

    static CommandRegistry SetUpRegistry() {
        auto registry = CommandRegistry();
        return registry
            .WithFactory("echo",
                         [](std::vector<std::string>& args) {
                             return std::make_shared<EchoCommand>(args);
                         })
            .WithFactory("=",
                         [](std::vector<std::string>& args) {
                             return std::make_shared<AssignCommand>(args);
                         });
    }

    static CombinedPreprocessor SetUpPreprocessor(
        std::shared_ptr<IStorage> storage) {
        std::vector<IPreprocessorPtr> preprocessors = {
            std::make_shared<SubstitutionPreprocessor>(storage),
            std::make_shared<DoubleQuoteMergePreprocessor>(),
            std::make_shared<SpaceFilterPreprocessor>(),
            std::make_shared<QuoteToTextPreprocessor>(),
            std::make_shared<AssignmentReorderPreprocessor>(),
        };

        return CombinedPreprocessor(preprocessors);
    }
};

#endif //CLI_IMPLEMENTATION_DEFAULTCONFIGURATION_HPP
