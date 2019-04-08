using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphCircleManager : MonoBehaviour {

    public GameObject label;

    [SerializeField]
    float activeTime = 2f;

    public void ShowLabel(float xPos, float yPos, int score)
    {
        StopAllCoroutines();

        label.SetActive(true);
        label.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos - 50, yPos - 100);

        // If its the circle in the left it might get off screen so the X should be equal to 0 to avoid such scenario
        if (xPos - 50 <0)
        {
            label.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, yPos - 100);
        }
        if (yPos < 200)
        {
            label.GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos - 50, yPos + 100);
        }

        Text text = label.GetComponentInChildren<Text>();
        text.text = score.ToString();
    }

    public void HideLabel()
    {
        StartCoroutine(Hide());
    }

    private IEnumerator Hide()
    {
        yield return new WaitForSeconds(activeTime);
        label.SetActive(false);
    }

}
