using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    MemoryGridManager manager;

    public bool selected = false;
    public bool done = false;
    public bool wrong = false;
    public bool blocked = false;

    public int indexRow;
    public int indexColumn;

    private void Start()
    {
        manager = FindObjectOfType<MemoryGridManager>();
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDown()
    {
        if (selected & !blocked)
        {
            GetComponent<SpriteRenderer>().sprite = manager.userSelectedSprite;
            done = true;
            blocked = true;

            manager.score += 250;
            manager.tilesSelectedByThePlayer++;
        }
        else if (!blocked)
        {
            GetComponent<SpriteRenderer>().sprite = manager.userWrongTile;
            wrong = true;
            blocked = true;
        }

    }
}