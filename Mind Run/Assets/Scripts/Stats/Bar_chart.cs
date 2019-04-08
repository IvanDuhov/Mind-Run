using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using FTPClient;

public class Bar_chart : MonoBehaviour
{
    public string gameLabel;
    public Text error;

    [SerializeField] private Sprite dotSprite;
    private RectTransform graphContainer;

    [SerializeField] private Color color1;
    [SerializeField] private Color color2;

    private void Start()
    {
        StartCoroutine(WaitForTheOtherScripts());
    }

    private IEnumerator ShowIt()
    {
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath); //JSON.LoadFromJson();
        List<int> valuePointsList = new List<int>();

        // Checking for internet connection
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

        if (hasInternet)
        {
            FTP ftpClient = new FTP(@"ftp://ftp.ivanduhov.com/", "olympiad@ivanduhov.com", "SzK5gYLq(4}p");
            ftpClient.download(@"/GData.json", JSON.GDBPath);
        }
        try
        {
            GData gdb = JSON.ReadGDataFromDataPersistentPath(JSON.GDBPath);
            valuePointsList.Add(0);
            switch (gameLabel)
            {
                case "CardSort":
                    print("ok");
                    print(gdb.FocusAverage / gdb.FocusgMax);
                    valuePointsList.Add(ldb.FocusAverage * 100 / gdb.FocusgMax); valuePointsList.Add(gdb.FocusAverage * 100 / gdb.FocusgMax);
                    break;

                case "MemoryUI":
                    valuePointsList.Add((ldb.MemoryAverage * 100) / gdb.MemoryMax); valuePointsList.Add((gdb.MemoryAverage * 100) / gdb.MemoryMax);
                    break;

                case "TrainUI":
                    valuePointsList.Add(ldb.ProblemAverage * 100 / gdb.ProblemSolvingMax); valuePointsList.Add(gdb.ProblemSolvingAverage * 100 / gdb.ProblemSolvingMax);
                    break;

                case "TrueColourUI":
                    valuePointsList.Add(ldb.MentalAverage * 100 / gdb.MentalMax); valuePointsList.Add(gdb.MentalAverage * 100 / gdb.MentalMax);
                    break;

                case "WordCreationUI":
                    valuePointsList.Add(ldb.LanguageAverage * 100 / gdb.LanguageMax); valuePointsList.Add(gdb.LanguageAverage * 100 / gdb.LanguageMax);
                    break;

                default:
                    print("VIj parametura na scripta!!!");
                    valuePointsList.Add(ldb.FocusAverage * 100 / gdb.FocusgMax); valuePointsList.Add(gdb.FocusAverage * 100 / gdb.FocusgMax);
                    break;
            }

            graphContainer = transform.Find("barContainer").GetComponent<RectTransform>();

            //for (int i = 0; i < valuePointsArray.Length; i++)
            //{
            //    valuePointsList.Add(valuePointsArray[i]);
            //}

            print(valuePointsList[1]);
            print(valuePointsList[2]);
            ShowGraph(valuePointsList);
        }
        catch (Exception)
        {
            error.text = "Трябва Ви Интернет, за да е достъпна графиката.";
            print("trqbva ti net bace");
            throw;
        }

    }

    private GameObject CreateDot(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("dot", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = dotSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(50, 50);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {
        //valueList.Clear();
        //valueList.Add(0); valueList.Add(94); valueList.Add(83);

        float graphHeight = 800; // this should be qual to the delta y in the background
        float yMaximum = 100; // MaxInList(valueList); // 100f; // MaxInList(valueList); // The highest should be the max item in the list
        float xSize = 270f;

        // GameObject lastDotGameObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPosition = i * 1.5f * xSize;
            // print("yPos = " + valueList[i] + " / " + yMaximum + " = " + (valueList[i] / yMaximum) + " * " + graphHeight + " = " + (valueList[i] / yMaximum) * graphHeight);
            float yPosition = (valueList[i] / yMaximum) * graphHeight;
            if (float.IsNaN(yPosition)) // In case the yPosition is NaN, when in the CardSortHistory there are zeros
            {
                yPosition = 0;
            }
            CreateBar(new Vector2(xPosition, yPosition), xSize * 0.9f, i);
            /*
            GameObject dotGameObject = CreateDot(new Vector2(xPosition, yPosition));
            if (lastDotGameObject != null)
            {
                CreateDotConnection(lastDotGameObject.GetComponent<RectTransform>().anchoredPosition, dotGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastDotGameObject = dotGameObject;
            */
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 10f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private static int MaxInList(List<int> list)
    {
        int res = int.MinValue;

        for (int i = 0; i < list.Count; i++)
        {
            if (res < list[i])
            {
                res = list[i];
            }
        }

        return res;
    }

    private GameObject CreateBar(Vector2 graphPosition, float barWidth, int order)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPosition.x, 0f);
        rectTransform.sizeDelta = new Vector2(barWidth, graphPosition.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0);

        if (order == 1)
            gameObject.GetComponent<Image>().color = color1;
        else gameObject.GetComponent<Image>().color = color2;

        return gameObject;
    }

    IEnumerator WaitForTheOtherScripts()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ShowIt());
    }
}
