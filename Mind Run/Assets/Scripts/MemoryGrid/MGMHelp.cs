using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MGMHelp : MonoBehaviour {

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

        string key = PlayerPrefs.GetString("MG");

        if (key == "false")
        {
            play.gameObject.SetActive(false);
        }

        PlayerPrefs.DeleteKey("MG");
    }

    // Changing the text according to the video frames
    private IEnumerator TextUpdater()
    {
        hint.text = "Запомни квадтратчетата, които светват!";
        yield return new WaitForSeconds(4f);

        hint.text = "И не ги забравяй след като загаснат!";
        yield return new WaitForSeconds(3f);

        hint.text = "След което натисни върху тях!";
    }

    private IEnumerator UpdateFrames()
    {
        projector.sprite = help1;
        yield return new WaitForSeconds(waitFrame);

        projector.sprite = help2;
        yield return new WaitForSeconds(waitFrame + 1f);

        projector.sprite = help1;
        yield return new WaitForSeconds(waitFrame + 1.5f);

        projector.sprite = help3;
        yield return new WaitForSeconds(waitFrame);

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
