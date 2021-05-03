using System;
using System.Collections.Generic;

namespace TopTenPlaces
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\AnthonyChiara\Downloads\csharp-collections-beginning\Pop by Largest Final.csv";
            //may have to change if download from repo onto new computer
            CVSReader ourReader = new CVSReader(filePath);

            List<Country> countries = ourReader.ReadAllCountries();
            ourReader.RemoveCommaCountries(countries);

            Console.Write("Enter number of countries to display.");
            bool inputIsInteger = int.TryParse(Console.ReadLine(), out int userInput);
            if(!inputIsInteger || userInput <=0)
            {
                Console.WriteLine("You gotta put in a postive number. I'm leaving you.");
                return;
            }

            int maxToDisplay = userInput;
            for(int i=0; i<maxToDisplay;i++)
            {
                if (i > 0 && i % maxToDisplay == 0) //condition helps countries come out as batchesyou
                {
                    Console.WriteLine("Hit enter to continue another batch, anything else to quit.");
                    if (Console.ReadLine() != "")
                        break;
                }
                var theCountry = countries[i];//a country is equal to an index that's in a List of countries of type Country
                Console.WriteLine($"{i+1}: {PopulationFormatter.FormatPopulation(theCountry.Population).PadLeft(15)}: {theCountry.Name}");

                

                //enumarating backwards
                /*
                 for (int i= countries.Count - 1; i>=0; i--)
                {
                    int displayIndex = countries.Count - 1 - i;
                    if (displayIndex > 0 && (displayIndex % maxToDisplay == 0))
                {
                     Console.WriteLine("Hit enter to continue another batch, anything else to quit.");
                     if (Console.ReadLine() != "")
                        break;
                }
                    var theCountry = countries[i];//a country is equal to an index that's in a List of countries of type Country

                    Console.WriteLine($"{displayIndex+1}: {PopulationFormatter.FormatPopulation(theCountry.Population).PadLeft(15)}: {theCountry.Name}");
                 */
            }
            Console.WriteLine($"{countries.Count} countries.");
        }
    }
}
