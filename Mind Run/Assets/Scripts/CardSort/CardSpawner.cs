using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CardSpawner : MonoBehaviour
{
    // The different card's colours
    public GameObject redCard;
    public GameObject greenCard;
    public GameObject yellowCard;
    public GameObject blueCard;

    // Queue of cards
    public Queue<GameObject> cards;
    public Color dark;

    // Starting position and position offset for each next card
    public Vector3 startingPosition;
    public Vector3 positionChange;

    // Starting scale and scale offset for each next card
    public Vector3 scale;
    public Vector3 scaleChange;

    // Keeping a sight from the users results
    public int correctInARow;

    [HideInInspector]
    public int wrongInARow;
    [HideInInspector]
    public int score;

    // Different multipliers depending on the stage
    public int multiplier;
    public int multiplierFirstStage;
    public int multiplierSecondStage;
    public float waitTime;

    // Variables used to calculate the success rate
    public float numOfCardsSpawned = 0;
    public float correctCards = 0;

    // Triggers controlling the num of the different types of card's colours
    private bool twoTypeOfCards = true;
    private bool threeTypeOfCards = false;
    private bool fourTypeOfCards = false;
    private bool trigger1 = false;
    private bool trigger2 = false;

    public ClickDetector left;
    public ClickDetector right;

    public int mins;
    public int seconds;

    [SerializeField]
    private Image[] leftHints;

    [SerializeField]
    private Image[] rightHints;

    public Sprite red;
    public Sprite green;
    public Sprite blue;
    public Sprite yellow;

    private void Start()
    {
        correctInARow = 0;
        score = 0;
        multiplier = 1;

        cards = new Queue<GameObject>();

        SpawnTheFirstCards(); // Spawning the first 2 colour 10 cards

        UpdateHints();
        StartCoroutine(SwappingColours());
    }

    private void Update()
    {
        if ((mins <= 0) && (seconds <= 0))
        {
            PlayerPrefs.SetInt("LastCS", score);
            float temp = ((float)JSON.Round((correctCards * 1.0f) * 1.0f / numOfCardsSpawned * 1.0f * 100.0f, 4));
            PlayerPrefs.SetFloat("CardSortSuccessRate", temp);
            SceneManager.LoadScene("CardSortFinal");
        }
    }

    void SpawnTheFirstCards()
    {
        GameObject newCard;

        for (int i = 0; i < 10; i++)
        {
            SpawnCard(); // Spawning the first ten cards
        }

        for (int i = 0; i < 10; i++) // Tiding the first 10 cards
        {
            newCard = cards.Dequeue();

            if (i == 0)
                newCard.GetComponent<SpriteRenderer>().color = Color.white; // Making the first card white

            newCard.transform.position = startingPosition; // Chaning the possition
            newCard.transform.localScale = scale; // Changing the scale

            // Adjusting the sorting layer
            newCard.GetComponent<SpriteRenderer>().sortingLayerName = (i + 1).ToString();

            // Updating the scale and the position for the next card
            scale -= scaleChange;
            startingPosition += positionChange;

            cards.Enqueue(newCard);
        }
    }

    private IEnumerator SwappingColours()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            SwapColoursInClickDetectors(left, right);
        }
    } // Coroutine that swaps two colours

    void SwapColoursInClickDetectors(ClickDetector detector1, ClickDetector detector2)
    {
        // Select random indexes from the aviable colours
        int leftColourIndex = Random.Range(0, CountActualColours(detector1));
        int rightColourIndex = Random.Range(0, CountActualColours(detector2));

        // Swaps two colours
        string temp = detector1.colorOptions[leftColourIndex];
        detector1.colorOptions[leftColourIndex] = detector2.colorOptions[rightColourIndex];
        detector2.colorOptions[rightColourIndex] = temp;

        // Checks if it is time to implement new type of card
        if ((multiplier >= multiplierFirstStage) && (!trigger1))
        {
            twoTypeOfCards = false;
            threeTypeOfCards = true;
            right.colorOptions[1] = "yellow";
            trigger1 = true;
        }
        else if ((multiplier >= multiplierSecondStage) && (!trigger2))
        {
            twoTypeOfCards = false;
            threeTypeOfCards = false;
            fourTypeOfCards = true;
            left.colorOptions[1] = "blue";
            trigger2 = true;
        }

        // Updates the image hints
        UpdateHints();
    }

    void UpdateHints() // Takes care for the images hinting which card which direction is
    {
        // Removes the images from all sprites in case the players is not doing good and there is need to make it easier for him with removing a card
        DisactivateHints(leftHints);
        DisactivateHints(rightHints);

        for (int i = 0; i < 2; i++)
        {
            switch (left.colorOptions[i])
            {
                case "red":
                    leftHints[i].GetComponent<Image>().sprite = red;
                    break;
                case "green":
                    leftHints[i].GetComponent<Image>().sprite = green;
                    break;
                case "blue":
                    leftHints[i].GetComponent<Image>().sprite = blue;
                    break;
                case "yellow":
                    leftHints[i].GetComponent<Image>().sprite = yellow;
                    break;
            }

            if (left.colorOptions[i] != "")
            {
                leftHints[i].gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < 2; i++)
        {
            switch (right.colorOptions[i])
            {
                case "red":
                    rightHints[i].GetComponent<Image>().sprite = red;
                    break;
                case "green":
                    rightHints[i].GetComponent<Image>().sprite = green;
                    break;
                case "blue":
                    rightHints[i].GetComponent<Image>().sprite = blue;
                    break;
                case "yellow":
                    rightHints[i].GetComponent<Image>().sprite = yellow;
                    break;
            }

            if (right.colorOptions[i] != "")
            {
                rightHints[i].gameObject.SetActive(true);
            }
        }
    }

    void DisactivateHints(Image[] hints)
    {
        for (int i = 0; i < hints.Length; i++)
        {
            hints[i].gameObject.SetActive(false);
        }
    } // Disactivates the hint images when the swap occurs

    int CountActualColours(ClickDetector detector)
    {
        int res = 0;

        for (int i = 0; i < detector.colorOptions.Length; i++)
        {
            if (detector.colorOptions[i] != "")
            {
                res++;
            }
        }

        return res;
    }

    public void SpawnCard()
    {
        GameObject card = redCard;

        // Defining the diversity of the cards depending on the users score
        if (twoTypeOfCards)
        {
            switch (Random.Range(1, 3))
            {
                case 1:
                    card = redCard;
                    break;
                case 2:
                    card = greenCard;
                    break;
            }
        }
        else if (threeTypeOfCards)
        {
            switch (Random.Range(1, 4))
            {
                case 1:
                    card = redCard;
                    break;
                case 2:
                    card = greenCard;
                    break;
                case 3:
                    card = yellowCard;
                    break;
            }
        }
        else if (fourTypeOfCards)
        {
            switch (Random.Range(1, 5))
            {
                case 1:
                    card = redCard;
                    break;
                case 2:
                    card = greenCard;
                    break;
                case 3:
                    card = yellowCard;
                    break;
                case 4:
                    card = blueCard;
                    break;
            }
        }

        var newCard = Instantiate(card, startingPosition, Quaternion.identity) as GameObject;

        newCard.transform.localScale = scale; // Changes the scale of the card for the last position in the queue
        newCard.GetComponent<SpriteRenderer>().sortingLayerName = "10"; // Chnages the sorting layer for the last position in the queue
        newCard.GetComponent<SpriteRenderer>().color = dark; // Makes the card darken

        cards.Enqueue(newCard); // Enlist the card in the queue
    } // Self explainatory
}
