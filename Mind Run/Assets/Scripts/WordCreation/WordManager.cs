using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WordManager : MonoBehaviour
{
    public string word = "";
    public Color usedLetter;
    public Text DisplayWord;

    public int guessedWords = 0;

    [SerializeField]
    private GameObject[] letterObjects;

    private Letter[] letterButtons;
    private PreWordCreation pwc;

    public int mins = 0;
    public int seconds = 0;

    [SerializeField]
    private Transform[] positions;

    public List<string> usedWords;
    public List<string> ListOfPossibleWords;
    private string[] letters;

    private void Start()
    {
        pwc = FindObjectOfType<PreWordCreation>();

        ListOfPossibleWords = pwc.ListOfPossibleWords;
        StartCoroutine(UpdateTheListWithPossibleWords());

        letters = pwc.letters;

        DisplayLetters();
    }

    private void Update()
    {
        if ((mins == 0) && (seconds == 0))
        {
            PlayerPrefs.SetInt("LastWC", guessedWords);
            PlayerPrefs.SetFloat("WordCreationSuccessRate", guessedWords / ListOfPossibleWords.Count);
            Destroy(GameObject.Find("GameObject"));
            SceneManager.LoadScene("WordCreationFinal");
        }

        if (DisplayWord.text.Contains("_") && DisplayWord.text.Length == 2)
        {
            DisplayWord.text = DisplayWord.text.Replace("_", "");
        }
    }

    private void DisplayLetters()
    {
        for (int i = 0; i < 6; i++)
        {
            string currentLetter = letters[i];

            for (int j = 0; j < letterObjects.Length; j++)
            {
                if (currentLetter == letterObjects[j].name)
                {
                    Instantiate(letterObjects[j], positions[i].position, Quaternion.identity);
                }
            }
        }

        letterButtons = FindObjectsOfType<Letter>();
    }

    private IEnumerator UpdateTheListWithPossibleWords()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(1f);
            ListOfPossibleWords = pwc.ListOfPossibleWords;
        }
    }

    public void SubmitButton()
    {
        if (usedWords.Contains(word.ToLower()))
        {
            print("veche si probval s tova!");
        }
        else if (ListOfPossibleWords.Contains(word.ToLower()))
        {
            print("Bravo Pozna!");
            usedWords.Add(word.ToLower());
            guessedWords++;
        }
        else
        {
            print("kofti");
        }

        word = "";
        DisplayWord.text = "_";

        ResetLetterButtons();
    }

    public void Del()
    {
        string res = "";

        if (word.Length > 0)
        {
            for (int i = 0; i < word.Length - 1; i++)
            {
                res += word[i];
            }
        }

        word = res;
        DisplayWord.text = word;
    }

    public void DelAll()
    {
        word = "";
        DisplayWord.text = "";
        ResetLetterButtons();
    }

    private void ResetLetterButtons() // Makes the letter buttons clickable again
    {
        for (int i = 0; i < letterButtons.Length; i++)
        {
            letterButtons[i].GetComponent<BoxCollider2D>().enabled = true;
            letterButtons[i].GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}