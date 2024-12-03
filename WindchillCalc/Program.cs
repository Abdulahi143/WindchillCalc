namespace WindchillCalc;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hej! Välkommen till Wind Chill beräknaren!");
        bool isRecalculating = false;
        bool isRunning = false;

        if (!isRecalculating)
        {
            StartAlternativs();
        }
        int alternative = int.Parse(Console.ReadLine());

        do
        {
            switch (alternative)
            {
                case 1:
                    Console.Write("Vad är temperaturen? (Skriv i celcius grader \u00b0C): "); 
                    double temperature = double.Parse(Console.ReadLine());
                    Console.Write("Vad är vindhastigheten? (Skriv i meter/sekund): ");
                    double windSpeed = double.Parse(Console.ReadLine());
                    double wct = WctCalc(temperature, windSpeed);
                    Console.WriteLine($"Beräknad windchill är: {wct} \u00b0C");
                    Console.WriteLine(WarningMessages(wct));
                    Console.Write("\nVill du räkna igen? (J för ja, annan tangent för nej): ");
                    string reCalculate = Console.ReadLine().ToLower();
                    if (reCalculate != "j")
                    {
                        Console.WriteLine("Programmet avlustas, hejdå!");
                        isRecalculating = false;
                    }
                    else
                    {
                        isRecalculating = true;
                    }
                    break;
                case 2:
                    Console.WriteLine("Programmet avslutas, hejdå!");
                    break;
            }
        } while (isRecalculating);
        
    }

    static void StartAlternativs()
    {
        Console.WriteLine("1. Beräkna Wind chill?");
        Console.WriteLine("2. Avsluta programmet?");
        Console.Write("Välj ett av de här alternativ (1 eller 2): ");
    }

    static double WctCalc(double tamperature, double windSpeed)
    {
        return  Math.Round(13.12 + 0.6215 * tamperature - 11.37 * Math.Pow(windSpeed, 0.16) + 0.3965 * tamperature * Math.Pow(windSpeed, 0.16), 1);
    }

    static string WarningMessages(double wct)
    {
        // Det här är ternary operator som liknar ungefär if/else-statement
        // är wct större än -25 ? ja då kallt annars forstätt till är 
        return wct > -25 ? "kallt"
            : wct >= -35 ? "Mycket kallt"
            : wct >= -60 ? "Risk för frostskada"
            : "Stor risk för frostskada";    
    }
}
