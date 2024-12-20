namespace WindchillCalc;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hej! Välkommen till Wind Chill beräknaren!");
        bool isRecalculating = false;
        // Här gör vi do-while loop för att köra programmet minst en gg
        do
        {
            if (!isRecalculating)
            {
                StartAlternativs();
                int alternative = GetValidAlternative();

                switch (alternative)
                {
                    case 1:
                        // Om användaren väljer 1 kommer det hoppas över till beräknignen!
                        // dvs det hoppas över till nästa block
                        break;
                    case 2:
                        Console.WriteLine("Programmet avslutas, hejdå!");
                        return; // Avsluta programmet! OBS! inte break utan return med inget!
                    default:
                        Console.WriteLine("Ogiltigt val! Välj 1 eller 2.");
                        continue;
                }
            }
            double temperature = GetValidInput("Vad är temperaturen? (Skriv i Celsius grader \u00b0C, mellan -50 och 10): ", -50, 10);
            double windSpeed = GetValidInput("Vad är vindhastigheten? (Skriv i km/h, mellan 5 och 150): ", 5, 150);

            double wct = WctCalc(temperature, windSpeed);
            Console.WriteLine($"\nBeräknad windchill är: {wct} \u00b0C");
            Console.WriteLine(WarningMessages(wct));

            Console.Write("\nVill du räkna igen? (J för ja, annan tangent för nej): ");
            string reCalculate = Console.ReadLine()?.ToLower();
            if (reCalculate == "j")
            {
                isRecalculating = true;
            }
            else
            {
                Console.WriteLine("Programmet avslutas, hejdå!");
                break;
            }
        } while (isRecalculating);
    }
    //Här kollar vi om alternativen är gilitiga dvs om det är 1 eller 2 annars fråga vi om och om
    static int GetValidAlternative()
    {
        int alternative;
        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out alternative) && (alternative == 1 || alternative == 2))
            {
                break;
            }
            Console.Write("Ogiltigt val! Välj 1 eller 2: ");
        }
        return alternative;
    }

    //Här gjorde jag ett void funktion för alternativerna
    //som jag kan återanvända utan skriva om samma kod om och om 
    static void StartAlternativs()
    {
        Console.WriteLine("\n1. Beräkna Wind chill?");
        Console.WriteLine("2. Avsluta programmet?");
        Console.Write("Välj ett av de här alternativen (1 eller 2): ");
    }

    //Det här är en funktion som tar emot tre argumenter prompt som är själva frågan och min/max
    //som är värdet som vi har för att
    //ha koll på vad användaren skriver och fråg om och om igen ifall användaren har fel
    static double GetValidInput(string prompt, double min, double max)
    {
        double value;
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
            {
                break; 
            }
            Console.WriteLine($"Värdet måste vara mellan {min} och {max}.");
        }
        return value;
    }
    //Funktion som beräknar windchillet genom att ha temperaturen och vindhastigheten som argumenter
    static double WctCalc(double temperature, double windSpeed)
    {
        return Math.Round(
            13.12 + 0.6215 * temperature - 11.37 * Math.Pow(windSpeed, 0.16) +
            0.3965 * temperature * Math.Pow(windSpeed, 0.16), 1);
    }

   static string WarningMessages(double wct)
    {
        //Här ternary operator lik if-else som ger viss varning om temperaturen övertiger -25,-35 och -60
        return wct > -25 ? "Kallt"
            : wct >= -35 ? "Mycket kallt"
            : wct >= -60 ? "Risk för frostskada"
            : "Stor risk för frostskada";
    }
}
