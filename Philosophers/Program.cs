namespace Philosophers;

public static class MainClass {
    public static void Main() {
        Thread philosopher0 = new Thread(() =>
            Philosopher.CreatePhilosopher(
                "Philosopher 0",
                Philosopher.Forks[0],
                Philosopher.Forks[1]));

        Thread philosopher1 = new Thread(() =>
            Philosopher.CreatePhilosopher(
                "Philosopher 1",
                Philosopher.Forks[1],
                Philosopher.Forks[2]));

        Thread philosopher2 = new Thread(() =>
            Philosopher.CreatePhilosopher(
                "Philosopher 2",
                Philosopher.Forks[2],
                Philosopher.Forks[3]));

        Thread philosopher3 = new Thread(() =>
            Philosopher.CreatePhilosopher(
                "Philosopher 3",
                Philosopher.Forks[3],
                Philosopher.Forks[4]));

        Thread philosopher4 = new Thread(() =>
            Philosopher.CreatePhilosopher(
                "Philosopher 4",
                Philosopher.Forks[4],
                Philosopher.Forks[0]));
        
        philosopher0.Start();
        philosopher1.Start();
        philosopher2.Start();
        philosopher3.Start();
        philosopher4.Start();
        
    }
}

public static class Philosopher {
    private static Mutex Mut = new();
    private static readonly Random Rnd = new();
    public static string[] Forks = { "0", "0", "0", "0", "0" };

    internal static void CreatePhilosopher(string name, string fork0, string fork1) {
        while (true) {
            Eat(name, fork0, fork1);
            Think(name);
            Thread.Sleep(100 / 15);
        }
    }

    private static void Think(string name) {
        Console.WriteLine($"{name} is thinking.");
        Thread.Sleep(Rnd.Next(500, 5000));
    }

    private static void Eat(string name, string fork0, string fork1) {
        if (fork1 == "1" && fork0 == "1") {
            Console.WriteLine($"{name} is unable to eat at the moment");
        }
        else {
            TakeForks(name, fork0, fork1);
        }
    }

    private static void TakeForks(string name, string fork0, string fork1) {
        lock (fork0) {
            lock (fork1) {
                fork0 = "1";
                fork1 = "1";
                Console.WriteLine($"{name} is eating.");
                Thread.Sleep(3000);
            }
        }
    }

    private static void PutForks(string fork0, string fork1) {
        Monitor.Exit(fork0);
        Monitor.Exit(fork1);
    }
}