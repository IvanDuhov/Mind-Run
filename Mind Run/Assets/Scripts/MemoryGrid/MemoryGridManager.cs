using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MemoryGridManager : MonoBehaviour
{
    GameObject[,] tiles = new GameObject[7, 7];

    public GameObject tile;

    public Sprite selectedTileSprite;
    public Sprite userSelectedSprite;
    public Sprite userWrongTile;
    private Sprite baseTileSprite;

    public Image wrongTurn;
    public Image correctTurn;

    public Transform tilesParent;

    public Vector3 startPosition;
    public int numOfRounds = 15;
    public float offset;

    public int totalTurns;
    public int currentTurn = 1;
    public int tilesToBeSelected;
    public int tilesSelectedByThePlayer;

    public int score;

    private void Start()
    {
        SpawnTiles();

        baseTileSprite = tiles[1, 1].GetComponent<SpriteRenderer>().sprite;

        StartCoroutine(Tutorial());
    }

    void SpawnTiles() // Spawning the tiles
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject newTile = Instantiate(tile, startPosition, Quaternion.identity, tilesParent) as GameObject;

                // Setting the tile indexers
                newTile.GetComponent<Tile>().indexRow = i;
                newTile.GetComponent<Tile>().indexColumn = j;

                tiles[i, j] = newTile;

                startPosition.x += offset;
            }

            startPosition.y -= offset;
            startPosition.x -= 5 * offset;
        }
    }

    GameObject SelectRandomTile(int iDownBoundary, int iUpperBoundary, int jDownBoundary, int jUpperBoundary)
    {
        int indexI = Random.Range(iDownBoundary, iUpperBoundary);
        int indexJ = Random.Range(jDownBoundary, jUpperBoundary);

        return tiles[indexI, indexJ];
    } // Selecting random tile

    private IEnumerator Tutorial() // Tutorial starting only the first time
    {
        tilesToBeSelected = 2;
        currentTurn = 1;

        for (int i = currentTurn; i <= numOfRounds; i++)
        {
            SelectTiles(); // Selecting the tiles 
            print("Turn " + currentTurn + " has " + tilesToBeSelected + " tiles to be found!");

            ColourTheSelectedTiles(); // Coulours the selected tiles

            yield return new WaitForSeconds(1.2f); // Waits for a second so the player could see, which are the selected tiles
            EnableTileClicking();

            ResetTilesColour(); // Chnages the colour of all tiles to white

            // Loops untill the player finds all selected tiles
            while (tilesSelectedByThePlayer != CountOfSelectedTiles()) // waits for the user input - to find all selected tiles
            {
                if (IsThereWrongTile()) // Checks if the user has made a mistake
                {
                    wrongTurn.gameObject.SetActive(true);

                    ResetTilesColour();
                    break;
                }

                yield return new WaitForSeconds(0.05f);
            }

            DisableTileClicking();
            bool turnPassed = tilesSelectedByThePlayer == CountOfSelectedTiles();

            if (turnPassed)
            {
                score += 1000;
                correctTurn.gameObject.SetActive(true);
            }

            if (currentTurn == numOfRounds)
            {
                PlayerPrefs.SetInt("LastMGM", score);

                /*
                PersonalProgram pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();
                if (pp.isPlaying)
                {
                    pp.currentGame++;
                    print("Redirect to" + pp.Schedule[pp.currentGame]);

                    LData ldb = JSON.ReadFromDataPersistentPath(JSON.dataPersistentPath);
                    ldb = JSON.JsonManipulation("MemoryUI", ldb, score);
                    JSON.SaveToJson(ldb, JSON.dataPersistentPath);

                    SceneManager.LoadScene(pp.Schedule[pp.currentGame]);
                }
                */
                //else
                //{
                    SceneManager.LoadScene("MGMFinal");
                //}
            }

            yield return new WaitForSeconds(1);

            // Disactivating any UI images
            wrongTurn.gameObject.SetActive(false);
            correctTurn.gameObject.SetActive(false);

            ResetTilesColour(); // Chnages the colour of all tiles to white
            ResetTheTilesStates(); // Resets all tiles to the starting states

            yield return new WaitForSeconds(0.8f);

            tilesSelectedByThePlayer = 0;

            if (turnPassed)
            {
                tilesToBeSelected += currentTurn / 5 + 2;

                if (tilesToBeSelected >= 17)
                    tilesToBeSelected = 16;
            }
            else
            {
                tilesToBeSelected -= currentTurn / 3 - 1;

                if (tilesToBeSelected < 4)
                    tilesToBeSelected = 3;
            }

            currentTurn++;
        }

        print("Tutorial acomplished!");
    }
    
    void SelectTiles()
    {
        while (CountOfSelectedTiles() != tilesToBeSelected)
        {
            if (tilesToBeSelected <= 3)
            {
                if (!SelectRandomTile(2, 4, 1, 3).GetComponent<Tile>().selected)
                {
                    SelectRandomTile(2, 4, 1, 3).GetComponent<Tile>().selected = true;
                }
            }
            else if (tilesToBeSelected > 3 && tilesToBeSelected < 6)
            {
                if (!SelectRandomTile(1, 4, 0, 4).GetComponent<Tile>().selected)
                {
                    SelectRandomTile(1, 4, 0, 4).GetComponent<Tile>().selected = true;
                }
            }
            else if (tilesToBeSelected > 6 && tilesToBeSelected < 9)
            {
                if (!SelectRandomTile(0, 6, 0, 4).GetComponent<Tile>().selected)
                {
                    SelectRandomTile(0, 6, 0, 4).GetComponent<Tile>().selected = true;
                }
            }
            else
            {
                if (!SelectRandomTile(0, 7, 0, 5).GetComponent<Tile>().selected)
                {
                    SelectRandomTile(0, 7, 0, 5).GetComponent<Tile>().selected = true;
                }
            }

        }
    } // Selecting tiles = tilesToBeSelected

    int CountOfSelectedTiles()
    {
        int result = 0;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if ((tiles[i, j] != null) && (tiles[i, j].GetComponent<Tile>().selected))
                {
                    result++;
                }
            }
        }

        return result;
    } // Counts all selected tiles

    bool IsThereWrongTile()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if ((tiles[i, j] != null) && (tiles[i, j].GetComponent<Tile>().wrong))
                {
                    return true;
                }
            }
        }
        return false;
    }

    void ColourTheSelectedTiles()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if ((tiles[i, j] != null) && (tiles[i, j].GetComponent<Tile>().selected))
                {
                    tiles[i, j].GetComponent<SpriteRenderer>().sprite = selectedTileSprite;
                }
            }
        }
    } // Colours all selceted tiles in blue

    void ResetTheTilesStates()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[i, j] != null)
                {
                    tiles[i, j].GetComponent<Tile>().selected = false;
                    tiles[i, j].GetComponent<Tile>().wrong = false;
                    tiles[i, j].GetComponent<Tile>().done = false;
                    tiles[i, j].GetComponent<Tile>().blocked = false;
                }
            }
        }
    } // Reseting the states: selected, done and wrong of each tile

    void ResetTilesColour()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[i, j] != null)
                {
                    tiles[i, j].GetComponent<SpriteRenderer>().sprite = baseTileSprite;
                }
            }
        }
    } // Resets the colout of all tiles to white

    void DisableTileClicking()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[i, j] != null)
                {
                    tiles[i, j].GetComponent<BoxCollider>().enabled = false;
                }
            }
        }
    }

    void EnableTileClicking()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                if (tiles[i, j] != null)
                {
                    tiles[i, j].GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }
}
