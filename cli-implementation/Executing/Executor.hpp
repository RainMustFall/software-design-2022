#include <utility>
#include "Execution.hpp"
#include "SynchronousProcess.hpp"

//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_EXECUTOR_HPP
#define CLI_EXECUTOR_HPP

class Executor {
public:
    explicit Executor(IStorage* storage) : _storage(storage) {}

    IProcess* Run(Execution& execution) {
        auto emptyStdin = std::stringstream();
        auto process = new SynchronousProcess();
        // TODO: Make use of edges
        // For now we will just flush everything into stdout of current execution
        for (auto command : execution._commands) {
            // We can put stdout of previous command instead of stdin here
            auto curContext = ExecutionContext(_storage, emptyStdin);
            auto curResult = command->Execute(curContext);
            // TODO: Think about asynchronous processes?
            _flush(curResult->GetStdout(), process->GetWritableStdout());
            _flush(curResult->GetStderr(), process->GetWritableStderr());
        }
        return process;
    }
private:
    IStorage* _storage;

    static void _flush(std::istream& from, std::ostream& to){
        int ch;
        while ((ch = from.get()) != EOF) {
            to << (char) ch;
        }
    }
};

#endif //CLI_EXECUTOR_HPP
