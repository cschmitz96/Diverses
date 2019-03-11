#include <iostream>
#include <sstream>
#include <windows.h>
#include <stdio.h>
#include <time.h>
#include <math.h>

using namespace std;

//Global Variables
string *party;
int partyLength;
int population;
int populationSqrt;
int *people;
int *adressX;
int *adressY;


//Methods
void initialize();
boolean logStats();
void convince(int personPosition);
int mod(int a, int b);
void outputGrid();
bool checkSquare(int input);

int main(int argc, char *argv[])
{
    // to make rand() work
    srand (time(nullptr));

    //Inputs
    while(1){
        int inputPopulation;
        cout << "Enter your Population, it has to be a quare number or -1 to quit" << endl;
        cin >> inputPopulation;
        if(inputPopulation == -1){
            break;
        }
//        string inputParty[3] = {" red ", "black", "  g  "};
        string inputParty[2] = {" red ", "black"};
        bool check = checkSquare(inputPopulation);
        if(!check){
            cout << "Your number is not a square number" << endl;
        }
        else{
            //Initialize
            population = inputPopulation;
            populationSqrt = (int)sqrt(population);
            party = inputParty;
            partyLength = (sizeof(inputParty)/sizeof(*inputParty));
            people = new int[population];
            adressX = new int[population];
            adressY = new int[population];

            initialize();
            int run_count;
            int outputRuns;
            int sleepSpeed;
            cout << "Enter how many runs you want to do or 1 if you want to play till somone wins" << endl;
            cin >> run_count;
            cout << "Enter after how many runs you want an output  (1000 - 10000 recommended)" << endl;
            cin >> outputRuns;
            cout << "Enter how long should be the break after an output (1000 = 1sec)" << endl;
            cin >> sleepSpeed;
            if(run_count == 1){
                for (int i = 0; true ; i++) {
                    convince(rand() % population);
                    if(i % outputRuns == 0){
                        system("cls");
                        outputGrid();
                        if (logStats()) {
                            break;
                        };
                        Sleep(sleepSpeed);
                    }
                }
            }
            else{
                for (int i = 0; i<run_count ; i++) {
                    convince(rand() % population);
                    if(i % outputRuns == 0){
                        system("cls");
                        outputGrid();
                        if (logStats()) {
                            break;
                        };
                        Sleep(sleepSpeed);
                    }
                }
            }
        }
    }
}

bool checkSquare(int input){
    for(int i =0; i<input ; i++){
        if(i*i == input)
            return true;
    }
    return false;
}

void initialize () {
    int sqrtPopulation = sqrt(population);
    for (int peopleNumber = 0; peopleNumber < population; peopleNumber++) {
        //Give a Person a random Party
        people[peopleNumber] = rand() % partyLength;
        //Calculate the adress of a Person
        adressX[peopleNumber] = (int)peopleNumber / sqrtPopulation;
        adressY[peopleNumber] = peopleNumber % sqrtPopulation;
    }
}

void convince(int personPosition) {
    int random = rand() % 4;
    int position;
    switch (random) {
        // some math magic (QuickMafs)
        case 0: {
            position = mod((personPosition - 1 + populationSqrt) % populationSqrt, populationSqrt) + (((int)personPosition / populationSqrt) * populationSqrt);
            break;
        }
        case 1: {
            position = mod((personPosition + 1 - populationSqrt) % populationSqrt, populationSqrt) + (((int)personPosition / populationSqrt) * populationSqrt);
            break;
        }
        case 2: {
            position = mod(personPosition - populationSqrt, population);
            break;
        }
        case 3: {
            position = mod(personPosition + populationSqrt, population);
            break;
        }
    }
    if (people[personPosition] != people[position]) {
        if (rand() % 2 == 0) {
            people[position] = people[personPosition];
        }
    }
}

int mod(int a, int b)
{ return (a%b+b)%b; }

void outputGrid() {
    string output = "";
    for (int i = 0; i < population; i++) {
        output.append(party[people[i]] + " ");
        if ((i + 1) % populationSqrt == 0) {
            output.append("\n");
        }

    }
    cout << output;
//    SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE),124);
}

boolean logStats() {
    double relativeParty[partyLength] = {0};
    int absoluteParty[partyLength] = {0};
    for (int peopleCount = 0; peopleCount < population; peopleCount++) {
        absoluteParty[people[peopleCount]] += 1;
    }
    cout << "----------------------------" << endl;

    //normal stats
    for (int i = 0; i < (sizeof(absoluteParty)/sizeof(*absoluteParty)); i++) {
        relativeParty[i] = (double)absoluteParty[i] / ((double)population / (double)100);
       cout << party[i] << "  " << absoluteParty[i] << " " << relativeParty[i] << "%" << endl;
    }

    //someone won
    for (int i = 0; i < (sizeof(absoluteParty)/sizeof(*absoluteParty)); i++) {
        if ((int)absoluteParty[i] == population) {
            cout << "----------------------------" << endl;
            cout << party[i] << " has Won!!!" << endl;
            return true;
        }
    }
    cout << "----------------------------" << endl;
    return false;
}
