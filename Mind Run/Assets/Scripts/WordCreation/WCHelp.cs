using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WCHelp : MonoBehaviour {

    public Text hint;
    public Image projector;
    public Transform replayPanel;

    public Sprite help1;
    public Sprite help2;
    public Sprite help3;
    public Sprite help4;
    public Sprite help5;

    private float waitFrame = 1.5f;

    private void Start()
    {
        StartCoroutine(TextUpdater());
        StartCoroutine(UpdateFrames());
    }

    // Changing the text according to the video frames
    private IEnumerator TextUpdater()
    {
        hint.text = "Целта ти е да създаваш думички на английски eзик!";
        yield return new WaitForSeconds(3f);

        hint.text = "Изпиши думата като кликаш върху буквите!";
        yield return new WaitForSeconds(5f);

        hint.text = "Натисни SUBMIT за да предадеш предложението си!";
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

        projector.sprite = help5;
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
