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
int *probabilities;
int *people;
int *adressX;
int *adressY;
bool colorMode = false;
int *partyColor;
int convinceCount = 0;


//Methods
void initialize();
bool logStats();
int findNeighbour(int personPosition);
void talk(int position, int personPosition);
void convince(int personPosition);
int mod(int a, int b);
void outputGrid();
bool checkSquare(int input);
bool isNumber(string possibleNumber);

int main()
{
    // to make rand() work
    srand (time(nullptr));    
    int numberOfParties = 2;
    string inputParties[] = {"AA", "__"};
    int inputPartyColor[] = {199, 7, 47, 151, 226, 87, 120, 90, 222};
    int outputRuns = 10000;
    int sleepSpeed = 50;

    int inputProbabilities[numberOfParties];
    for (int i = 0; i < numberOfParties; i++) {
        inputProbabilities[i] = 50;
    }

    //Initialize
    population = 400;
    populationSqrt = (int)sqrt(population);
//    probabilities = inputProbabilities;
    party = inputParties;
    partyLength = (sizeof(inputParties)/sizeof(*inputParties));
    people = new int[population];
    adressX = new int[population];
    adressY = new int[population];
    partyColor = inputPartyColor;

    initialize();
    for (int i = 0; true ; i++) {
        convinceCount++;
        convince(rand() % population);
        if(i % outputRuns == 0){
            system("cls");
            outputGrid();
            if (logStats()) {
                break;
            }
            Sleep(sleepSpeed);
        }
    }
}

void initialize () {
    int sqrtPopulation = sqrt(population);
    for (int peopleNumber = 0; peopleNumber < population; peopleNumber++) {
        /*if(peopleNumber < 200){
            people[peopleNumber] = 0;
        }
        else{
            people[peopleNumber] = 1;
        }*/

        //Give a Person a random Party
        people[peopleNumber] = rand() % partyLength;
        //Calculate the adress of a Person
        adressX[peopleNumber] = (int)peopleNumber / sqrtPopulation;
        adressY[peopleNumber] = peopleNumber % sqrtPopulation;
    }
}

int findNeighbour(int personPosition){
    int random = rand() % 4;
    int position = NULL;
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
    return position;

}

void talk(int position, int personPosition){
    int random = rand() % 2;
    if (random == 1) {
        people[position] = people[personPosition];
    }
}

void convince(int personPosition) {
    int position = findNeighbour(personPosition);
    talk(position, personPosition);
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
}

bool logStats() {
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
            cout << "Nach insgesamt " << convinceCount << " ueberzeugungen!" << endl;

            return true;
        }
    }
    cout << "�berzeugungen: " << convinceCount << endl;
    cout << "----------------------------" << endl;
    return false;
}


