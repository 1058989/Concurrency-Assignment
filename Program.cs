﻿namespace readytogo;
class Program
{
    // you MUST fill in your anme(s) and student number(s) here
    private static readonly string studentname1 = "Giovanni Vonk";
    private static readonly string studentnum1 = "1058989";
    private static readonly string studentname2 = "Marcel Boot";
    private static readonly string studentnum2 = "1038695";

    // variables for concurrency?
    // add the variables you need for concurrency here in case of need

    public static SemaphoreSlim Cooksemaphore = new(0);
    public static SemaphoreSlim Clientsemaphore = new(0);
        
    
  

    // do not add more variables after this comment.
    // feel free to change the values of the variables below to test your code
    private static readonly int total_clients = 50; // this needs to be the same as the number of cooks
    private static int total_coocks = 50; // this needs to be the same as the number of clients
    
    // do not change the code below
    public static LinkedList<Order> orders = new();
    public static LinkedList<Order> pickups = new();

    private static readonly Client[] clients = new Client[total_clients];
    private  static readonly Cook[] cooks = new Cook[total_coocks];

    public static Thread[] cookThreads = new Thread[cooks.Length];
    public static Thread[] clientThreads = new Thread[clients.Length];
    static void Main() //this method is not working properly
    {
        Console.WriteLine("Hello, we are starting our new pickup restaurant!");
        // the following code will create the clients and cooks DO NOT CHANGE THIS CODE
        // create many threads as clients,
        CreateClients();
        // create many coocks that can prepare only one dish per time
        CreateCooks();
        // each cook thread will start
        StartCooks();
        // each client thead will start 
        StartClients();
        // DO NOT CHANGE THE CODE ABOVE
        // use the space below to add your code if needed

        foreach (Thread thread in clientThreads)
        {
            thread.Join();
        }

        foreach (Thread thread in cookThreads)
        {
            thread.Join();
        }

        // DO NOT CHANGE THE CODE BELOW
        // print the number of orders placed and the number of orders consumed left in the lists
        Console.WriteLine("Orders left to work: " + orders.Count);
        Console.WriteLine("Orders left and not picked up: " + pickups.Count);
        // the lists should be empty
        Console.WriteLine("Name: " + studentname1 + " Student number: " + studentnum1);
        Console.WriteLine("Name: " + studentname2 + " Student number: " + studentnum2);
        Console.WriteLine("Press any key to exit");
        Console.ReadKey(); // this lines can be used to stop the program from exiting
    }

    private static void StartCooks() // this method is not working properly
    {   // feel free to change the code in this method if needed
        for (int i = 0; i < cooks.Length; i++)
        {
        //    Console.Write("Start COOK");
            cookThreads[i] = new Thread(cooks[i].DoWork);
            cookThreads[i].Start();
        }



    }

    private static void StartClients() // this method is not working properly
    {   // feel free to change the code in this method if needed
        for (int i = 0; i < clients.Length; i++)
        {
          //  Console.Write("Start CLIENT");
            clientThreads[i] = new Thread(clients[i].DoWork);
            clientThreads[i].Start();
        }

    }

    private static void CreateCooks()
    {   // feel free to change the code in this method if needed but not the signature
        for (int i = 0; i < total_coocks; i++)
        {
            cooks[i] = new Cook(i); // Properly initialize Cook instance with required arguments
        }
    }

    private static void CreateClients()
    {   // feel free to change the code in this method if needed but not the signature
        for (int i = 0; i < total_clients; i++)
        {
            clients[i] = new Client(i); // Properly initialize Client instance with required arguments
        }
    }
}

public class Order  //do not change this class
{
    private bool ready;

    public Order()
    {
        ready = false;
    }

    public void Done()
    {
        ready = true;

    }
    public bool isReady()
    {
        return ready;
    }
}