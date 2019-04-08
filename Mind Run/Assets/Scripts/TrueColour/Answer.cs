using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Answer : MonoBehaviour
{
    [SerializeField]
    private GameObject[] colourCards; // All combinations of meaning and ink colour

    [SerializeField]
    private Transform[] points; // Here are stored the two positions - up and down

    public int score;
    public int multiplier;

    public int mins;
    public int seconds;

    public GameObject correct;
    public GameObject wrong;

    [HideInInspector]
    public int correctInARow = 0;

    private void Start()
    {
        SpawnCards();
    }

    private void Update()
    {
        if ((mins == 0) && (seconds == 0))
        {
            PlayerPrefs.SetInt("LastTC", score);
            SceneManager.LoadScene("TrueColourFinal");
        }
    }

    public void Yes()
    {
        GameObject up = GameObject.Find("up");
        GameObject down = GameObject.Find("down");

        if (up.GetComponent<ColourCard>().meaning == down.GetComponent<ColourCard>().inkcolour)
        {
            score += 20 * multiplier;
            correctInARow++;
            correct.SetActive(true);
        }
        else
        {
            correctInARow = 0;
            wrong.SetActive(true);
        }

        StartCoroutine(HideSigns());

        // Clear the cards
        Destroy(up);
        Destroy(down);

        multiplier = (int)(correctInARow / 5) + 1; // Updates the multiplier

        // spawn new cards
        SpawnCards();
    }

    public void No()
    {
        GameObject up = GameObject.Find("up");
        GameObject down = GameObject.Find("down");

        if (up.GetComponent<ColourCard>().meaning != down.GetComponent<ColourCard>().inkcolour)
        {
            score += 20 * multiplier;
            correctInARow++;
            correct.SetActive(true);
        }
        else
        {
            correctInARow = 0;
            wrong.SetActive(true);
        }

        StartCoroutine(HideSigns());

        // Clear the cards
        Destroy(up);
        Destroy(down);

        multiplier = (int)(correctInARow / 5) + 1; // Updates the multiplier

        // spawn new cards
        SpawnCards();
    }

    private void SpawnCards()
    {
        var up = Instantiate(colourCards[Random.Range(0, 25)], points[0].position, Quaternion.identity) as GameObject;
        up.name = "up";

        var down = Instantiate(colourCards[Random.Range(0, 25)], points[1].position, Quaternion.identity) as GameObject;
        down.name = "down";
    }

    private IEnumerator HideSigns()
    {
        yield return new WaitForSeconds(0.175f);
        correct.SetActive(false);
        wrong.SetActive(false);
    }

}
