using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using FTPClient;

public class DatabaseCreator : MonoBehaviour
{
    private void Awake()
    {
        try
        {
            LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);
            print(JSON.dataPersistentPath);
        }
        catch (Exception)
        {
            LData ldbNew = new LData();

            ldbNew.FocusHistory = new int[5];
            ldbNew.LanguageHistory = new int[5];
            ldbNew.WordFormationHistory = new int[5];
            ldbNew.MemoryHistory = new int[5];
            ldbNew.MemoryGridHistory = new int[5];
            ldbNew.ProblemHistory = new int[5];
            ldbNew.TrainHistory = new int[5];
            ldbNew.CardSortHistory = new int[5];
            ldbNew.MentalHistory = new int[5];
            ldbNew.TrueColourHistory = new int[5];

            ldbNew.tutorialMenu = false;

            JSON.WriteInDataPersistentPath(ldbNew, JSON.dataPersistentPath);
            print("Database sucessfuly created! in the dataPersistentPath");
            throw;
        }

        StartCoroutine(DownloadGlobalDataBase());
    }

    private IEnumerator DownloadGlobalDataBase()
    {
        // Checking for internet connection
        UnityWebRequest www = UnityWebRequest.Get("http://google.com");
        yield return www.SendWebRequest();

        bool hasInternet = false;

        if (www.isNetworkError || www.isHttpError || www == null)
        {
        }
        else
        {
            hasInternet = true;
        }

        if (hasInternet)
        {
            FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
            ftpClient.download(@"/GData.json", JSON.GDBPath);
            print("GData downloaded!");
        }
    }
}