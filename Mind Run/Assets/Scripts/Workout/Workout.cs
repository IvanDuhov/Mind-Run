using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Workout : MonoBehaviour
{

    public Text info;
    public Text buttonText;

    public Transform exitMenu;

    public Image[] games = new Image[3];
    public Image[] passed = new Image[3];

    public Sprite MemoryGridSprite;
    public Sprite CardSortSprite;
    public Sprite TrueColourSprite;
    public Sprite TrainSprite;
    public Sprite WordCreationSprite;

    public Sprite passedSprite;

    PersonalProgram pp;

    void Start()
    {
        pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();
        info.text = "Областите, в които ще подобряваш уменията си, са: " + pp.Areas[0] + ", " + pp.Areas[1] + " и " + pp.Areas[2] + "." + " За теб подбрахме игрите, които ще изиграеш в посочения ред: ";

        for (int i = 0; i < 3; i++)
        {
            if (pp.Schedule[i] == "Memory grid")
            {
                games[i].sprite = MemoryGridSprite;
                passed[i].sprite = MemoryGridSprite;
            }
            else if (pp.Schedule[i] == "CardSort")
            {
                games[i].sprite = CardSortSprite;
                passed[i].sprite = CardSortSprite;
            }
            else if (pp.Schedule[i] == "True colour")
            {
                games[i].sprite = TrueColourSprite;
                passed[i].sprite = TrueColourSprite;
            }
            else if (pp.Schedule[i] == "Train")
            {
                games[i].sprite = TrainSprite;
                passed[i].sprite = TrainSprite;
            }
            else if (pp.Schedule[i] == "PreWordCreation")
            {
                games[i].sprite = WordCreationSprite;
                passed[i].sprite = WordCreationSprite;
            }
        }

        if (pp.currentGame >= 4)
        {
            pp.currentGame = 2;
        }

        for (int i = 0; i < pp.currentGame; i++)
        {
            passed[i].sprite = passedSprite;
            passed[i].gameObject.SetActive(true);
        }

        if (pp.currentGame > 0)
        {
            buttonText.text = "ПРОДЪЛЖИ";
        }
    }

    public void Continue()
    {
        if (pp.currentGame == 3)
        {
            SceneManager.LoadScene("WorkoutRecap");
        }
        else
        {
            SceneManager.LoadScene(pp.Schedule[pp.currentGame]);
        }
    }

    public void RequestExit()
    {
        exitMenu.gameObject.SetActive(true);
    }

    public void ConfirmExit()
    {
        Destroy(pp.gameObject);
        SceneManager.LoadScene("Menu");
    }

    public void CancelExit()
    {
        exitMenu.gameObject.SetActive(false);
    }



}
