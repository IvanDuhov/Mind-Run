using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using FTPClient;

public class PersonalProgram : MonoBehaviour
{
    [SerializeField]
    int secsTillTimeout = 3;

    // showing if a workout session has started
    public bool isPlaying = false;

    // storage for the games and the areas, which will be in the workout session
    public string[] Schedule = new string[3];
    public string[] Areas = new string[3];

    // will be used in the final program scene for showing the progress after the workout
    public int[] averageProgress = new int[5];

    public int currentGame = 0;

    public Transform infoMenu;

    private bool hasInternet = false;

    // Fix for the double instances of the script
    private void Awake()
    {
        int personalProgram = FindObjectsOfType<PersonalProgram>().Length;

        if (personalProgram != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Starting the process for creating a workout plan
    public void Play()
    {
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);

        // Saving the values here, so it would be easier to calculate the progress in the final scene
        averageProgress[0] = ldb.MemoryAverage;
        averageProgress[1] = ldb.ProblemAverage;
        averageProgress[2] = ldb.LanguageAverage;
        averageProgress[3] = ldb.MentalAverage;
        averageProgress[4] = ldb.FocusAverage;

        StartCoroutine(StartCreatingTheProgram());
    }

    private IEnumerator StartCreatingTheProgram()
    {
        StartCoroutine(CreateTheProgram());

        yield return new WaitForSeconds(secsTillTimeout);
        print(hasInternet);

        if (!hasInternet)
        {
            StopCoroutine(CreateTheProgram());
            Create();
            print("Offline creation!");
        }

    }

    private IEnumerator CreateTheProgram() // Starting the process of chosing the games and the areas, which the use will be training
    {
        // Checking for internet connection
        UnityWebRequest www = UnityWebRequest.Get("http://google.com");
        yield return www.SendWebRequest();

        hasInternet = false;

        if (www.isNetworkError || www.isHttpError || www == null)
        {
        }
        else
        {
            hasInternet = true;
        }

        if (hasInternet)
        {
            isPlaying = true;

            FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
            ftpClient.download(@"/GData.json", JSON.GDBPath);
        }

        Create();
    }

    private void Create()
    {
        try
        {
            isPlaying = true;

            GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
            LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);

            bool MemoryGridSelected, CardSortSelected, TrueColourSelected, TrainSelected, wc;
            MemoryGridSelected = CardSortSelected = TrueColourSelected = TrainSelected = wc = false;

            for (int i = 0; i < 3; i++)
            {
                print(ldb.MemoryAverage + " | " + gdb.MemoryAverage);
                if (ldb.MemoryAverage <= gdb.MemoryAverage && !MemoryGridSelected)
                {
                    Schedule[i] = "Memory grid";
                    Areas[i] = "памет";
                    MemoryGridSelected = true;
                }
                else if (ldb.FocusAverage <= gdb.FocusAverage && !CardSortSelected)
                {
                    Schedule[i] = "CardSort";
                    Areas[i] = "концентрация";
                    CardSortSelected = true;
                }
                else if (ldb.MentalAverage <= gdb.MentalAverage && !TrueColourSelected)
                {
                    Schedule[i] = "True colour";
                    Areas[i] = "ментална ловкост";
                    TrueColourSelected = true;
                }
                else if (ldb.ProblemAverage <= gdb.ProblemSolvingAverage && !TrainSelected)
                {
                    Schedule[i] = "Train";
                    Areas[i] = "решаване на проблеми";
                    TrainSelected = true;
                }
                //else if (ldb.LanguageAverage <= gdb.LanguageAverage && !wc)
                //{
                //    Schedule[i] = "PreWordCreation";
                //    wc = true;
                //}
            }

            string combinedFix = Schedule[0] + Schedule[1] + Schedule[2];

            // Filling the program with games in case the user is the best one
            for (int i = 0; i < 3; i++)
            {
                if (Schedule[i] == "")
                {
                    if (!combinedFix.Contains("Memory grid"))
                    {
                        Schedule[i] = "Memory grid";
                        Areas[i] = "памет";
                    }
                    if (!combinedFix.Contains("CardSort"))
                    {
                        Schedule[i] = "CardSort";
                        Areas[i] = "концентрация";
                    }
                    if (!combinedFix.Contains("True colour"))
                    {
                        Schedule[i] = "True colour";
                        Areas[i] = "ментална ловкост";
                    }
                    if (!combinedFix.Contains("Train"))
                    {
                        Schedule[i] = "Train";
                        Areas[i] = "решаване на проблеми";
                    }
                }
            }

            SceneManager.LoadScene("Workout");
        }
        catch (Exception)
        {
            isPlaying = false;

            infoMenu.gameObject.SetActive(true);
            print("trqbva ti net bace");
            throw;
        }
    }
}
