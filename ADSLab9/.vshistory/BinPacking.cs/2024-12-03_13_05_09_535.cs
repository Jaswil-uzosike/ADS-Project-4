public class Scales : HillClimbing
{
    public List<int> solution = new List<int>();
    int solSize = 0;
    public double fitness = 0.00;


    public Scales() : base(data)
    {
        solSize = data.Count;
        solution = genRandomSol();
        calculateFitness();
    }

    public void copySolution(List<int> sol)
    {
        solution = new List<int>(sol);
        calculateFitness();
    }

    public void calculateFitness()
    {
        double left=0.00, right=0.00;
        for(int i=0; i<solSize; i++)
        {
            if(solution[i] == 0)
                left += data[i];
            else
                right += data[i];
        }
        fitness =  Math.Abs(left-right);
    }

    public List<int> genRandomSol ()
    {
        List<int> res = new List<int>();

        Random r = new Random();
        for (int i = 0; i < solSize; i++)
        {
            res.Add(r.Next(2));
        }
        return res; 
    }

    public void printSol()
    {
        foreach (int i in solution)
        {
            Console.Write(i+" ");
        }
        Console.WriteLine();
    }
}