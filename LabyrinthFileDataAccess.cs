using System;
using System.IO;
using System.Threading.Tasks;

namespace Labirintus.Persistence
{
    public class LabyrinthFileDataAccess
    {
        public LabyrinthTable LoadTable(String @path, int size)
        {
            

            
                using (StreamReader reader = new StreamReader(@path)) // fájl megnyitása
                {

                    String[] numbers;
                    int counter = 0;
                    LabyrinthTable table = new LabyrinthTable(size); // létrehozzuk a táblát

                    foreach (String line in System.IO.File.ReadLines(@path))
                    {
                        numbers = line.Split(',');
                        for (Int32 j = 0; j < size; j++)
                        {
                            table.SetValue(counter, j, Int32.Parse(numbers[j]));
                        }
                        counter++;
                    }



                    return table;
                }
            
            
        }
    }
}
