using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter : MonoBehaviour
{
    public string value;

    private WordManager wm;

    private void Start()
    {
        wm = FindObjectOfType<WordManager>();
    }

    private void OnMouseDown()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().color = wm.usedLetter;

        wm.word += value;
        wm.DisplayWord.text += value;
    }

}
