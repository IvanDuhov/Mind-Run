using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WordCreationUI : MonoBehaviour
{
    public Text result;
    public Text time;

    private string realTime;

    private WordManager wm;

    // UIs in the final scene
    public Text LastScoreText;
    public Text highestResult;
    private int lastScore;

    private void Start()
    {
        if (LastScoreText != null)
        {
            StartCoroutine(GoForIt());
        }

        if (result != null)
        {
            wm = FindObjectOfType<WordManager>();

            StartCoroutine(Counter());
        }
    }

    private IEnumerator GoForIt()
    {


        int result1 = PlayerPrefs.GetInt("LastWC"); print(result1);
        LastScoreText.text = result1.ToString();

        // Getting data from the previous scene
        float successRate = PlayerPrefs.GetFloat("WordCreationSuccessRate");
        successRate = JSON.Round(successRate, 2);
        print(successRate);

        // Loading the JSON
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath); // JSON.LoadFromJson():

        // Manipulating the JSON
        ldb = JSON.JsonManipulation("WordCreationUI", ldb, result1);

        // Asaning the successrate
        // ldb.CardSortSuccessRate = successRate;
        highestResult.text = "Най-висок резултат: " + ldb.WordFormationHighest;

        // Saving back the JSON
        JSON.WriteInDataPersistentPath(ldb, JSON.dataPersistentPath); //JSON.SaveToJson(ldb);

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
        JSON.JsonGlobalDataManipulation("WordCreationUI", ldb, hasInternet);

        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if (result != null)
        {
            result.text = wm.usedWords.Count.ToString() + " / " + wm.ListOfPossibleWords.Count.ToString();

            time.text = realTime;
        }
    }

    private IEnumerator Counter()
    {
        while (wm.seconds >= 0)
        {
            yield return new WaitForSeconds(1f);
            wm.seconds--;
            if ((wm.seconds <= 0) && (wm.mins != 0))
            {
                wm.mins--;
                wm.seconds = 59;
            }

            if (wm.seconds < 0)
                wm.seconds = 0;

            if (wm.seconds < 10)
                realTime = wm.mins.ToString() + ":0" + wm.seconds.ToString();
            else
                realTime = wm.mins.ToString() + ":" + wm.seconds.ToString();
        }
    }
}