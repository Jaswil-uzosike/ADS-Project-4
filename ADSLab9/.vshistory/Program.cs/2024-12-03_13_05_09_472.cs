Console.WriteLine("Algorithms and Data Structures - Lab 9");

List<Double> data = ReadWriteFile.readData("dataset.csv");
HillClimbing hc = new HillClimbing(data);
hc.runHC(300);


// List<Double> data = ReadWriteFile.readData("data.csv");
// HillClimbing hc = new HillClimbing(data);
// hc.runSHC(10);