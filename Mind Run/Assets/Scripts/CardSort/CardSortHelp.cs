using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSortHelp : MonoBehaviour
{
    public Text hint;
    public Image projector;
    public Transform replayPanel;

    public Button play;

    public Sprite help1;
    public Sprite help2;
    public Sprite help3;
    public Sprite help4;
    public Sprite help5;
    public Sprite help6;
    public Sprite help7;

    private float waitFrame = 1.5f;

    private void Start()
    {
        StartCoroutine(TextUpdater());
        StartCoroutine(UpdateFrames());

        string key = PlayerPrefs.GetString("CS");

        if (key == "false")
        {
            play.gameObject.SetActive(false);
        }

        PlayerPrefs.DeleteKey("CS");
    }

    // Changing the text according to the video frames
    private IEnumerator TextUpdater()
    {
        hint.text = "Сортирай картите като натискаш съответно в лявата и дясната част на екрана!";
        yield return new WaitForSeconds(5.5f);

        hint.text = "Грешно сортираните карти ти носят времево наказание от 1 секунда!";
        yield return new WaitForSeconds(3.5f);

        hint.text = "Надявам се си ме разбрал, ако не, винаги можеш да ме помолиш да ти обясня отново. Успех!";
    }

    private IEnumerator UpdateFrames()
    {
        projector.sprite = help1;
        yield return new WaitForSeconds(waitFrame);

        projector.sprite = help2;
        yield return new WaitForSeconds(waitFrame);

        projector.sprite = help3;
        yield return new WaitForSeconds(waitFrame);

        projector.sprite = help4;
        yield return new WaitForSeconds(waitFrame);

        projector.sprite = help5;
        yield return new WaitForSeconds(waitFrame);

        projector.sprite = help6;
        yield return new WaitForSeconds(waitFrame);

        projector.sprite = help7;
        yield return new WaitForSeconds(waitFrame);

        replayPanel.gameObject.SetActive(true);
    }

    public void RePlay()
    {
        StartCoroutine(TextUpdater());
        StartCoroutine(UpdateFrames());

        replayPanel.gameObject.SetActive(false);
    }


}
