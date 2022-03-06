#include <utility>
#include "Execution.hpp"
#include "SynchronousProcess.hpp"
#include "ExecutionException.hpp"

//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_EXECUTOR_HPP
#define CLI_EXECUTOR_HPP

class Executor {
public:
    explicit Executor(std::shared_ptr<IStorage> storage) : _storage(std::move(storage)) {}

    std::shared_ptr<IProcess> Run(Execution& execution) {
        auto emptyStdin = std::stringstream();
        std::shared_ptr<IProcess> prevResult;
        auto process = std::make_shared<SynchronousProcess>();
        for (auto i = 0; i < execution._commands.size(); ++i) {
            const auto& command = execution._commands[i];
            const auto& edge = execution._edges[i];
            auto curContext = ExecutionContext(_storage, prevResult != nullptr ? prevResult->GetStdout() : emptyStdin);
            auto curResult = command->Execute(curContext);
            if (edge == ExecutionEdge::piping) {
                prevResult = curResult;
            }
            else if (edge == ExecutionEdge::ignoring) {
                _flush(curResult->GetStdout(), process->GetWritableStdout());
                prevResult = nullptr;
            }
            else {
                throw ExecutionException("Unknown edge type: " + std::to_string(edge));
            }
            _flush(curResult->GetStderr(), process->GetWritableStderr());
        }
        return process;
    }
private:
    std::shared_ptr<IStorage> _storage;

    static void _flush(std::istream& from, std::ostream& to){
        char ch;
        while (from.get(ch) && !from.eof()) {
            to << ch;
        }
    }
};

#endif //CLI_EXECUTOR_HPP
