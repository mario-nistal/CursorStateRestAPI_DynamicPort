using Microsoft.Owin.Hosting;
using System;

class Program
{
    static void Main(string[] args)
    {
        int port = 9210; // Default port
        if (args.Length > 0 && int.TryParse(args[0], out int parsedPort))
        {
            port = parsedPort;
        }

        string baseAddress = $"http://*:{port}/";

        // Start OWIN host
        using (WebApp.Start<Startup>(url: baseAddress))
        {
            Console.WriteLine("Web API running at " + baseAddress);
            Console.WriteLine("Access cursor state data at " + baseAddress + "api/cursorstate");
            Console.WriteLine("Access process CPU usage data at " + baseAddress + "api/process?process={processName:Required}&pollTime={pollingTime: default = 1000ms}");
            Console.WriteLine("Access process RAM Usage data at " + baseAddress + "api/memory?memory={processName:Required}");
            Console.ReadLine();
        }
    }
}
