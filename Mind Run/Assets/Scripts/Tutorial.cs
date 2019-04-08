using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public Transform introduction;
    public Transform program;
    public Transform skills;
    public Transform goToGames;

    public Transform games;
    public Transform stat;

    public Transform radarChart;
    public Transform help;

    public Transform pdfHelp;

    private void Start()
    {
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);

        if (SceneManager.GetActiveScene().name == "Menu" && !ldb.tutorialMenu)
        {
            introduction.gameObject.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Games" && !ldb.tutorialMenu)
        {
            games.gameObject.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Stats" && !ldb.tutorialMenu)
        {
            radarChart.gameObject.SetActive(true);
        }
        else if (SceneManager.GetActiveScene().name == "Help" && !ldb.tutorialMenu)
        {
            pdfHelp.gameObject.SetActive(true);
        }

        // print(ldb.tutorialMenu);
    }

    public void FirstStep()
    {
        introduction.gameObject.SetActive(false);
        program.gameObject.SetActive(true);
    }

    public void Porgram()
    {
        program.gameObject.SetActive(false);
        skills.gameObject.SetActive(true);
    }

    public void Skills()
    {
        skills.gameObject.SetActive(false);
        goToGames.gameObject.SetActive(true);
    }

    public void Stat()
    {
        games.gameObject.SetActive(false);
        stat.gameObject.SetActive(true);
    }

    public void RadarChart()
    {
        radarChart.gameObject.SetActive(false);
        help.gameObject.SetActive(true);
    }

    public void PDFHelp()
    {
        pdfHelp.gameObject.SetActive(false);

        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);

        ldb.tutorialMenu = true;

        JSON.WriteInDataPersistentPath(ldb, JSON.dataPersistentPath);

        print(ldb.tutorialMenu);
    }

    public void ExplainAgaian()
    {
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);

        ldb.tutorialMenu = false;

        JSON.WriteInDataPersistentPath(ldb, JSON.dataPersistentPath);

        SceneManager.LoadScene("Menu");
    }

}