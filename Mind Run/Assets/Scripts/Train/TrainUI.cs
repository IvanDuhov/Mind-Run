using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;
using UnityEngine.Networking;

public class TrainUI : MonoBehaviour
{
    public Text ratio;
    public Text time;

    private string realTime = "";

    public TrainSpawner ts;

    // UIs in the final scene
    public Text lastScoreText;
    public Text highestScore;
    private int lastScore;

    private void Start()
    {
        if (lastScoreText != null)
        {
            StartCoroutine(GoForIt());
        }

        if (ratio != null)
        {
            StartCoroutine(Counter());
        }
    }

    private IEnumerator GoForIt()
    {


        // Get the data from the previous scene
        int result = PlayerPrefs.GetInt("LastTrain");
        lastScoreText.text = result.ToString();

        // Loading the JSON
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath); // JSON.LoadFromJson();

        if (ldb == null)
        {
            lastScoreText.text = "Doesnt fint the JSON file ...";
        }

        // Updating the data depending on the result and the type of game
        ldb = JSON.JsonManipulation("TrainUI", ldb, result);

        // Assigning the highest score
        highestScore.text = "Най-висок резултат: " + ldb.TrainHighest;

        // Saving back the JSON
        JSON.SaveToJson(ldb, JSON.dataPersistentPath); //JSON.SaveToJson(ldb);

        // Deleting all not relevant info in order to protect from user intervention

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
        JSON.JsonGlobalDataManipulation("TrainUI", ldb, hasInternet);

        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if (ratio != null)
        {
            ratio.text = "Резултат: " + ts.correctTrains + " / " + ts.spawnedTrains;
            time.text = "Време: " + realTime;
        }
    }

    private IEnumerator Counter()
    {
        int seconds = ts.secs;
        int mins = ts.mins;

        while (ts.loopTimeInSeconds != 0)
        {
            yield return new WaitForSeconds(1f);
            seconds--;
            if ((seconds <= 0) && (mins != 0))
            {
                mins--;
                seconds = 59;
            }

            if (seconds < 0)
                seconds = 0;

            if (seconds < 10)
                realTime = mins.ToString() + ":0" + seconds.ToString();
            else
                realTime = mins.ToString() + ":" + seconds.ToString();

            ts.mins = mins;
            ts.secs = seconds;
            seconds = ts.secs;
            mins = ts.mins;
        }
    }

}