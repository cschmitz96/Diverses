#ifndef QUESTION_H
#define QUESTION_H
#include <string>


class Question
{
public:
    Question(std::string new_id, std::string new_question, std::string a, std::string b, std::string c, std::string d, char correct);

    void setQuestion(std::string new_id, std::string new_question, std::string a, std::string b, std::string c, std::string d, char correct);
    void printQuestion();

    std::string id;
    std::string question;
    std::string answereA;
    std::string answereB;
    std::string answereC;
    std::string answereD;
    char correctAnswere;
};

#endif // QUESTION_H
