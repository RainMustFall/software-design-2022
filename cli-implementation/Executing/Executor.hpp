#include <utility>
#include "Execution.hpp"
#include "SyncProcess.hpp"

//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_EXECUTOR_HPP
#define CLI_EXECUTOR_HPP

// TODO: Think about asynchronous processes?
class Executor {
public:
    IProcess* Run(Execution& execution, ExecutionContext& executionContext) {
        auto bufferedStdout = std::ostringstream();
        auto bufferedStderr = std::ostringstream();
        auto process = new SyncProcess();
        // TODO: Make use of edges
        for (auto command : execution._commands) {
            auto curResult = command->Execute(executionContext);
            _flush(curResult->GetStdout(), _stdout);
            _flush(curResult->GetStderr(), _stderr);
        }
    }
private:
    static void _flush(std::istream& from, std::ostream& to){
        while (!from.eof()) {
            to << from.get();
        }
    }
};

#endif //CLI_EXECUTOR_HPP
