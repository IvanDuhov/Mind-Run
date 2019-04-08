using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using FTPClient;

public class JSON : MonoBehaviour
{
    private static string[] FocusGames = { "CardSort" };
    private static string[] MemoryGames = { "MemoryUI" };
    private static string[] ProblemSolvingGames = { "TrainUI" };
    private static string[] MentalAgilityGames = { "TrueColour" };
    private static string[] LanguageGames = { "WordCreationUI" };

    private const string filename = "LData.json";
    public static string dataPersistentPath = Application.persistentDataPath + "/" + "LData.json"; //Path.Combine(Application.persistentDataPath, filename);
    public static string streamingAssets = Path.Combine(Application.streamingAssetsPath, filename);
    public static string GDBPath = Application.persistentDataPath + "/" + "GData.json";
    
    string json;

    public static LData ReadJson(string path)
    {
        //string loadData = File.ReadAllText(path);
        //return JsonUtility.FromJson<LData>(loadData);

        WWW www = new WWW(path);
        while (!www.isDone) { }
        string json = www.text;
        return JsonUtility.FromJson<LData>(json);
    }

    public static void SaveToJson(LData ldb, string path)
    {
        string dataAsJson = JsonUtility.ToJson(ldb, true); //true=pretty
        File.WriteAllText(path, dataAsJson);

        print("Data Saved to Json");
    }

    public static LData ReadFromDataPersistentPath(string path)
    {
        /*
        byte[] jsonBytes = File.ReadAllBytes(JSON.dataPersistentPath);
        string dataAsJson = Encoding.ASCII.GetString(jsonBytes);
        return JsonUtility.FromJson<LData>(dataAsJson);
        */

        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        var fileContent = File.ReadAllText(path);
        LData ldb = JsonConvert.DeserializeObject<LData>(fileContent);

        return ldb;
    }

    public static GData ReadGDataFromDataPersistentPath(string path)
    {
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        var fileContent = File.ReadAllText(path);
        GData gdb = JsonConvert.DeserializeObject<GData>(fileContent);

        return gdb;
    }

    public static void WriteInDataPersistentPath(LData ldb, string path)
    {
        //string dataAsJson = JsonUtility.ToJson(ldb, true);
        //byte[] jsonBytes = Encoding.ASCII.GetBytes(dataAsJson);

        // File.WriteAllBytes(path, jsonBytes);

        // write
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        var json = JsonConvert.SerializeObject(ldb, setting);
        File.WriteAllText(path, json);
    }

    public static void WriteInDataPersistentPath(GData gdb, string path)
    {
        var setting = new JsonSerializerSettings();
        setting.Formatting = Formatting.Indented;
        setting.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

        var json = JsonConvert.SerializeObject(gdb, setting);
        File.WriteAllText(path, json);
    }

    public static LData JsonManipulation(string gameLabel, LData ldb, int score, bool internetAccess = false)
    {
        int newAverage = 0;
        FTP ftpClient;

        if (internetAccess)
        {
            ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
            ftpClient.download(@"/GData.json", JSON.GDBPath);
        }

        // Updating each game stat
        switch (gameLabel) // Done
        {
            case "CardSort":
                // If the user actually played!
                if (score != 0)
                {
                    // If the game is played for first time
                    if (ldb.CardSortAverage == 0)
                    {
                        ldb.CardSortAverage = score;
                    }
                    if (ldb.FocusAverage == 0)
                    {
                        ldb.FocusAverage = score;
                    }

                    // Updating the all time score and count
                    ldb.CardSortScore += uint.Parse(score.ToString());
                    ldb.CardSortCount++;

                    // Checking if there is a new high score and if so updating it in the JSON
                    if (ldb.CardSortHighest < score)
                    {
                        ldb.CardSortHighest = score;
                    }

                    // Getting the progress in the average result in the main game type - Focus
                    ldb.CardSortProgress = ((float)JSON.Round((score * 1.0f - ldb.CardSortAverage * 1.0f) * 1.0f / ldb.CardSortAverage * 1.0f * 100.0f, 4));
                    //newAverage = Mathf.RoundToInt((ldb.FocusAverage * ldb.CardSortProgress / 100)) + ldb.FocusAverage;
                    newAverage = (int)(ldb.CardSortScore / ldb.CardSortCount);
                    print("The new average focus score is: " + newAverage + ", with change of " + ldb.CardSortProgress + "%.");

                    // Updating the new average score for the game
                    ldb.CardSortAverage = (int)(ldb.CardSortScore / ldb.CardSortCount);

                    // Adding the result to the game history
                    ldb.CardSortHistory = JSON.ShiftLeft(ldb.CardSortHistory);
                    ldb.CardSortHistory[ldb.CardSortHistory.Length - 1] = score;
                    print(score);
                }
                break;

            case "MemoryUI":
                print("Okay, we can get in the switch(case)!");
                // If the user actually played!
                if (score != 0)
                {
                    // If the game is played for first time
                    if (ldb.MemoryGridAverage == 0)
                    {
                        ldb.MemoryGridAverage = score;
                    }
                    if (ldb.MemoryAverage == 0)
                    {
                        ldb.MemoryAverage = score;
                    }

                    // Updating the all time score and count
                    ldb.MemoryGridScore += uint.Parse(score.ToString());
                    ldb.MemoryGridCount++;

                    // Checking if there is a new high score and if so updating it in the JSON
                    if (ldb.MemoryGridHighest < score)
                    {
                        ldb.MemoryGridHighest = score;
                    }

                    // Getting the progress in the average result in the main game type - Memory
                    ldb.MemoryGridProgress = ((float)JSON.Round((score * 1.0f - ldb.MemoryGridAverage * 1.0f) * 1.0f / ldb.MemoryGridAverage * 1.0f * 100.0f, 4));
                    // newAverage = Mathf.RoundToInt((ldb.MemoryAverage * ldb.MemoryGridProgress / 100)) + ldb.MemoryAverage;
                    //print("The new average focus score is: " + newAverage + ", with change of " + ldb.MemoryGridProgress + "%.");

                    // Updating the new average score for the game
                    ldb.MemoryGridAverage = (int)(ldb.MemoryGridScore / ldb.MemoryGridCount);
                    newAverage = ldb.MemoryGridAverage;

                    // Adding the result to the game history
                    ldb.MemoryGridHistory = JSON.ShiftLeft(ldb.MemoryGridHistory);
                    ldb.MemoryGridHistory[ldb.MemoryGridHistory.Length - 1] = score;
                }
                break;

            case "TrainUI":
                // If the game is played for first time
                if (score != 0)
                {
                    if (ldb.TrainAverage == 0)
                    {
                        ldb.TrainAverage = score;
                    }
                    if (ldb.ProblemAverage == 0)
                    {
                        ldb.ProblemAverage = score;
                    }

                    // Updating the all time score and count
                    ldb.TrainScore += uint.Parse(score.ToString());
                    ldb.TrainCount++;

                    // Checking if there is a new high score and if so updating it in the JSON
                    if (ldb.TrainHighest < score)
                    {
                        ldb.TrainHighest = score;
                    }

                    // Getting the progress in the average result in the main game type - Focus
                    ldb.TrainProgress = ((float)JSON.Round((score * 1.0f - ldb.TrainAverage * 1.0f) * 1.0f / ldb.TrainAverage * 1.0f * 100.0f, 4));
                    //newAverage = Mathf.RoundToInt((ldb.ProblemAverage * ldb.TrainProgress / 100)) + ldb.ProblemAverage;
                    //print("The new average focus score is: " + newAverage + ", with change of " + ldb.TrainProgress + "%.");

                    // Updating the new average score for the game
                    ldb.TrainAverage = (int)(ldb.TrainScore / ldb.TrainCount);
                    newAverage = ldb.TrainAverage;

                    // Adding the result to the game history
                    ldb.TrainHistory = JSON.ShiftLeft(ldb.TrainHistory);
                    ldb.TrainHistory[ldb.TrainHistory.Length - 1] = score;
                }
                break;

            case "TrueColour":
                // If the game is played for first time
                if (score != 0)
                {
                    if (ldb.TrueColourAverage == 0)
                    {
                        ldb.TrueColourAverage = score;
                    }
                    if (ldb.MentalAverage == 0)
                    {
                        ldb.MentalAverage = score;
                    }

                    // Updating the all time score and count
                    ldb.TrueColourScore += uint.Parse(score.ToString());
                    ldb.TrueColourCount++;

                    // Checking if there is a new high score and if so updating it in the JSON
                    if (ldb.TrueColourHighest < score)
                    {
                        ldb.TrueColourHighest = score;
                    }

                    // Getting the progress in the average result in the main game type - Focus
                    ldb.TrueColourProgress = ((float)JSON.Round((score * 1.0f - ldb.TrueColourAverage * 1.0f) * 1.0f / ldb.TrueColourAverage * 1.0f * 100.0f, 4));
                    //newAverage = Mathf.RoundToInt((ldb.MentalAverage * ldb.TrueColourProgress / 100)) + ldb.MentalAverage;
                    //print("The new average focus score is: " + newAverage + ", with change of " + ldb.TrueColourProgress + "%.");

                    // Updating the new average score for the game
                    ldb.TrueColourAverage = (int)(ldb.TrueColourScore / ldb.TrueColourCount);
                    newAverage = ldb.TrueColourAverage;

                    // Adding the result to the game history
                    ldb.TrueColourHistory = JSON.ShiftLeft(ldb.TrueColourHistory);
                    ldb.TrueColourHistory[ldb.TrueColourHistory.Length - 1] = score;
                }
                break;

            case "WordCreationUI":
                // If the game is played for first time
                if (score != 0)
                {
                    if (ldb.WordFormationAverage == 0)
                    {
                        ldb.WordFormationAverage = score;
                    }
                    if (ldb.LanguageAverage == 0)
                    {
                        ldb.LanguageAverage = score;
                    }

                    // Updating the all time score and count
                    ldb.WordFormationScore += uint.Parse(score.ToString());
                    ldb.WordFormationCount++;

                    // Checking if there is a new high score and if so updating it in the JSON
                    if (ldb.WordFormationHighest < score)
                    {
                        ldb.WordFormationHighest = score;
                    }

                    // Getting the progress in the average result in the main game type - Focus
                    ldb.WordFormationProgress = ((float)JSON.Round((score * 1.0f - ldb.WordFormationAverage * 1.0f) * 1.0f / ldb.WordFormationAverage * 1.0f * 100.0f, 4));
                    //newAverage = Mathf.RoundToInt((ldb.LanguageAverage * ldb.WordFormationProgress / 100)) + ldb.LanguageAverage;
                    //print("The new average focus score is: " + newAverage + ", with change of " + ldb.WordFormationProgress + "%.");

                    // Updating the new average score for the game
                    ldb.WordFormationAverage = (int)(ldb.WordFormationScore / ldb.WordFormationScore) * 100;
                    newAverage = ldb.WordFormationAverage;

                    // Adding the result to the game history
                    ldb.WordFormationHistory = JSON.ShiftLeft(ldb.WordFormationHistory);
                    ldb.WordFormationHistory[ldb.WordFormationHistory.Length - 1] = score;
                }
                break;

            default:
                break;
        }

        // Updating the main type of games stats
        #region CheckingWhatIsTheTypeOfTheGame
        if (score != 0)
        {
            if (IsInArray(gameLabel, FocusGames))
            {
                //ldb.FocusScore += uint.Parse(score.ToString());
                //ldb.FocusCount++;

                // ldb.FocusAverage = (int)(ldb.FocusScore / ldb.FocusCount);
                //print(ldb.FocusAverage);
                ldb.FocusAverage = newAverage;

                /*
                if (internetAccess)
                {
                    ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.FocusScore += newAverage;
                    gdb.FocusCount++;

                    gdb.FocusAverage = gdb.FocusScore / gdb.FocusCount;

                    if (newAverage > gdb.FocusgMax)
                    {
                        gdb.FocusgMax = newAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Focus GDB updated!");
                    print(gdb.FocusAverage);
                }
                */
                ldb.FocusHistory = JSON.ShiftLeft(ldb.FocusHistory);
                ldb.FocusHistory[ldb.FocusHistory.Length - 1] = ldb.FocusAverage;
            }
            else if (IsInArray(gameLabel, MemoryGames))
            {
                ldb.MemoryAverage = newAverage;

                /*
                if (internetAccess)
                {
                    ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.MemoryScore += newAverage;
                    gdb.MemoryCount++;

                    gdb.MemoryAverage = gdb.MemoryScore / gdb.MemoryCount;

                    if (newAverage > gdb.MemoryMax)
                    {
                        gdb.MemoryMax = newAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Memory GDB updated successful!");
                    print(gdb.MemoryAverage);
                }
                */
                ldb.MemoryHistory = JSON.ShiftLeft(ldb.MemoryHistory);
                ldb.MemoryHistory[ldb.MemoryHistory.Length - 1] = ldb.MemoryAverage;
            }
            else if (IsInArray(gameLabel, ProblemSolvingGames))
            {
                ldb.ProblemAverage = newAverage;

                /*
                if (internetAccess)
                {
                    ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.ProblemSolvingScore += newAverage;
                    gdb.ProblemSolvingCount++;

                    gdb.ProblemSolvingAverage = gdb.MemoryScore / gdb.MemoryCount;

                    if (newAverage > gdb.ProblemSolvingMax)
                    {
                        gdb.ProblemSolvingMax = newAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Problem Solving GDB updated successful!");
                    print(gdb.ProblemSolvingAverage);
                }
                */
                ldb.ProblemHistory = JSON.ShiftLeft(ldb.ProblemHistory);
                ldb.ProblemHistory[ldb.ProblemHistory.Length - 1] = ldb.ProblemAverage;
            }
            else if (IsInArray(gameLabel, MentalAgilityGames))
            {
                ldb.MentalAverage = newAverage;

                /*
                if (internetAccess)
                {
                    ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.MentalScore += newAverage;
                    gdb.MentalCount++;

                    gdb.MentalAverage = gdb.MentalScore / gdb.MentalCount;

                    if (newAverage > gdb.MentalMax)
                    {
                        gdb.MentalMax = newAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Mental Solving GDB updated successful!");
                    print(gdb.MentalAverage);
                }
                */
                ldb.MentalHistory = JSON.ShiftLeft(ldb.MentalHistory);
                ldb.MentalHistory[ldb.MentalHistory.Length - 1] = ldb.MentalAverage;
            }
            else if (IsInArray(gameLabel, LanguageGames))
            {
                ldb.LanguageScore += uint.Parse(score.ToString());
                ldb.LanguageCount++;

                /*
                if (internetAccess)
                {
                    ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.LanguageScore += newAverage;
                    gdb.LanguageCount++;

                    gdb.LanguageAverage = gdb.LanguageScore / gdb.LanguageCount;

                    if (newAverage > gdb.LanguageMax)
                    {
                        gdb.LanguageMax = newAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Language Solving GDB updated successful!");
                    print(gdb.LanguageAverage);
                }
                */
                ldb.LanguageAverage = (int)(ldb.LanguageScore / ldb.LanguageCount);
            }
            else print("Wrong Parameter!!! Check the final scene!");
        }
        #endregion

        return ldb;
    }

    public static void JsonGlobalDataManipulation(string gameLabel, LData ldb, bool internetAccess = false)
    {
        if (internetAccess)
        {
            if (IsInArray(gameLabel, FocusGames))
            {
                if (internetAccess)
                {
                    FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.FocusScore += ldb.FocusAverage;
                    gdb.FocusCount++;

                    gdb.FocusAverage = gdb.FocusScore / gdb.FocusCount;

                    if (ldb.FocusAverage > gdb.FocusgMax)
                    {
                        gdb.FocusgMax = ldb.FocusAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Focus GDB updated!");
                    print(gdb.FocusAverage);
                }
            }
            else if (IsInArray(gameLabel, MemoryGames))
            {
                if (internetAccess)
                {
                    FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.MemoryScore += ldb.MemoryAverage;
                    gdb.MemoryCount++;

                    gdb.MemoryAverage = gdb.MemoryScore / gdb.MemoryCount;

                    if (ldb.MemoryAverage > gdb.MemoryMax)
                    {
                        gdb.MemoryMax = ldb.MemoryAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Memory GDB updated successful!");
                    print(gdb.MemoryAverage);
                }
            }
            else if (IsInArray(gameLabel, ProblemSolvingGames))
            {
                if (internetAccess)
                {
                    FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.ProblemSolvingScore += ldb.ProblemAverage;
                    gdb.ProblemSolvingCount++;

                    gdb.ProblemSolvingAverage = gdb.ProblemSolvingScore / gdb.ProblemSolvingCount;

                    if (ldb.ProblemAverage > gdb.ProblemSolvingMax)
                    {
                        gdb.ProblemSolvingMax = ldb.ProblemAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Problem Solving GDB updated successful!");
                    print(gdb.ProblemSolvingAverage);
                }
            }
            else if (IsInArray(gameLabel, MentalAgilityGames))
            {
                if (internetAccess)
                {
                    FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.MentalScore += ldb.MentalAverage;
                    gdb.MentalCount++;

                    gdb.MentalAverage = gdb.MentalScore / gdb.MentalCount;

                    if (ldb.MentalAverage > gdb.MentalMax)
                    {
                        gdb.MentalMax = ldb.MentalAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Mental Solving GDB updated successful!");
                    print(gdb.MentalAverage);
                }
            }
            else if (IsInArray(gameLabel, LanguageGames))
            {
                if (internetAccess)
                {
                    FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
                    GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
                    gdb.LanguageScore += ldb.LanguageAverage;
                    gdb.LanguageCount++;

                    gdb.LanguageAverage = gdb.LanguageScore / gdb.LanguageCount;

                    if (ldb.LanguageAverage > gdb.LanguageMax)
                    {
                        gdb.LanguageMax = ldb.LanguageAverage;
                    }

                    JSON.WriteInDataPersistentPath(gdb, JSON.GDBPath);
                    ftpClient.upload("/GData.json", JSON.GDBPath);

                    print("Language Solving GDB updated successful!");
                    print(gdb.LanguageAverage);
                }
            }
            else print("Wrong Parameter!!! Check the final scene!");

            FTP asd = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
            asd.upload("/GData.json", JSON.GDBPath);
        }


    }

    public static int[] ShiftLeft(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            array[i] = array[i + 1];
        }

        return array;
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    public static bool IsInArray(string key, string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == key)
            {
                return true;
            }
        }

        return false;
    }
}