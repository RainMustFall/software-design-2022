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

class DefaultConfiguration {
public:
    DefaultConfiguration() {
        _storage = reinterpret_cast<IStorage *>(new InMemoryStorage());
        _preprocessors = {
            new AssignmentReorderPreprocessor(),
            new SubstitutionPreprocessor(_storage),
            new DoubleQuoteMergePreprocessor(),
            new SpaceFilterPreprocessor(),
        };
        _combined = new CombinedPreprocessor(_preprocessors);
    }

    ~DefaultConfiguration(){
        delete _storage;
        for (auto p : _preprocessors)
            delete p;
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
    std::vector<IPreprocessor*> _preprocessors;
    CombinedPreprocessor* _combined;
};

#endif //CLI_IMPLEMENTATION_DEFAULTCONFIGURATION_HPP
