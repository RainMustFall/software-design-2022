//
// Created by sergbelom on 2/21/22.
//

#ifndef CLI_RETURNCODE_HPP
#define CLI_RETURNCODE_HPP

namespace cli {

enum ReturnCode {
    ok = 0,
    executionError = 1,
    unableToParse = 43,
    notFinishedYet = 99
};

}

#endif //CLI_RETURNCODE_HPP
