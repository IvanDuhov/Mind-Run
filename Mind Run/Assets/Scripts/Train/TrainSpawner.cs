using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainSpawner : MonoBehaviour
{
    public GameObject train;

    public Color32 black;
    public Color32 red;
    public Color32 green;
    public Color32 yellow;
    public Color32 cyan;
    public Color32 purple;
    public Color32 blue;

    public int mins = 0;
    public int secs = 0;

    [HideInInspector]
    public int spawnedTrains = 0;
    [HideInInspector]
    public int correctTrains = 0;

    [SerializeField]
    private int pointsPerTrain;

    public float waitTime = 2f;
    public float waitTimeChange = 0.05f;

    [HideInInspector]
    public int loopTimeInSeconds;

    private void Start()
    {
        StartCoroutine(SpawningTrains());
    }

    private IEnumerator SpawningTrains()
    {
        loopTimeInSeconds = mins * 60 + secs;

        StartCoroutine(SecondsCounter());

        while (loopTimeInSeconds >= 0)
        {
            SpawnATrain();
            yield return new WaitForSeconds(waitTime);
        }

        PlayerPrefs.SetInt("LastTrain", correctTrains * pointsPerTrain);
        SceneManager.LoadScene("TrainFinal");
    }

    private IEnumerator SecondsCounter()
    {
        while (loopTimeInSeconds >= 0)
        {
            yield return new WaitForSeconds(1f);
            loopTimeInSeconds--;
        }
    }

    private void SpawnATrain()
    {
        GameObject newTrain;

        spawnedTrains++;

        switch (Random.Range(1, 8))
        {
            case 1:
                newTrain = Instantiate(train, transform.position, Quaternion.identity) as GameObject;
                newTrain.GetComponent<SpriteRenderer>().color = black;
                newTrain.GetComponent<Train>().colour = "black";
                break;
            case 2:
                newTrain = Instantiate(train, transform.position, Quaternion.identity) as GameObject;
                newTrain.GetComponent<SpriteRenderer>().color = red;
                newTrain.GetComponent<Train>().colour = "red";
                break;
            case 3:
                newTrain = Instantiate(train, transform.position, Quaternion.identity) as GameObject;
                newTrain.GetComponent<SpriteRenderer>().color = green;
                newTrain.GetComponent<Train>().colour = "green";
                break;
            case 4:
                newTrain = Instantiate(train, transform.position, Quaternion.identity) as GameObject;
                newTrain.GetComponent<SpriteRenderer>().color = yellow;
                newTrain.GetComponent<Train>().colour = "yellow";
                break;
            case 5:
                newTrain = Instantiate(train, transform.position, Quaternion.identity) as GameObject;
                newTrain.GetComponent<SpriteRenderer>().color = cyan;
                newTrain.GetComponent<Train>().colour = "cyan";
                break;
            case 6:
                newTrain = Instantiate(train, transform.position, Quaternion.identity) as GameObject;
                newTrain.GetComponent<SpriteRenderer>().color = purple;
                newTrain.GetComponent<Train>().colour = "purple";
                break;
            case 7:
                newTrain = Instantiate(train, transform.position, Quaternion.identity) as GameObject;
                newTrain.GetComponent<SpriteRenderer>().color = blue;
                newTrain.GetComponent<Train>().colour = "blue";
                break;
        }
    }

}
