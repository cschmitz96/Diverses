using System;

namespace C_Sharp_HelloWorld
{
    public class GameLogic
    {
        public Person[] people;
        public Party[] parties;
        public int numberOfParties;
        public int population;
        public int populationSqrt;

        public int convinceCount;
        public int sleepSpeed = 50;
        public int outputRuns = 10000;

        //Konstruktor
        public GameLogic(int inputPopulation, string[] inputParties)
        {
            Random random = new Random();

            population = inputPopulation;
            populationSqrt = (int)Math.Sqrt(population);
            //initialisation of Party Array
            parties = new Party[inputParties.Length];
            for (int i=0; i<inputParties.Length; i++)
            {
                parties[i] = new Party();
                parties[i].setName(inputParties[i]);
                parties[i].setColor(0);
            }

            //initialisation of people Array
            people = new Person[population];
            for (int i=0; i<population; i++)
            {
                int randparty = random.Next(parties.Length);
                people[i] = new Person();
                people[i].setIndex(i);
                people[i].setParty(parties[randparty]);
            }
        }

        public void outputGrid()
        {
            string output = "";
            for (int i = 0; i < population; i++)
            {
                output = output + people[i].getParty().name + " ";
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
                    if (people[peoplecount].getParty().name == parties[i].name)
                    {
                        absoluteparty[i] += 1;
                    }
                }
            }
            Console.WriteLine("----------------------------");

            for (int i = 0; i < absoluteparty.Length; i++)
            {
                relativeparty[i] = (double)absoluteparty[i] / ((double)population / (double)100);
                Console.WriteLine(parties[i].name + "   " + absoluteparty[i] + "   " + relativeparty[i] + "%");
            }

            for (int i = 0; i < absoluteparty.Length; i++)
            {
                if ((int)absoluteparty[i] == population)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine(parties[i].name + "  has won!!!");
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

        //getter und setter Methoden
        public void setIndex(int newIndex)
        {
            index = newIndex;
        }

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
        public string name;
        public int color;
        public Party()
        {
            name = default;
            color = 0;
        }
        public void setName(string newName)
        {
            name = newName;
        }

        public void setColor(int newColor)
        {
            color = newColor;
        }
    }
    public class HelloWorld
    {
        static void Main(string[] args)
        {
            int population = 400;
            string[] parties = {"AAA", "___"};
            GameLogic game = new GameLogic(population, parties);
            game.gameLoop();
        }
    }
}
