#include <iostream>
#include <sstream>
#include <fstream>
#include <string>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#include <question.h>


int main()
{
    srand (time(NULL));
    std::ifstream csvread;
    csvread.open("Fragenkatalog.csv", std::ios::in);
    if(csvread){
        std::string s="";
        int id_numb = 12;
        //length = 7*id_numb +1
        const int length = 85;
        std::string question[length];
        int i=0;
        while(getline(csvread, s, ';'))
        {
            question[i]=s;
            i++;
        }
        csvread.close();
        int a=0;
        int tempx = 0;
        int rand_question = rand() % id_numb+1;
        for (;a<length;a++) {
            std::string s = question[a];
            std::stringstream geek(s);
            int temp = 0;
            geek >> temp;
            if(temp == rand_question) {
                tempx = a;
                break;
            }
        }
        std::string x = question[tempx+6];
        char check[2];
        strcpy(check, x.c_str());
        Question final_question(question[tempx], question[tempx+1], question[tempx+2], question[tempx+3], question[tempx+4], question[tempx+5], check[0]);
        final_question.printQuestion();
    }
    else{
        std::cerr << "Fehler beim lesen" << std::endl;
    }
    return 0;
}
