using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using FTPClient;

public class Stats : MonoBehaviour
{
    [SerializeField]
    int secsTillTimeout = 5;

    public RadarChart youChart;
    public RadarChart theyChart;

    public Text error;

    private bool hasInternet = false;

    void Awake()
    {
        // StartCoroutine(ShowRadar());
        StartCoroutine(TimeoutTimer());
    }

    private IEnumerator TimeoutTimer()
    {
        hasInternet = false;
        StartCoroutine(ShowRadar());

        yield return new WaitForSeconds(secsTillTimeout);
        print(hasInternet);

        if (!hasInternet)
        {
            StopCoroutine(ShowRadar());
            ShowRadarGraphOffline();
        }
    }

    private IEnumerator ShowRadar()
    {
        hasInternet = false;

        UnityWebRequest www = UnityWebRequest.Get("http://google.com");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError || www == null)
        {
            hasInternet = false;
        }
        else
        {
            hasInternet = true;
        }

        if (hasInternet)
        {
            FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");

            ftpClient.download(@"/GData.json", JSON.GDBPath);

            GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
            LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);

            float memoryA = (float)ldb.MemoryAverage / (gdb.MemoryMax + 0.0001f);
            float problemA = (float)ldb.ProblemAverage / (gdb.ProblemSolvingMax + 0.0001f);
            float languageA = (float)ldb.LanguageAverage / (gdb.LanguageMax + 0.0001f);
            float mentalA = (float)ldb.MentalAverage / (gdb.MentalMax + 0.0001f);
            float focusA = (float)ldb.FocusAverage / (gdb.FocusgMax + 0.0001f);

            if (memoryA > 1)
                memoryA = 1;

            if (problemA > 1)
                problemA = 1;

            if (languageA > 1)
                languageA = 1;

            if (mentalA > 1)
                mentalA = 1;

            if (focusA > 1)
                focusA = 1;

            youChart.SetParameters(new float[] { memoryA, problemA, languageA, mentalA, focusA });
            theyChart.SetParameters(new float[] { (float)gdb.MemoryAverage / (gdb.MemoryMax + 0.0001f), (float)gdb.ProblemSolvingAverage / (gdb.ProblemSolvingMax + 0.0001f), (float)gdb.LanguageAverage / (gdb.LanguageMax + 0.0001f), (float)gdb.MentalAverage / (gdb.MentalMax + 0.0001f), (float)gdb.FocusAverage / (gdb.FocusgMax + 0.0001f) });

            error.text = "Данните, които виждате са актуални!";
        }
        else
        {
            ShowRadarGraphOffline();
        }
    }

    private void ShowRadarGraphOffline()
    {
        try
        {
            GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
            LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);

            float memoryA = (float)ldb.MemoryAverage / (gdb.MemoryMax + 0.0001f);
            float problemA = (float)ldb.ProblemAverage / (gdb.ProblemSolvingMax + 0.0001f);
            float languageA = (float)ldb.LanguageAverage / (gdb.LanguageMax + 0.0001f);
            float mentalA = (float)ldb.MentalAverage / (gdb.MentalMax + 0.0001f);
            float focusA = (float)ldb.FocusAverage / (gdb.FocusgMax + 0.0001f);

            if (memoryA > 1)
                memoryA = 1;

            if (problemA > 1)
                problemA = 1;

            if (languageA > 1)
                languageA = 1;

            if (mentalA > 1)
                mentalA = 1;

            if (focusA > 1)
                focusA = 1;

            youChart.SetParameters(new float[] { memoryA, problemA, languageA, mentalA, focusA });
            theyChart.SetParameters(new float[] { (float)gdb.MemoryAverage / (gdb.MemoryMax + 0.0001f), (float)gdb.ProblemSolvingAverage / (gdb.ProblemSolvingMax + 0.0001f), (float)gdb.LanguageAverage / (gdb.LanguageMax + 0.0001f), (float)gdb.MentalAverage / (gdb.MentalMax + 0.0001f), (float)gdb.FocusAverage / (gdb.FocusgMax + 0.0001f) });

            error.text = "Не сте свързан с Интернет. Данните, които виждате не са актуални!";
        }
        catch (Exception)
        {
            error.text = "Нуждаете се от Интернет, за да използвате модула Статистика!";
        }
    }
}
