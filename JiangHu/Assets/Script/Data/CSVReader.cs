using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CSVReaderNamespace
{
    public static class CSVReader
    {
        public static List<List<string>> SplitCsvGrid(string csvText)
        {
            List<List<string>> csvGrid = new List<List<string>>();
            StringReader reader = new StringReader(csvText);
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');
                List<string> row = new List<string>(values);
                csvGrid.Add(row);
            }
            return csvGrid;
        }
    }
}
