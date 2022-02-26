//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_EXECUTIONCONTEXT_HPP
#define CLI_IMPLEMENTATION_EXECUTIONCONTEXT_HPP

#include "IStorage.hpp"

// TODO: Pointers to shared ptrs
class ExecutionContext {
public:
    IStorage* GetStorage() {
        return _storage;
    }
private:
    IStorage* _storage;
};

#endif //CLI_IMPLEMENTATION_EXECUTIONCONTEXT_HPP
