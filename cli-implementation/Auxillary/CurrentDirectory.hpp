//
// Created by Dzmitry Karshakevich on 07.03.2022.
//

#ifndef CLI_IMPLEMENTATION_CURRENTDIRECTORY_HPP_
#define CLI_IMPLEMENTATION_CURRENTDIRECTORY_HPP_

#ifdef _WIN32

#include <windows.h>

namespace cli {

std::string CurrentDirectory() {
  char buffer[MAX_PATH];
  GetCurrentDirectoryA(MAX_PATH, buffer);
  return std::string(buffer);
}

}

#else

#include <unistd.h>

std::string CurrentDirectory() {
  char buffer[1000];
  size_t length = ::readlink("/proc/self/exe", buffer, sizeof(buffer)-1);
  if (length != -1) {
    buffer[length] = '\0';
    return std::string(buffer);
  }

  throw std::runtime_error("Getting pwd failed");
}

#endif

#endif  // CLI_IMPLEMENTATION_CURRENTDIRECTORY_HPP_
