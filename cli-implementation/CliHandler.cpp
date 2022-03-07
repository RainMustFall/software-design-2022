#include <sstream>

#include "Parser.hpp"
#include "DefaultConfiguration.hpp"
#include "CliHandler.hpp"

#define BOLDGREEN   "\033[1m\033[32m"
#define BOLDBLUE    "\033[1m\033[34m"
#define RESET   "\033[0m"

void PrintPrompt() {
    std::cout
        << BOLDGREEN << std::getenv("USERNAME")
        << RESET << ":"
        << BOLDBLUE << std::filesystem::current_path().string()
        << RESET << "$ " << std::flush;
}

void start_cli() {
    DefaultConfiguration configuration;
    std::string line;
    PrintPrompt();
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
        PrintPrompt();
        std::getline(std::cin, line);
    }
}

int main() {
    start_cli();
    return 0;
}
