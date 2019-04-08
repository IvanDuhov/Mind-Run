using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrueColourHelp : MonoBehaviour {

    public Text hint;
    public Image projector;
    public Transform replayPanel;

    public Button play;

    public Sprite help1;
    public Sprite help2;
    public Sprite help3;
    public Sprite help4;

    private float waitFrame = 1.5f;

    private void Start()
    {
        StartCoroutine(TextUpdater());
        StartCoroutine(UpdateFrames());

        string key = PlayerPrefs.GetString("TC");

        if (key == "false")
        {
            play.gameObject.SetActive(false);
        }

        PlayerPrefs.DeleteKey("TC");
    }

    // Changing the text according to the video frames
    private IEnumerator TextUpdater()
    {
        hint.text = "Цeлта е да определиш дали значението на горната дума, съвпада с цвета на долната!";
        yield return new WaitForSeconds(3f);
    }

    private IEnumerator UpdateFrames()
    {
        projector.sprite = help1;
        yield return new WaitForSeconds(waitFrame);

        projector.sprite = help2;
        yield return new WaitForSeconds(waitFrame + 1f);

        projector.sprite = help3;
        yield return new WaitForSeconds(waitFrame + 1.5f);

        projector.sprite = help4;
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
