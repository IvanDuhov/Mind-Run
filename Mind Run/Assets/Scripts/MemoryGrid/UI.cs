using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text turns;
    public Text score;
    public Text highestScore;

    // UIs in the final scene
    public Text lastScoreText;
    private int finalScore;

    private MemoryGridManager manager;

	void Start ()
    {
        if (lastScoreText != null)
        {
            StartCoroutine(GoForIt());
        }

        if (turns != null)
        {
            manager = FindObjectOfType<MemoryGridManager>();

            turns.text = "Рунд: " + manager.currentTurn + " / " + manager.totalTurns;
            score.text = "Резултат: " + manager.score;
        }
    }
	
    private IEnumerator GoForIt()
    {


        // Get the data from the previous scene
        int result = PlayerPrefs.GetInt("LastMGM");
        lastScoreText.text = result.ToString();

        // Loading the JSON
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath); // JSON.LoadFromJson();

        if (ldb == null)
        {
            lastScoreText.text = "Doesnt fint the JSON file ...";
        }

        // Updating the data depending on the result and the type of game
        ldb = JSON.JsonManipulation("MemoryUI", ldb, result);

        // Assigning the highest score
        highestScore.text = "Най-висок резултат: " + ldb.MemoryGridHighest;

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
        JSON.JsonGlobalDataManipulation("MemoryUI", ldb, hasInternet);

        PlayerPrefs.DeleteAll();
    }

	void Update ()
    {
        if (turns != null)
        {
            turns.text = "Рунд: " + manager.currentTurn + " / " + manager.totalTurns;
            score.text = "Резултат: " + manager.score;
        }
    }
}
