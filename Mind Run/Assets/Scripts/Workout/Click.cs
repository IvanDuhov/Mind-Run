using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Click : MonoBehaviour
{
    public Image button;

    public Sprite MemoryGridSprite;
    public Sprite CardSortSprite;
    public Sprite TrueColourSprite;
    public Sprite TrainSprite;
    public Sprite WordCreationSprite;

    public void OnMouseDown()
    {
        PersonalProgram pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();

        if (button.sprite == MemoryGridSprite)
        {
            if (pp.Schedule[pp.currentGame] != "Memory grid")
            {
                PlayerPrefs.SetString("MG", "false");
            }

            SceneManager.LoadScene("MGMHelp");
        }
        else if (button.sprite == CardSortSprite)
        {
            if (pp.Schedule[pp.currentGame] != "CardSort")
            {
                PlayerPrefs.SetString("CS", "false");
            }

            SceneManager.LoadScene("CardSortHelp");
        }
        else if (button.sprite == TrainSprite)
        {
            if (pp.Schedule[pp.currentGame] != "Train")
            {
                PlayerPrefs.SetString("T", "false");
            }

            SceneManager.LoadScene("TrainHelp");
        }
        else if (button.sprite == TrueColourSprite)
        {
            if (pp.Schedule[pp.currentGame] != "True colour")
            {
                PlayerPrefs.SetString("TC", "false");
            }

            SceneManager.LoadScene("TrueColourHelp");
        }
        else if (button.sprite == WordCreationSprite)
        {
            SceneManager.LoadScene("WordCreationHelp");
        }
    }

}
