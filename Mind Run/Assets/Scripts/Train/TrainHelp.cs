using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrainHelp : MonoBehaviour {

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
    public Sprite help8;
    
    private float waitFrame = 1.25f;

    private void Start()
    {
        StartCoroutine(TextUpdater());
        StartCoroutine(UpdateFrames());

        string key = PlayerPrefs.GetString("T");

        if (key == "false")
        {
            play.gameObject.SetActive(false);
        }

        PlayerPrefs.DeleteKey("T");
    }

    // Changing the text according to the video frames
    private IEnumerator TextUpdater()
    {
        hint.text = "Трябва да насочиш влака към гарата отговаряща на цвета му!";
        yield return new WaitForSeconds(5.5f);

        hint.text = "Пренасочването става чрез натискане на определените места, които пренасочват релсите!";
        yield return new WaitForSeconds(4.5f);

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

        projector.sprite = help8;
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
