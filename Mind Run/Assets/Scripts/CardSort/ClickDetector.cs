using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    public string side;
    public ClickDetector otherSide;

    public string[] colorOptions;

    private CardSpawner cs;
    private bool freezed = false;

    [SerializeField]
    private Color wrong;

    private bool needToWait = false;

    private void Start()
    {
        cs = FindObjectOfType<CardSpawner>();
    }

    private void OnMouseDown()
    {
        if (!freezed)
        {
            if (cs.seconds > 0)
            {
                var card = cs.cards.Dequeue(); // gets the first card from the queue

                if (ColourCheck(card.GetComponent<Card>().colour))
                {
                    cs.correctCards++;
                    cs.correctInARow++;
                    cs.wrongInARow = 0;
                    cs.score += 20 * cs.multiplier;
                }
                else
                {
                    needToWait = true;

                    cs.wrongInARow++;
                    cs.correctInARow = 0;
                }

                if (needToWait)
                {
                    // Starting the punishment process
                    StartCoroutine(PunishTime(card));
                    freezed = true;
                }
                else
                {
                    Destroy(card);

                    cs.SpawnCard(); // Spawns another card at the last position

                    MoveAllCards();

                }
                cs.numOfCardsSpawned++;
            }
        }
    }

    IEnumerator PunishTime(GameObject card)
    {
        otherSide.freezed = true;
        card.GetComponent<SpriteRenderer>().color = wrong;
        yield return new WaitForSeconds(1.5f);

        Destroy(card);

        cs.SpawnCard(); // Spawns another card at the last position

        MoveAllCards();

        needToWait = false;
        freezed = false;
        otherSide.freezed = false;
    }

    private void MoveAllCards()
    {
        cs.multiplier = (int)(cs.correctInARow / 5) + 1;

        for (int i = 0; i < 10; i++)
        {
            var card = cs.cards.Dequeue();

            if (i == 0)
                card.GetComponent<SpriteRenderer>().color = Color.white;

            card.GetComponent<SpriteRenderer>().sortingLayerName = (i + 1).ToString();

            card.transform.position -= cs.positionChange;
            card.transform.localScale += cs.scaleChange;

            cs.cards.Enqueue(card);
        }
    }

    private bool ColourCheck(string color)
    {
        for (int i = 0; i < colorOptions.Length; i++)
        {
            if ((colorOptions[i] != null) && (colorOptions[i] == color))
                return true;
        }

        return false;
    }

}
