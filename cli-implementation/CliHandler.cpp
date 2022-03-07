#include <sstream>

#include "Parser.hpp"
#include "Auxillary/CurrentDirectory.hpp"
#include "DefaultConfiguration.hpp"
#include "CliHandler.hpp"

namespace cli {

#ifdef _WIN32

#include <windows.h>

#define GREEN 10
#define WHITE 7
#define BLUE  3

void PrintPrompt() {
    HANDLE hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
    SetConsoleTextAttribute(hConsole, GREEN);
    std::cout << std::getenv("USERNAME");
    SetConsoleTextAttribute(hConsole, WHITE);
    std::cout << ":";
    SetConsoleTextAttribute(hConsole, BLUE);
    std::cout << CurrentDirectory();
    SetConsoleTextAttribute(hConsole, WHITE);
    std::cout << "$ " << std::flush;
}

#else

#define BOLDGREEN   "\033[1m\033[32m"
#define BOLDBLUE    "\033[1m\033[34m"
#define RESET   "\033[0m"

void PrintPrompt() {
    std::cout
        << BOLDGREEN << std::getenv("USERNAME")
        << RESET << ":"
        << BOLDBLUE << CurrentDirectory()
        << RESET << "$ " << std::flush;
}

#endif

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

}

int main() {
    cli::start_cli();
    return 0;
}
