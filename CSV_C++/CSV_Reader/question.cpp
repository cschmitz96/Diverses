#include <iostream>

#include "question.h"

Question::Question(std::string new_id, std::string new_question, std::string a, std::string b, std::string c, std::string d, char correct)
{
    setQuestion( new_id, new_question, a, b, c, d, correct);
}
void Question::setQuestion(std::string new_id, std::string new_question, std::string a, std::string b, std::string c, std::string d, char correct)
{
    id = new_id;
    question = new_question;
    answereA = a;
    answereB = b;
    answereC = c;
    answereD = d;
    correctAnswere= correct;
}
void Question::printQuestion()
{
    std::cout << "id: "<< id <<std::endl;
    std::cout << "Frage: "<< question <<std::endl;
    std::cout << "A: "<< answereA <<std::endl;
    std::cout << "B: "<< answereB <<std::endl;
    std::cout << "C: "<< answereC <<std::endl;
    std::cout << "D: "<< answereD <<std::endl;
    std::cout << "Richtig ist: "<< correctAnswere <<std::endl;
}
