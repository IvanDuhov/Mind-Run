using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class CardUI : MonoBehaviour
{
    // UIs in the game scene
    public Text score;
    public Text time;
    public Text multiplier;

    public Image dots;

    public Sprite dots0;
    public Sprite dots1;
    public Sprite dots2;
    public Sprite dots3;
    public Sprite dots4;
    public Sprite dots5;

    private CardSpawner cs;
    private string realTime;

    // Uis in the result scene
    public Text DisplayLastCardSortResult;
    public Text highestResult;
    private int result;
    private float successRate;

    public void DEBUGGING()
    {
        LData ldb = JSON.ReadJson(JSON.streamingAssets); // JSON.LoadFromJson();
        Debug.Log("JSON SHOULD BE LOADED SUCCESSFULY!");
        Debug.Log("JSON: " + ldb.CardSortHighest);
        JSON.SaveToJson(ldb, JSON.dataPersistentPath); //JSON.SaveToJson(ldb);
        Debug.Log("JSON SHOULD BE SAVED SUCCESSFULY!");
        Debug.Log("JSON PATH: " + JSON.dataPersistentPath);
    }

    private void Start()
    {
        if (DisplayLastCardSortResult != null)
        {
            StartCoroutine(GoForIt());
        }

        if (dots != null)
        {
            cs = FindObjectOfType<CardSpawner>();
            StartCoroutine(Counter());
        }
    }

    IEnumerator GoForIt() // Checks the Internet status
    {


        result = PlayerPrefs.GetInt("LastCS");
        DisplayLastCardSortResult.text = result.ToString();

        // Getting data from the previous scene
        successRate = PlayerPrefs.GetFloat("CardSortSuccessRate");
        successRate = JSON.Round(successRate, 2);
        // print(successRate);

        // Loading the JSON
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath); // JSON.LoadFromJson():

        // Manipulating the JSON
        ldb = JSON.JsonManipulation("CardSort", ldb, result);

        // Asaning the successrate
        ldb.CardSortSuccessRate = successRate;
        highestResult.text = "Най-висок резултат: " + ldb.CardSortHighest;

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
        JSON.JsonGlobalDataManipulation("CardSort", ldb, hasInternet);

        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        UpdateDots();

        if (cs != null)
        {
            time.text = realTime;
            score.text = "Резултат: " + cs.score.ToString();
            multiplier.text = "x" + cs.multiplier;
        }
    }

    private IEnumerator Counter()
    {
        if (cs != null)
        {
            while (cs.seconds >= 0)
            {
                yield return new WaitForSeconds(1f);
                cs.seconds--;
                if ((cs.seconds <= 0) && (cs.mins != 0))
                {
                    cs.mins--;
                    cs.seconds = 59;
                }

                if (cs.seconds < 0)
                    cs.seconds = 0;

                if (cs.seconds < 10)
                    realTime = cs.mins.ToString() + ":0" + cs.seconds.ToString();
                else
                    realTime = cs.mins.ToString() + ":" + cs.seconds.ToString();
            }
        }
    }

    private void UpdateDots()
    {
        if (dots != null)
        {
            switch (cs.correctInARow % 5)
            {
                case 0:
                    dots.sprite = dots1;
                    break;
                case 1:
                    dots.sprite = dots2;
                    break;
                case 2:
                    dots.sprite = dots3;
                    break;
                case 3:
                    dots.sprite = dots4;
                    break;
                case 4:
                    dots.sprite = dots5;
                    break;
            }
        }
    }

}