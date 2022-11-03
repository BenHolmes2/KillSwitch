// See https://aka.ms/new-console-template for more information

using System.Globalization;


static async Task WriteNew(List<string> lines)
{
    await File.WriteAllLinesAsync("C:\\Users\\User\\Kill Switch\\Assets\\DataTransfer\\DataTransfer\\WriteLines.txt", lines);
}
static async Task WriteAppend(List<string> lines)
{
    using StreamWriter file = new("C:\\Users\\User\\Kill Switch\\Assets\\DataTransfer\\DataTransfer\\WriteLines.txt", append: true);
    foreach (string line in lines)
    {
        await file.WriteLineAsync(line);
    }
}

string[] lines = System.IO.File.ReadAllLines(@"C:\Users\User\Kill Switch\Assets\DataTransfer\DataTransfer\GameDataTesting.txt");

// Display the file contents by using a foreach loop.



var initialList = lines.ToList();
var finalList = new List<string>();
string temp = "";

for(int i = 0; i < initialList.Count; i = i + 34) 
{ 
    temp = initialList[i+1].Remove(0,12);
    temp = temp.Replace(" (sec) ", ""); ;
    temp = temp + ",";
    finalList.Add(temp);
    for (int j= 2; j <= 9; j++) {
        temp = initialList[i + j].Remove(0, 12);
        temp = temp.Replace(" (sec) ", ""); ;
        temp = temp + ",";
        finalList[i / 34] += temp;
    }
    temp = initialList[i + 10].Remove(0, 13);
    temp = temp.Replace(" (sec) ", ""); ;
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 11].Remove(0, 13);
    temp = temp.Replace(" (sec) ", ""); ;
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 12].Remove(0, 17);
    temp = temp + ",";
    finalList[i / 34] += temp;
    for (int j = 13; j <= 21; j++)
    {
        temp = initialList[i + j].Remove(0, 18);
        temp = temp + ",";
        finalList[i / 34] += temp;
    }
    for (int j = 22; j <= 24; j++)
    {
        temp = initialList[i + j].Remove(0, 19);
        temp = temp + ",";
        finalList[i / 34] += temp;
    }
    temp = initialList[i + 25].Remove(0, 23);
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 26].Remove(0, 14);
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 27].Remove(0, 29);
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 28].Remove(0, 31);
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 29].Remove(0, 26);
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 30].Remove(0, 28);
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 31].Remove(0, 29);
    temp = temp + ",";
    finalList[i / 34] += temp;
    temp = initialList[i + 32].Remove(0, 27);
    temp = temp + ",";
    finalList[i / 34] += temp;
}


WriteAppend(finalList);












//System.Console.WriteLine("Contents of testing.txt = ");
//foreach (string line in finalList)
//{
//    // Use a tab to indent each line of the file.
//    Console.WriteLine("\t" + line);
//}

// Keep the console window open in debug mode.
Console.WriteLine("Press any key to exit.");
System.Console.ReadKey();