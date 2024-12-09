public class BinPacking : HillClimbing
{
    public List<int> solution = new List<int>();
    public int solSize = 0;
    public double fitness = 0.00;
    public List<double> binWeights = new List<double>();
    public double binCapacity;
    public double weightBinCount = 5.0; // Weighting for number of bins
    public double weightExtraSpace = 1.0; // Weighting for extra space
    public double weightOverflow = 2.5; // Heavy penalty for overflow

    public BinPacking(List<double> data, double binCapacity) : base(data)
    {
        solSize = data.Count;
        this.binCapacity = binCapacity;
        solution = generateInitialSolution();
        calculateFitness();
    }

    public void copySolution(List<int> sol)
    {
        solution = new List<int>(sol);
        calculateFitness();
    }

    public void calculateFitness()
    {
        binWeights = new List<double>();
        int maxBin = solution.Max() + 1;
        for (int i = 0; i < maxBin; i++) binWeights.Add(0);

        // Calculate bin weights
        for (int i = 0; i < solSize; i++)
        {
            binWeights[solution[i]] += data[i];
        }

        // Initialize fitness components
        double binCountPenalty = weightBinCount * maxBin;
        double extraSpacePenalty = 0.0;
        double overflowPenalty = 0.0;

        // Calculate penalties
        foreach (var binWeight in binWeights)
        {
            if (binWeight > binCapacity)
            {
                overflowPenalty += weightOverflow * (binWeight - binCapacity);
            }
            else
            {
                extraSpacePenalty += weightExtraSpace * (binCapacity - binWeight);
            }
        }

        // Combine penalties into fitness
        fitness = binCountPenalty + extraSpacePenalty + overflowPenalty;
    }

    public List<int> generateInitialSolution()
    {
        List<int> res = new List<int>();
        double totalWeight = data.Sum();
        int estimatedBins = (int)Math.Ceiling(totalWeight / binCapacity) + 5;

        List<double> binLoad = new List<double>(new double[estimatedBins]);

        int currentBin = 0;
        for (int i = 0; i < solSize; i++)
        {
            // Place the weight in the current bin
            res.Add(currentBin);
            binLoad[currentBin] += data[i];

            // Move to the next bin in round-robin fashion
            currentBin = (currentBin + 1) % estimatedBins;
        }

        return res;
    }

    public void printSolution()
    {
        int numBinsUsed = binWeights.Count;
        int numOverflowedBins = 0;
        double totalUnusedSpace = 0.0;

        // Calculate overflowed bins and total unused space
        foreach (var binWeight in binWeights)
        {
            if (binWeight > binCapacity)
            {
                numOverflowedBins++;
            }
            else
            {
                totalUnusedSpace += (binCapacity - binWeight);
            }
        }

        Console.WriteLine("Solution:");
        for (int i = 0; i < solSize; i++)
        {
            Console.WriteLine($"Weight {data[i]} -> Bin {solution[i]}");
        }

        Console.WriteLine("Bin Weights:");
        foreach (var bw in binWeights)
        {
            Console.WriteLine(bw);
        }

        //Console.WriteLine($"Fitness: {fitness}");
        Console.WriteLine($"Number of Bins Used: {numBinsUsed}");
        Console.WriteLine($"Number of Overflowed Bins: {numOverflowedBins}");
        Console.WriteLine($"Total Unused Space: {totalUnusedSpace}");
    }

}
