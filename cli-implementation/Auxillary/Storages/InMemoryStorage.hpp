//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_INMEMORYSTORAGE_HPP
#define CLI_IMPLEMENTATION_INMEMORYSTORAGE_HPP

#include <unordered_map>
#include "IStorage.hpp"

/*
 * Represents an in memory storage.
 * Mappings are only saved in a corresponding object and do not persist between usages.
 * */
class InMemoryStorage : public IStorage {
public:
    bool TryGet(std::string& key, OUT std::string& value) override {
        if (_mapping.contains(key)) {
            value = _mapping[key];
            return true;
        }
        return false;
    }

    void Set(std::string& key, std::string& value) override {
        _mapping[key] = value;
    }
private:
    std::unordered_map<std::string, std::string> _mapping;
};

#endif //CLI_IMPLEMENTATION_INMEMORYSTORAGE_HPP
