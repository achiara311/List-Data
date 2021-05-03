using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace TopTenPlaces
{
    class CVSReader
    {
        private string _csvfilePath;

        public CVSReader(string csvfilePath)
        {
            this._csvfilePath = csvfilePath;
        }

        public List<Country> ReadAllCountries() //imports countries from the csv file and returns array of countries
        {

            //THIS IS HOW YOU WILL 90% OF THE TIME INSTANTIATE AN ARRAY WITHOUT KNOWING THE VALUES AT FIRST
            List<Country> countries = new List<Country>();
            //to read text files --instantiate StreamReader

            using (StreamReader streamReader = new StreamReader(_csvfilePath))
            {
                //read header line
                //reads the next line of a file and returns what it has read as a string
                streamReader.ReadLine();

                string csvOneLineAtATime;
                //ReadLine will return null if it finds that it can't read anymore data
                //because it's already reached the end of the file
                while ((csvOneLineAtATime = streamReader.ReadLine()) != null)
                {
                    countries.Add(ReadCountryFromCsvLine(csvOneLineAtATime));
                }
            }

            return countries;
        }

        public void RemoveCommaCountries(List<Country>countries)   //removing country that has a comma
        {
            //for (int i = countries.Count - 1 ; i >= 0; i--) //better off working backwards when removing specific parts in a List
            // {
            //if (countries[i].Name.Contains(','))
            // countries.RemoveAt(i);
            //}

            //Easiest
            countries.RemoveAll(x=>x.Name.Contains(','));
        }

        public Country ReadCountryFromCsvLine(string csvOneLineAtATime) //method is gonna read file one line at a time
        {
            //each line of the csv files contains the name,code,region,and population seperated by commas
            //which is why we use .Split(',')
            //new char[] {','}) is used for specific quotes around name of country in csv file; will throw error if we don't
            //string[] parts = csvLine.Split(new char[] { ',' });
            string[] parts = csvOneLineAtATime.Split(',');
            string name;
            string code;
            string region;
            string populationText;
            switch (parts.Length)
            {
                case 4: //if parts contains 4 elements, do this
                    name = parts[0];
                    code = parts[1];
                    region = parts[2];
                    populationText = parts[3];
                    break;
                case 5://if parts contains 5 elements, do this
                    name = parts[0] + ", " + parts[1]; //re-form name
                    name = name.Replace("\"", null).Trim(); //removes double quotes
                    code = parts[2];
                    region = parts[3];
                    populationText = parts[4];
                    break;
                default:
                    throw new Exception($"Can't parse country from csvLine: {csvOneLineAtATime}, mate.");
            }

            //TryParse will leave population=0 if it can't parse it
            int.TryParse(populationText, out int population); //in as string, out as an int to match param of Country object
            return new Country(name, code, region, population);
            //almost like creating a new object and returning that Country object at the end of this method
        }

    }
}