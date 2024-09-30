using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common;

public static class SimpleTable
{
    // Första raden är kolumnrubriker, resten data
    public static void WriteTable(List<List<string>> data)
    {
        int maxCols = 0;
        foreach (var row in data)
            if (row.Count > maxCols)
                maxCols = row.Count;
        var columnWidth = new int[maxCols];
        foreach (var row in data)
            for (int i = 0; i < row.Count; i++)
                if (row[i].Length > columnWidth[i])
                    columnWidth[i] = row[i].Length;
        foreach (var row in data)
        {
            for (int i = 0; i < row.Count; i++)
            {
                Console.Write($"{row[i].PadRight(columnWidth[i])} ");
            }
            Console.WriteLine();
        }

    }
    
    public static void SaveTable(List<List<string?>> data, string fileName)
    // Kan enkelt öppnas i Excel
    {
        using StreamWriter file = new(fileName, false, Encoding.UTF8);
        foreach (var row in data)
            file.WriteLine(string.Join(";", row));
    }
}
