using System;

namespace C_Sharp_HelloWorld
{
    public class GameLogic
    {
        private Person[] people;
        private Party[] parties;
        private int population;
        private int populationSqrt;

        private int convinceCount;
        private int sleepSpeed = 50;
        private int outputRuns = 10000;

        //Konstruktor
        public GameLogic(int inputPopulation, string[] inputParties, int mode)
        {
            Random random = new Random();

            population = inputPopulation;
            populationSqrt = (int)Math.Sqrt(population);
            //initialisation of Party Array
            parties = new Party[inputParties.Length];
            for (int i=0; i<inputParties.Length; i++)
            {
                parties[i] = new Party(inputParties[i]);
            }

            //initialisation of people Array
            if (mode == 0)
            {
                initializeRandomArray();
            }
            else if (mode == 1)
            {
                initializeConstructedArray();
            }
        }
        public void initializeRandomArray()
        {
            Random random = new Random();

            people = new Person[population];
            for (int i = 0; i < population; i++)
            {
                int randparty = random.Next(parties.Length);
                people[i] = new Person(i, parties[randparty]);
            }
        }

        public void initializeConstructedArray()
        {
            people = new Person[population];
            int equalPart = population / parties.Length;
            int temp = -1;
            for(int i = 0; i < population; i++)
            {
                if (i % equalPart == 0)
                {
                    temp++;
                }
                people[i] = new Person(i, parties[temp]);
            }
        }
        public void outputGrid()
        {
            string output = "";
            for (int i = 0; i < population; i++)
            {
                output = output + people[i].getParty().getName() + " ";
                if ((i + 1) % populationSqrt == 0)
                {
                    output = output + "\n";
                }
            }
            Console.WriteLine(output);
        }
        public bool logStats()
        {
            double[] relativeparty = new double[parties.Length];
            int[] absoluteparty = new int[parties.Length];

            for (int peoplecount = 0; peoplecount < population; peoplecount++)
            {
                for (int i = 0; i < parties.Length; i++)
                {
                    if (people[peoplecount].getParty().getName() == parties[i].getName())
                    {
                        absoluteparty[i] += 1;
                    }
                }
            }
            Console.WriteLine("----------------------------");

            for (int i = 0; i < absoluteparty.Length; i++)
            {
                relativeparty[i] = (double)absoluteparty[i] / ((double)population / (double)100);
                Console.WriteLine(parties[i].getName() + "   " + absoluteparty[i] + "   " + relativeparty[i] + "%");
            }

            for (int i = 0; i < absoluteparty.Length; i++)
            {
                if ((int)absoluteparty[i] == population)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine(parties[i].getName() + "  has won!!!");
                    Console.WriteLine("nach insgesamt  " + convinceCount + "  überzeugungen");
                    return true;
                }
            }
            return false;
        }

        public void gameLoop()
        {
            for (int i = 0; true; i++)
            {
                convinceCount++;
                Random rand = new Random();
                people[rand.Next(population)].convince(population, people);
                if (i % outputRuns == 0)
                {
                    Console.Clear();
                    outputGrid();
                    if (logStats())
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(sleepSpeed);
                }
            }
        }
    }

    public class Person
    {
        private int index;
        private Party party;

        //Konstruktor
        public Person(int newIndex, Party newParty)
        {
            index = newIndex;
            party = newParty;
        }
        //getter und setter Methoden
        public void setParty(Party newParty)
        {
            party = newParty;
        }
        public Party getParty()
        {
            return party;
        }

        //actual Methods
        public void convince(int population, Person[] people)
        {
            Neighbour neighbour = new Neighbour();
            Random random = new Random();
            int position = neighbour.findNeighbour(index, population);
            talk(position, people);
        }

        public void talk(int position, Person[] people)
        {
            Random random = new Random();
            int rand = random.Next(2);
            if (rand == 1)
            {
                people[position].setParty(people[index].getParty());
            }
        }
    }

    public class Neighbour
    {
        public int findNeighbour(int personIndex, int population)
        {
            Random random = new Random();
            int populationSqrt = (int)Math.Sqrt(population);
            int rand = random.Next(4);
            int position = -1;
            switch (rand)
            {
                case 0:
                    position = mod((personIndex - 1 + populationSqrt) % populationSqrt, populationSqrt) + (((int)personIndex / populationSqrt) * populationSqrt);
                    break;
                case 1:
                    position = mod((personIndex + 1 + populationSqrt) % populationSqrt, populationSqrt) + (((int)personIndex / populationSqrt) * populationSqrt);
                    break;
                case 2:
                    position = mod(personIndex - populationSqrt, population);
                    break;
                case 3:
                    position = mod(personIndex + populationSqrt, population);
                    break;
            }
            return position;
        }

        public int mod(int a, int b)
        {
            return (a % b + b) % b;
        }
    }

    public class Party
    {
        private string name;
        private int color;
        //Konstruktor
        public Party(string newName)
        {
            name = newName;
            color = 0;
        }
        public string getName()
        {
            return name;
        }
        public int getColor()
        {
            return color;
        }
    }
    public class HelloWorld
    {
        static void Main(string[] args)
        {
            int population = 400;
            string[] parties = {"AAA", "___"};
            GameLogic game = new GameLogic(population, parties, 0);
            game.gameLoop();
        }
    }
}
