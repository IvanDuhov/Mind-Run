using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorkoutRecap : MonoBehaviour
{
    public Text MemoryAverage;
    public Text ProblemAverage;
    public Text LanguageAverage;
    public Text MentalAverage;
    public Text FocusAverage;

    public Text MemoryNew;
    public Text ProblemNew;
    public Text LanguageNew;
    public Text MentalNew;
    public Text FocusNew;

    public Text MemoryPer;
    public Text ProblemPer;
    public Text LanguagePer;
    public Text MentalPer;
    public Text FocusPer;

    PersonalProgram pp;

    private void Start()
    {
        pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();

        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);

        // assigning the value labels
        MemoryAverage.text = ldb.MemoryAverage.ToString();
        ProblemAverage.text = ldb.ProblemAverage.ToString();
        LanguageAverage.text = ldb.LanguageAverage.ToString();
        MentalAverage.text = ldb.MentalAverage.ToString();
        FocusAverage.text = ldb.FocusAverage.ToString();

        // showing the change
        MemoryNew.text = (ldb.MemoryAverage - pp.averageProgress[0]).ToString();
        ProblemNew.text = (ldb.ProblemAverage - pp.averageProgress[1]).ToString();
        LanguageNew.text = (ldb.LanguageAverage - pp.averageProgress[2]).ToString();
        MentalNew.text = (ldb.MentalAverage - pp.averageProgress[3]).ToString();
        FocusNew.text = (ldb.FocusAverage - pp.averageProgress[4]).ToString();

        // Calculating the % change
        float memory = Mathf.Abs((JSON.Round(((float)pp.averageProgress[0] / ldb.MemoryAverage - 1), 2) * 100));
        if (float.IsNaN(memory))
        {
            memory = 0;
        }

        if (ldb.MemoryAverage - pp.averageProgress[0] < 0)
        {
            MemoryPer.text = "-" + memory + "%";
        }
        else if (ldb.MemoryAverage - pp.averageProgress[0] >= 0)
        {
            MemoryPer.text = "";
            MemoryPer.text = "+" + memory + "%";
        }

        float problem = Mathf.Abs((JSON.Round(((float)pp.averageProgress[1] / ldb.ProblemAverage - 1), 2) * 100));
        if (float.IsNaN(problem))
        {
            problem = 0;
        }

        if (ldb.ProblemAverage - pp.averageProgress[1] < 0)
        {
            ProblemPer.text = "-" + problem + "%";
        }
        else if (ldb.ProblemAverage - pp.averageProgress[1] >= 0)
        {
            ProblemPer.text = "";
            ProblemPer.text = "+" + problem + "%";
        }

        float language = Mathf.Abs((JSON.Round(((float)pp.averageProgress[2] / ldb.LanguageAverage - 1), 2) * 100));
        if (float.IsNaN(language))
        {
            language = 0;
        }

        if (ldb.LanguageAverage - pp.averageProgress[2] < 0)
        {
            LanguagePer.text = "-" + language + "%";
        }
        else if (ldb.LanguageAverage - pp.averageProgress[2] >= 0)
        {
            LanguagePer.text = "";
            LanguagePer.text = "+" + language + "%";
        }

        float mental = Mathf.Abs((JSON.Round(((float)pp.averageProgress[3] / ldb.MentalAverage - 1), 2) * 100));
        if (float.IsNaN(mental))
        {
            mental = 0;
        }

        if (ldb.MentalAverage - pp.averageProgress[3] < 0)
        {
            MentalPer.text = "-" + mental + "%";
        }
        else if (ldb.MentalAverage - pp.averageProgress[3] >= 0)
        {
            MentalPer.text = "";
            MentalPer.text = "+" + mental + "%";
        }

        float focus = Mathf.Abs((JSON.Round(((float)pp.averageProgress[4] / ldb.FocusAverage - 1), 2) * 100));
        if (float.IsNaN(focus))
        {
            focus = 0;
        }

        if (ldb.FocusAverage - pp.averageProgress[4] < 0)
        {
            FocusPer.text = "-" + focus + "%";
        }
        else if (ldb.FocusAverage - pp.averageProgress[4] >= 0)
        {
            FocusPer.text = "";
            FocusPer.text = "+" + focus + "%";
        }
    }

}
