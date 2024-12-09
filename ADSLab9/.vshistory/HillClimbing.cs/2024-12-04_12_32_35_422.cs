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

            Console.WriteLine("\n");
            Console.WriteLine($"Iteration {i + 1}");
            Console.WriteLine($"Current Solution Fitness: {currentSolution.fitness}");
            currentSolution.printSolution();

            // Make a small change to the new solution
            newSolution = smallChange(newSolution);

            Console.WriteLine($"\nNew Solution Fitness: {newSolution.fitness}");
            newSolution.printSolution();

            // Greedy Acceptance: Only accept the new solution if its fitness is lower
            if (newSolution.fitness < currentSolution.fitness)
            {
                currentSolution.copySolution(newSolution.solution);
                Console.WriteLine("New solution accepted.");
            }
            else
            {
                Console.WriteLine("New solution rejected.");
                // Restore the new solution to the current solution for consistency
                newSolution.copySolution(currentSolution.solution);
            }
        }

        Console.WriteLine($"Final Solution Fitness: {currentSolution.fitness}");
        ReadWriteFile.writeResults(result, "C:\\Users\\izunn\\OneDrive - Sheffield Hallam University\\BinPacking2\\BinPacking2\\ADSLab9\\result.csv");
        ReadWriteFile.writeSolutions(solutions, "C:\\Users\\izunn\\OneDrive - Sheffield Hallam University\\BinPacking2\\BinPacking2\\ADSLab9\\solutions.csv");
    }

    public BinPacking smallChange(BinPacking solution)
    {
        Random random = new Random();
        int weightIndex = random.Next(solution.solution.Count);
        int currentBin = solution.solution[weightIndex];
        double itemWeight = HillClimbing.data[weightIndex];

        // Step 1: Identify bins with overflow
        int targetBin = -1;

        // Look for a bin that can accommodate the weight without overflowing
        for (int i = 0; i < solution.binWeights.Count; i++)
        {
            if (i != currentBin && solution.binWeights[i] + itemWeight <= solution.binCapacity)
            {
                targetBin = i;
                break;
            }
        }

        // Step 2: If no suitable bin is found, create a new bin
        if (targetBin == -1)
        {
            solution.binWeights.Add(itemWeight); // Create a new bin
            targetBin = solution.binWeights.Count - 1;
        }
        else
        {
            // Adjust bin weights for the reassignment
            solution.binWeights[targetBin] += itemWeight;
        }

        // Remove weight from the current bin
        solution.binWeights[currentBin] -= itemWeight;

        // Step 3: Update the solution representation
        solution.solution[weightIndex] = targetBin;

        // Recalculate fitness
        solution.calculateFitness();

        return solution;
    }

}
