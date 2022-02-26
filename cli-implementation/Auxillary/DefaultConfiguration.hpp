//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_DEFAULTCONFIGURATION_HPP
#define CLI_IMPLEMENTATION_DEFAULTCONFIGURATION_HPP

#include "IStorage.hpp"
#include "Storages/InMemoryStorage.hpp"
#include "CombinedPreprocessor.hpp"
#include "AssignmentReorderPreprocessor.hpp"
#include "SubstitutionPreprocessor.hpp"
#include "DoubleQuoteMergePreprocessor.hpp"
#include "SpaceFilterPreprocessor.hpp"
#include "Interpretation/CommandRegistry.hpp"

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
private:
    IStorage* _storage;
    CombinedPreprocessor* _combined;
    CommandRegistry* _registry;

    static CommandRegistry* SetUpRegistry(){
        auto registry = new CommandRegistry();
        return registry
            ->WithFactory()
            ->WithFactory();
    }

    static CombinedPreprocessor* SetUpPreprocessor(IStorage* storage){
        std::vector<IPreprocessor*> preprocessors = {
                new AssignmentReorderPreprocessor(),
                new SubstitutionPreprocessor(storage),
                new DoubleQuoteMergePreprocessor(),
                new SpaceFilterPreprocessor(),
        };
        return new CombinedPreprocessor(preprocessors);
    }
};

#endif //CLI_IMPLEMENTATION_DEFAULTCONFIGURATION_HPP
