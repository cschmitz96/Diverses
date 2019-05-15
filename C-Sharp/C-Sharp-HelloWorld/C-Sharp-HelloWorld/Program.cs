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
        private int sleepSpeed;
        private int outputRuns;
        private int colorMode;

        //Konstruktor
        public GameLogic(int inputPopulation, string[] inputParties, int arrayMode, int newSleepSpeed, int newOutputRuns, System.ConsoleColor[] colors, int newColorMode)
        {
            Random random = new Random();

            sleepSpeed = newSleepSpeed;
            outputRuns = newOutputRuns;
            population = inputPopulation;
            colorMode = newColorMode;
            populationSqrt = (int)Math.Sqrt(population);
            //initialisation of Party Array
            parties = new Party[inputParties.Length];
            for (int i=0; i<inputParties.Length; i++)
            {
                parties[i] = new Party(inputParties[i], colors[i]);
            }

            //initialisation of people Array
            if (arrayMode == 0)
            {
                initializeRandomArray();
            }
            else if (arrayMode == 1)
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
        public void outputGridNoColor()
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
        public void outputGridWithColor()
        {
            for(int i = 0; i < population; i++)
            {
                Console.ForegroundColor = people[i].getParty().getColor();
                Console.Write(people[i].getParty().getName() + " ");
                if((i+1) % populationSqrt == 0)
                {
                    Console.WriteLine("");
                }
            }
        }
        public bool logStats()
        {
            Console.ForegroundColor = ConsoleColor.White;
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
                    if (colorMode == 0)
                    {
                        outputGridNoColor();
                    }
                    else if (colorMode == 1)
                    {
                        outputGridWithColor();
                    }
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
                    //left
                    position = mod((personIndex - 1 + populationSqrt) % populationSqrt, populationSqrt) + (((int)personIndex / populationSqrt) * populationSqrt);
                    break;
                case 1:
                    //right
                    position = mod((personIndex + 1 + populationSqrt) % populationSqrt, populationSqrt) + (((int)personIndex / populationSqrt) * populationSqrt);
                    break;
                case 2:
                    //above
                    position = mod(personIndex - populationSqrt, population);
                    break;
                case 3:
                    //below
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
        private System.ConsoleColor color;
        //Konstruktor
        public Party(string newName, System.ConsoleColor newColor)
        {
            name = newName;
            color = newColor;
        }
        public string getName()
        {
            return name;
        }
        public System.ConsoleColor getColor()
        {
            return color;
        }
    }
    public class HelloWorld
    {
        static void Main(string[] args)
        {
            int population = 400;
            string[] parties = {"O", "X", "I"};
            System.ConsoleColor[] colors = { ConsoleColor.Red, ConsoleColor.Green, ConsoleColor.Blue};
            int sleepSpeed = 50;
            int outputRuns = 5000;
            //Population, Array der Partein, Modus des Personen arrays, Pause zwischen den Outputs, nach wie vielen Runs Ausgegeben werden soll, Farben der PArtein, Farbmodus
            GameLogic game = new GameLogic(population, parties, 0, sleepSpeed, outputRuns, colors, 1);
            game.gameLoop();
        }
    }
}
