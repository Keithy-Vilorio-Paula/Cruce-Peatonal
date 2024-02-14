using System;
using System.Threading;

class CrosswalkSemaphore
{
    private SemaphoreSlim pedestrianSemaphore = new SemaphoreSlim(1); // Semáforo para peatones
    private SemaphoreSlim vehicleSemaphore = new SemaphoreSlim(1);    // Semáforo para vehículos

    // Método para que los peatones crucen
    public void CrossPedestrian()
    {
        pedestrianSemaphore.Wait(); // Bloquea el semáforo de los peatones
        Console.WriteLine("El peatón cruza...");
        Thread.Sleep(5000); // Simula el tiempo que tarda un peatón en cruzar
        Console.WriteLine("El peatón ha cruzado.");
        pedestrianSemaphore.Release(); // Libera el semáforo de los peatones
    }

    // Método para que los vehículos pasen
    public void PassVehicle()
    {
        vehicleSemaphore.Wait(); // Bloquea el semáforo de los vehículos
        Console.WriteLine("El vehículo pasa...");
        Thread.Sleep(3000); // Simula el tiempo que tarda un vehículo en pasar
        Console.WriteLine("El vehículo ha pasado.");
        vehicleSemaphore.Release(); // Libera el semáforo de los vehículos
    }
}

class Program
{
    static void Main(string[] args)
    {
        CrosswalkSemaphore crosswalk = new CrosswalkSemaphore();

        // Se inician dos hilos para controlar los semáforos de peatones y vehículos
        Thread pedestrianThread = new Thread(() =>
        {
            while (true)
            {
                crosswalk.CrossPedestrian();
                Thread.Sleep(2000); // Espera entre cada cambio de estado de los semáforos
            }
        });

        Thread vehicleThread = new Thread(() =>
        {
            while (true)
            {
                crosswalk.PassVehicle();
                Thread.Sleep(2000); // Espera entre cada cambio de estado de los semáforos
            }
        });

        // Se inician los hilos
        pedestrianThread.Start();
        vehicleThread.Start();

        // El programa espera a que los hilos terminen (esto nunca sucederá en este ejemplo)
        pedestrianThread.Join();
        vehicleThread.Join();
    }
}
