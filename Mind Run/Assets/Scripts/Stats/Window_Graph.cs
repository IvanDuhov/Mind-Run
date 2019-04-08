using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Graph : MonoBehaviour {

    public string gameLabel;

    [SerializeField] private Sprite circleSprite;
    private RectTransform graphContainer;
    private float yMaximum;

    private void Start()
    {
        StartCoroutine(WaitForTheOtherScripts());
    }

    private void ShowIt() {
        LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath); //JSON.LoadFromJson();
        int[] valuePointsArray;
        List<int> valuePointsList = new List<int>();

        switch (gameLabel)
        {
            case "CardSort":
                valuePointsArray = ldb.CardSortHistory;
                yMaximum = ldb.CardSortHighest;
                break;

            case "MemoryUI":
                valuePointsArray = ldb.MemoryGridHistory;
                yMaximum = ldb.MemoryGridHighest;
                break;

            case "TrainUI":
                valuePointsArray = ldb.TrainHistory;
                yMaximum = ldb.TrainHighest;
                break;

            case "TrueColourUI":
                valuePointsArray = ldb.TrueColourHistory;
                yMaximum = ldb.TrueColourHighest;
                break;

            case "WordCreationUI":
                valuePointsArray = ldb.WordFormationHistory;
                yMaximum = ldb.WordFormationHighest;
                break;

            case "TrueColour":
                valuePointsArray = ldb.TrueColourHistory;
                yMaximum = ldb.TrueColourHighest;
                break;

            default:
                valuePointsArray = ldb.CardSortHistory;
                yMaximum = ldb.CardSortHighest;
                break;
        }

        graphContainer = transform.Find("graphContainer").GetComponent<RectTransform>();

        for (int i = 0; i < valuePointsArray.Length; i++)
        {
            valuePointsList.Add(valuePointsArray[i]);
        }

        ShowGraph(valuePointsList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.AddComponent<GraphCircle>();
        gameObject.AddComponent<CircleCollider2D>();
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(50, 50);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList) {

        float graphHeight = 500; // this should be qual to the delta y in the background
        // the yMaximum is assigned from the local database
        // float yMaximum = MaxInList(valueList); // The highest should be the highest result ever // max item in the list
        float xSize = 310f;

        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = i * xSize;

            // print("yPos = " + valueList[i] + " / " + yMaximum + " = " + (valueList[i] / yMaximum) + " * " + graphHeight + " = " + (valueList[i] / yMaximum) * graphHeight);

            float yPosition = (valueList[i] / yMaximum) * graphHeight;

            if (float.IsNaN(yPosition)) // In case the yPosition is NaN, when in the CardSortHistory there are zeros
            {
                yPosition = 0;
            }

            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }

            circleGameObject.GetComponent<GraphCircle>().score = valueList[i];
            circleGameObject.GetComponent<GraphCircle>().yPos = yPosition;
            circleGameObject.GetComponent<GraphCircle>().xPos = xPosition;

            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB) {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1,1,1, .5f);
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

    IEnumerator WaitForTheOtherScripts()
    {
        yield return new WaitForSeconds(0.5f);
        ShowIt();
    }
}
