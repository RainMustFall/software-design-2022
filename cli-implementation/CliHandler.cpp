#include <sstream>

#include "Parser.hpp"
#include "DefaultConfiguration.hpp"
#include "CliHandler.hpp"

void start_cli() {
    DefaultConfiguration configuration;
    std::string line;
    std::getline(std::cin, line);
    while (line != "exit") {
        try {
            for (const auto&[stdout_str, stderr_str]:
                handleAndExtract(line, configuration)) {
                std::cout << stdout_str;
                std::cerr << stderr_str;
            }
        }
        catch (std::exception& err) {
            auto what = err.what();
            std::cerr << what << std::endl;
        }
        std::getline(std::cin, line);
    }
}

int main() {
    start_cli();
    return 0;
}
