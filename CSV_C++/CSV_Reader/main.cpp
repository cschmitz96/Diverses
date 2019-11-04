#include <iostream>
#include <sstream>
#include <fstream>
#include <string>
#include <stdlib.h>
#include <stdio.h>
#include <time.h>

#include <question.h>

using namespace std;

int main()
{
    srand (time(NULL));
    ifstream csvread;
    csvread.open("test.csv", ios::in);
    if(csvread){
        string s="";
        int id_numb = 2;
        int length = (id_numb* 7)+1;
        string question[length];
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
        while (rand_question == 0) {
            rand_question = rand() % id_numb+1;
        }
        for (;a<length;a++) {
            string s = question[a];
            stringstream geek(s);
            int temp = 0;
            geek >> temp;
            if(temp == rand_question) {
                tempx = a;
                break;
            }
        }
        Question final_question(question[tempx], question[tempx+1], question[tempx+2], question[tempx+3], question[tempx+4], question[tempx+5], question[tempx+6]);
        final_question.printQuestion();
    }
    else{
        cerr << "Fehler beim lesen" << endl;
    }
    return 0;
}
