using System;

namespace C_Sharp_HelloWorld
{
    class HelloWorld
    {

        static void Main(string[] args)
        {
            HelloWorld world = new HelloWorld();
            Random random = new Random();
            int numberOfParties = 2; 
            string[] inputParties = { "AA", "__" };
            int outputRuns = 10000;
            int sleepSpeed = 50;
            int population = 900;
            int populationSqrt = (int)Math.Sqrt(population);
            int[] people = new int[population];
            people = world.initialize(population, people, numberOfParties);

            int convinceCount = 0;

            for (int i = 0; true; i++)
            {
                convinceCount++;
                int randPerson = random.Next(population);
                people = world.convince(people, randPerson, populationSqrt, population);
                if (i % outputRuns == 0)
                {
                    Console.Clear();
                    world.outputGrid(population, inputParties, people, populationSqrt);
                    if(world.logStats(numberOfParties, population, people, inputParties, convinceCount)){
                        break;
                    }
                    System.Threading.Thread.Sleep(sleepSpeed);
                }
            }
        }

        public int[] initialize(int population, int[] people, int numberOfParties)
        {
            Random random = new Random();
            for(int peopleNumber=0; peopleNumber < population; peopleNumber++)
            {
                people[peopleNumber] = random.Next(numberOfParties);
            }
            return people;
        }

        public int[] convince(int[] people, int personPosition, int populationSqrt, int population)
        {
            int position = findNeighbours(personPosition, populationSqrt, population);
            people = talk(people, position, personPosition);
            return people;
        }
        public int findNeighbours(int personPosition, int populationSqrt, int population)
        {
            Random random = new Random();
            int rand = random.Next(4);
            int position = -1;
            switch (rand)
            {
                case 0:
                    position = mod((personPosition - 1 + populationSqrt) % populationSqrt, populationSqrt) + (((int)personPosition / populationSqrt) * populationSqrt);
                    break;
                case 1:
                    position = mod((personPosition + 1 + populationSqrt) % populationSqrt, populationSqrt) + (((int)personPosition / populationSqrt) * populationSqrt);
                    break;
                case 2:
                    position = mod(personPosition - populationSqrt, population);
                    break;
                case 3:
                    position = mod(personPosition + populationSqrt, population);
                    break;
            }
            return position;
        }

        public int[] talk(int[] people, int position, int personPosition)
        {
            Random random = new Random();
            int rand = random.Next(2);
            if (rand == 1)
            {
                people[position] = people[personPosition];
            }
            return people;
        }
        public int mod(int a, int b)
        {
            return (a % b + b) % b;
        }
        public void outputGrid(int population, string[] inputParties, int[] people, int populationSqrt)
        {
            string output = "";
            for (int i = 0; i < population; i++)
            {
                output = output + inputParties[people[i]] + " ";
                if ((i + 1) % populationSqrt == 0)
                {
                    output = output + "\n";
                }
            }
            Console.WriteLine(output);
        }

        public bool logStats(int numberOfParties, int population, int[] people, string[] inputParties, int convinceCount)
        {
            double[] relativeParty = new double[numberOfParties];
            int[] absoluteParty = new int[numberOfParties];

            for(int peopleCount=0; peopleCount < population; peopleCount++)
            {
                absoluteParty[people[peopleCount]] += 1;
            }
            Console.WriteLine("----------------------------");

            for(int i=0; i<absoluteParty.Length; i++)
            {
                relativeParty[i] = (double)absoluteParty[i] / ((double)population / (double)100);
                Console.WriteLine(inputParties[i] + "   " + absoluteParty[i] + "   " + relativeParty[i] + "%");
            }

            for(int i=0; i<absoluteParty.Length; i++)
            {
                if((int)absoluteParty[i] == population)
                {
                    Console.WriteLine("----------------------------");
                    Console.WriteLine(inputParties[i] + "  has Won!!!");
                    Console.WriteLine("Nach insgesamt  " + convinceCount + "  überzeugungen");
                    return true;
                }
            }
            return false;
        }





    }
}
