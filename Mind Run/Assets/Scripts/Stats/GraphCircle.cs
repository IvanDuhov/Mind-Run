using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GraphCircle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int score;
    public float yPos;
    public float xPos;

    private GraphCircleManager gcm;

    void Start()
    {
        gcm = FindObjectOfType<GraphCircleManager>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gcm.ShowLabel(xPos, yPos, score);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gcm.HideLabel();
    }

}
