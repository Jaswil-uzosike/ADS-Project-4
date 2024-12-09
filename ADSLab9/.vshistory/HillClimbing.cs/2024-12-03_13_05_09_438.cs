public class HillClimbing
{
    public static List<Double> data;
    public HillClimbing(List<Double> d)
    {
        data = new List<double>(d);
    }

    public void runHC(int iter)
    {
        double [,] result = new double[iter,3];
        List<List<int>> solutions = new List<List<int>>();

        // Create a new solution
        Scales solution = new Scales();
        Scales newSol = new Scales();
        // copy the existion solution to the new solution
        newSol.copySolution(solution.solution);
        // Start searching solutions
        for (int i = 0; i<iter; i++)
        {   
            result[i,0] = i;
            result[i,1] = solution.fitness;
            result[i,2] = newSol.fitness;
            solutions.Add(solution.solution);

            // perform a small change toward the new solution
            newSol = smallChange(newSol,3);
            Console.WriteLine("Iter "+(i+1));
            Console.WriteLine("Solution Fitness "+solution.fitness);
            solution.printSol();
            Console.WriteLine("New Solution Fitness "+newSol.fitness);
            newSol.printSol();

            // If tne new solution is better than the current solution, we copy the solution from the new one to the current solution
            if (newSol.fitness<solution.fitness)
            {
                solution.copySolution(newSol.solution);
            }
        }
            Console.WriteLine("Final solution "+solution.fitness);
            ReadWriteFile.writeResults(result, "result.csv");
            ReadWriteFile.writeSolutions(solutions, "solutions.csv");
    }


    // This method is an additional exercise for Stochastic Hill Climbing, which is not part of the tutorial.
    public void runSHC(int iter)
    {
        double T = 25.0;
        double pr = 0.00;
        double treshold = 0.46;
         // Create a new solution
         Scales sol = new Scales();
         Scales newSol = new Scales();

        // copy the existion solution to the new solution
         newSol.copySolution(sol.solution);

         for (int i=0; i<iter; i++)
         {
            Console.WriteLine("Iter :"+i);
            Console.WriteLine("Current fitness :"+sol.fitness);

            // perform a small change toward the new solution
            newSol = smallChange(newSol,3);
            Console.WriteLine("New fitness :"+newSol.fitness);
            double diff_fitness = newSol.fitness-sol.fitness;
            pr = 1/(1+(Math.Pow(Math.Exp(1),diff_fitness/T)));
            Console.WriteLine("Pr :"+pr+" versus the threshold "+treshold);

            if (pr>treshold)
            {
                sol.copySolution(newSol.solution);
            }
            Console.WriteLine();
         }
        Console.WriteLine("Final fitness :"+sol.fitness);
    }

    public Scales smallChange(Scales newSol, int n)
    {
        Random r = new Random();
        for(int i=0; i<n; i++)    
        {
            int a = r.Next(newSol.solution.Count);
            if(newSol.solution[a]==0)
                newSol.solution[a] = 1;
            else    
                newSol.solution[a] = 0;        }
        newSol.calculateFitness();
        return newSol;
    }
}