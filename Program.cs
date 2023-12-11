using System;
using System.IO;
using ChartHelper.Parsing;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;

namespace LibraryReader {
    class Program {
        static void Main(string[] args) {
            string[] sourcePaths = FileHelper.GetAllSrtbs(FileHelper.CustomPath).ToArray();
            string output = "# a list of all songs in your library, formatted as TITLE - ARTIST - CHARTER\n";

            SRTB curr;
            int count = 0;
            //bool ranged = false;
            //int min = 0, max = 99
            //if (args.Length > 0) {
            //    min = Int32.Parse(args[0]);
            //    ranged = true;
            //}
            //if (args.Length > 1)
            //    max = Int32.Parse(args[1]);
            for (int i = 0; i < sourcePaths.Length; i++) {
                try {
                    curr = SRTB.DeserializeFromFile(sourcePaths[i]);
                    var trackInfo = curr.GetTrackInfo();
                    count++;

                    //for (int j = 0; j < trackInfo.Difficulties.Count; j++) {
                    //    if (trackInfo.Difficulties[j].Active) {
                    //        var currDiff = curr.GetTrackData(j);
                    //        if(ranged && currDiff.DifficultyRating >= min && currDiff.DifficultyRating <= max) {
                                output += trackInfo.Title + " - " + trackInfo.ArtistName + " - " + trackInfo.Charter + '\n';
                            //switch (currDiff.DifficultyType) {
                            //        case SRTB.DifficultyType.Easy: output += "Easy "; break;
                            //        case SRTB.DifficultyType.Normal: output += "Normal "; break;
                            //        case SRTB.DifficultyType.Hard: output += "Hard "; break;
                            //        case SRTB.DifficultyType.Expert: output += "Expert "; break;
                            //        case SRTB.DifficultyType.XD: output += "XD "; break;
                            //        case (SRTB.DifficultyType)7: output += "RemiXD "; break;
                            //    }
                            //}
                    //    }
                    //}
                }
                catch (Exception E) {
                    Console.Write("messed up with chart " + sourcePaths[i] + '\n');
                }
            }
            using (StreamWriter outputFile = new StreamWriter(FileHelper.CustomPath + "\\song.txt")) {
                    outputFile.Write(output);
            }
            Console.Write("listing " + count + " charts in " + FileHelper.CustomPath + "\\song.txt\n(press any key to close)");
            Console.Read();
        }
    }
}
