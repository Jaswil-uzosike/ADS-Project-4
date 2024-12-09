public class HillClimbing
{
    public static List<double> data;

    public HillClimbing(List<double> d)
    {
        data = new List<double>(d);
    }

    public void runHC(BinPacking binPackingProblem, int iterations)
    {
        double[,] result = new double[iterations, 2];
        List<List<int>> solutions = new List<List<int>>();

        BinPacking currentSolution = binPackingProblem;
        BinPacking newSolution = new BinPacking(data, binPackingProblem.binCapacity);

        // Copy the current solution to the new solution
        newSolution.copySolution(currentSolution.solution);

        // Start searching for better solutions
        for (int i = 0; i < iterations; i++)
        {
            result[i, 0] = i;
            result[i, 1] = currentSolution.fitness;
            solutions.Add(new List<int>(currentSolution.solution));

            // Make a small change to the new solution
            newSolution = smallChange(newSolution);

            Console.WriteLine("\n");
            Console.WriteLine($"Iteration {i + 1}");
            Console.WriteLine($"Current Solution Fitness: {currentSolution.fitness}");
            currentSolution.printSolution();
            Console.WriteLine($"\nNew Solution Fitness: {newSolution.fitness}");
            newSolution.printSolution();

            // If the new solution is better, update the current solution
            if (newSolution.fitness < currentSolution.fitness)
            {
                currentSolution.copySolution(newSolution.solution);
            }
        }

        Console.WriteLine($"Final Solution Fitness: {currentSolution.fitness}");
        ReadWriteFile.writeResults(result, "result.csv");
        ReadWriteFile.writeSolutions(solutions, "solutions.csv");
    }

    public BinPacking smallChange(BinPacking solution)
    {
        Random random = new Random();

        // Randomly pick a weight and reassign it to a different bin
        int weightIndex = random.Next(solution.solution.Count);
        int currentBin = solution.solution[weightIndex];
        int newBin;

        // Ensure the new bin is different from the current bin
        do
        {
            newBin = random.Next(solution.binWeights.Count);
        } while (newBin == currentBin);

        solution.solution[weightIndex] = newBin;
        solution.calculateFitness();

        return solution;
    }
}
