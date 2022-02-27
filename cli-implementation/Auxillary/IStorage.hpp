//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_ISTORAGE_HPP
#define CLI_IMPLEMENTATION_ISTORAGE_HPP

#include <string>
#include "Defines.hpp"

/*
 * Abstracts out some key-value storage.
 * */
class IStorage {
public:
    virtual ~IStorage()= default;

    virtual bool TryGet(std::string& key, OUT std::string& value) const = 0;
    virtual void Set(std::string& key, std::string& value) = 0;
};

#endif //CLI_IMPLEMENTATION_ISTORAGE_HPP
