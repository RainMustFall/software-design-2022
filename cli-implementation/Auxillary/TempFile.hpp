//
// Created by Dzmitry Karshakevich on 27.02.2022.
//

#ifndef CLI_IMPLEMENTATION_AUXILLARY_TEMPFILE_HPP_
#define CLI_IMPLEMENTATION_AUXILLARY_TEMPFILE_HPP_

#include <string>
#include <fstream>

/*
 * A small class that implements RAII to
 * create and delete temporary files in a timely manner.
 */
class TempFile {
public:
  TempFile(): name_(std::tmpnam(nullptr)) {
    std::ofstream file(name_);
    file.close();
  }

  [[nodiscard]] const std::string& GetName() const {
    return name_;
  }

  ~TempFile(){
    std::remove(name_.data());
  }

private:
  std::string name_;
};

#endif // CLI_IMPLEMENTATION_AUXILLARY_TEMPFILE_HPP_
