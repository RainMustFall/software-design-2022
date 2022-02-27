//
// Created by Zeliboba on 26.02.2022.
//

#ifndef CLI_IMPLEMENTATION_EXECUTIONCONTEXT_HPP
#define CLI_IMPLEMENTATION_EXECUTIONCONTEXT_HPP

#include "IStorage.hpp"

// TODO: Pointers to shared ptrs
/*
 * Represents an execution context of any operation.
 * */
class ExecutionContext {
public:
    ExecutionContext(IStorage* storage, std::istream& istream) : _storage(storage), _stdin(istream) {}

    IStorage* GetStorage() {
        return _storage;
    }

    std::istream& GetStdin() {
        return _stdin;
    }
private:
    IStorage* _storage;
    std::istream& _stdin;
};

#endif //CLI_IMPLEMENTATION_EXECUTIONCONTEXT_HPP
