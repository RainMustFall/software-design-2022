//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_ISTORAGE_HPP
#define CLI_IMPLEMENTATION_ISTORAGE_HPP

#include <string>
#include "Defines.hpp"

class IStorage {
public:
    virtual bool TryGet(std::string& key, OUT std::string& value) = 0;
    virtual void Set(std::string& key, std::string& value) = 0;
};

#endif //CLI_IMPLEMENTATION_ISTORAGE_HPP
