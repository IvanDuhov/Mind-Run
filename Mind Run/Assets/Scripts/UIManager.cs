using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Transform infoMenu;

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void IUnderstand()
    {
        infoMenu.gameObject.SetActive(false);
    }

    public void PlayMemoryGrid()
    {
        SceneManager.LoadScene("Memory grid");
    }

    public void PlayTrain()
    {
        SceneManager.LoadScene("Train");
    }

    public void GoToGames()
    {
        PersonalProgram pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();

        if (pp.isPlaying && pp != null)
        {
            SceneManager.LoadScene("Workout");
            pp.currentGame++;
        }
        else
        {
            SceneManager.LoadScene("Games");
            Destroy(GameObject.Find("GameObject"));
        }
    }

    public void GoToHelp()
    {
        SceneManager.LoadScene("Help");
    }

    public void OpenPDFHelp()
    {
        print("should be ok!");

        StartCoroutine(ReadPDF());

    }

    private IEnumerator ReadPDF()
    {
        string path = Application.streamingAssetsPath + "/Help.pdf";
        string savePath = Application.persistentDataPath;

        WWW www = new WWW(path);
        yield return www;
        string error = www.error;

        byte[] bytes = www.bytes;
        try
        {
            File.WriteAllBytes(savePath + "/Help.pdf", bytes);
            error = "No Errors so far";
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        Application.OpenURL(savePath + "/Help.pdf");
    }

    public void BackToGames()
    {
        PersonalProgram pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();

        if (pp.isPlaying && pp != null)
        {
            SceneManager.LoadScene("Workout");
        }
        else
        {
            SceneManager.LoadScene("Games");
            Destroy(GameObject.Find("GameObject"));
        }
    }

    public void TestDataBase()
    {
        SceneManager.LoadScene("Gdata");
    }

    public void GoToMenu()
    {
        PersonalProgram pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();
        Destroy(pp.gameObject);

        SceneManager.LoadScene("Menu");
    }

    public void GoToSortCards()
    {
        SceneManager.LoadScene("CardSort");
    }

    public void GoToWordCreation()
    {
        SceneManager.LoadScene("PreWordCreation");
    }

    public void GoToTrueColour()
    {
        SceneManager.LoadScene("True colour");
    }

    public void GoToStats()
    {
        PersonalProgram pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();
        pp.isPlaying = false;
        SceneManager.LoadScene("Stats");
    }

    public void CardSortHelp()
    {
        SceneManager.LoadScene("CardSortHelp");
    }

    public void MGMHelp()
    {
        SceneManager.LoadScene("MGMHelp");
    }

    public void TrainHelp()
    {
        SceneManager.LoadScene("TrainHelp");
    }

    public void WordCreationHelp()
    {
        SceneManager.LoadScene("WordCreationHelp");
    }

    public void TrueColourHelp()
    {
        SceneManager.LoadScene("TrueColourHelp");
    }

}
