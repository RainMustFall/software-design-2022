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

    std::istream& GetStdin() {
        return _stdin;
    }

    std::ostream& GetStdout() {
        return _stdout;
    }

    std::ostream& GetStderr() {
        return _stderr;
    }
private:
    IStorage* _storage;
    std::istream& _stdin;
    std::ostream& _stdout;
    std::ostream& _stderr;
};

#endif //CLI_IMPLEMENTATION_EXECUTIONCONTEXT_HPP
