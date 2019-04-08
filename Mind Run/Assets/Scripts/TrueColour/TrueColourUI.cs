using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TrueColourUI : MonoBehaviour
{
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

    private Answer ans;
    private string realTime;

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

        if (dots != null)
        {
            ans = FindObjectOfType<Answer>();
            StartCoroutine(Counter());
        }
    }

    private IEnumerator GoForIt()
    {


        // Get the data from the previous scene
        int result = PlayerPrefs.GetInt("LastTC");
        lastScoreText.text = result.ToString();

        // Loading the JSON
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath); // JSON.LoadFromJson();

        if (ldb == null)
        {
            lastScoreText.text = "Doesnt fint the JSON file ...";
        }

        // Updating the data depending on the result and the type of game
        ldb = JSON.JsonManipulation("TrueColour", ldb, result);

        // Assigning the highest score
        highestScore.text = "Най-висок резултат: " + ldb.TrueColourHighest;

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
        JSON.JsonGlobalDataManipulation("TrueColour", ldb, hasInternet);

        PlayerPrefs.DeleteAll();
    }

    private void Update()
    {
        if (dots != null)
        {
            score.text = "Резултат: " + ans.score;
            multiplier.text = "x" + ans.multiplier;
            time.text = realTime;

            UpdateDots();
        }
    }

    private IEnumerator Counter()
    {
        while (ans.seconds >= 0)
        {
            yield return new WaitForSeconds(1f);
            ans.seconds--;
            if ((ans.seconds <= 0) && (ans.mins != 0))
            {
                ans.mins--;
                ans.seconds = 59;
            }

            if (ans.seconds < 0)
                ans.seconds = 0;

            if (ans.seconds < 10)
                realTime = ans.mins.ToString() + ":0" + ans.seconds.ToString();
            else
                realTime = ans.mins.ToString() + ":" + ans.seconds.ToString();
        }
    }

    private void UpdateDots()
    {
        switch (ans.correctInARow % 5)
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