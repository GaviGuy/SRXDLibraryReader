using System;
using System.IO;
using ChartHelper.Parsing;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace LibraryReader {
    class Program {
        static void Main(string[] args) {
            String customDir = FileHelper.CustomPath;
            if (args.Length > 1) {
                customDir = args[1];
            }
            else {
                Console.Write("enter path of custom directory (leave blank for default):\n");
                string temp = Console.ReadLine();
                if (temp.Length > 0)
                    customDir = temp;
            }
            string[] sourcePaths = FileHelper.GetAllSrtbs(customDir).ToArray();
            string output = "# a list of all songs in your library, formatted as TITLE - ARTIST - CHARTER\n";

            SRTB curr;
            int count = 0;
            if (args.Length > 0) {

            }
            for (int i = 0; i < sourcePaths.Length; i++) {
                try {
                    curr = SRTB.DeserializeFromFile(sourcePaths[i]);
                    var trackInfo = curr.GetTrackInfo();
                    count++;

                    for (int j = 0; j < trackInfo.Difficulties.Count; j++) {
                        output += trackInfo.Title + " - " + trackInfo.ArtistName + " - " + trackInfo.Charter + '\n';
                    }
                }
                catch (Exception E) {
                    Console.Write("encountered an issue with " + sourcePaths[i] + '\n');
                }
            }
            using (StreamWriter outputFile = new StreamWriter(FileHelper.CustomPath + "\\song.txt")) {
                    outputFile.Write(output);
            }
            Console.Write("listing " + count + " charts in " + FileHelper.CustomPath + "\\song.txt\n(press any key to finish)\n");
            Console.Read();
        }
    }
}
