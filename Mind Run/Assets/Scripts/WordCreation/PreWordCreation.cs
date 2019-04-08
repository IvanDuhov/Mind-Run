using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PreWordCreation : MonoBehaviour
{
    private const string URL = "https://www.dictionaryapi.com/api/v3/references/thesaurus/json/";

    #region APIKEYS
    private const string API_KEY1 = "9470866c-e45f-481b-b6dc-3debf1973de5"; // betonovozzz
    private const string API_KEY2 = "1b467453-a4d4-4f4c-aa22-6700ac638dcd"; // atsbounty1
    private const string API_KEY3 = "be46e61c-f96a-4961-b31f-0b10fea8d7d9"; // hount
    private const string API_KEY4 = "173def81-14aa-42a6-b7ca-cec0aa355fc3"; // mcpesho
    private const string API_KEY5 = "70f59928-d883-461a-b815-0b56832f7d71"; // csharper
    private const string API_KEY6 = "6e26488e-0ed2-48f7-a32d-d22d1c412ada"; // esos
    private const string API_KEY7 = "b990ce54-1ab8-4930-9cf4-2be646aab387"; // lightlord
    private const string API_KEY8 = "d1c17195-4d98-45aa-84cb-99b94a7a2972"; // mylord
    private const string API_KEY9 = "5c979f49-2756-40a8-b83a-cf25612a43a5"; // baelish
    private const string API_KEY10 = "dbecb585-02e9-49eb-845f-a2d55c4f9a33"; // esos
    #endregion

    [SerializeField]
    private int possibleWords = 0;
    [SerializeField] int countOfMinWords = 5;

    private List<string> words;
    public List<string> ListOfPossibleWords;
    private List<string> notAppropriateWords;
    public string[] letters;

    public Text label;
    public Text label2;

    private void Start()
    {
        StartCoroutine(CheckInternetAcess());

        DontDestroyOnLoad(gameObject);

        StartCoroutine(AveiabilityTest());
    }

    IEnumerator CheckInternetAcess() // Checks the Internet status
    {
        UnityWebRequest www = UnityWebRequest.Get("http://google.com");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError || www == null)
        {
            label.text = "Нуждаете се от Интернет за да играете тази игра, ще бъдете пренасочени към основното меню след 3 секунди ...";
        }
        else
        {
            // Show results as text
            Debug.Log(www.downloadHandler.text);
            label.text = "Интернет статус: наличен";
        }
    }

    IEnumerator ToMenu()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Menu");
    }

    private IEnumerator OnResponse(UnityWebRequest req, string word2) // Checks if the word exists
    {
        yield return req.SendWebRequest(); // Waiting for response from the API

        string rawdata = req.downloadHandler.text; // The indormation got thanks to the API

        if (label2 != null)
        {
            label2.text = "Намерени думи: " + ListOfPossibleWords.Count + " / 7";
        }

        if (req.downloadHandler.text.Contains("uuid"))
        {
            possibleWords++;

            ListOfPossibleWords.Add(word2.ToLower());
            // print(word2);
        }

        CheckThePossibleLetters();
    }

    private IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");
    }

    private UnityWebRequest CreateQuery(string wordForCheck) // Creates query for the API
    {
        UnityWebRequest www = UnityWebRequest.Get(((URL + wordForCheck + "?key=" + Router())).ToString());
        return www;
    }

    private void Variations(List<string> output, string str, int n, string curr)
    {
        if (curr.Length == n)
        {
            output.Add(curr);
            return;
        }

        foreach (char c in str)
            if (!curr.Contains(c.ToString()))
                Variations(output, str, n, curr + c.ToString());

    } // Gets all possible variations without repeating

    bool TestRange(int numberToCheck, int bottom, int top)
    {
        return (numberToCheck >= bottom && numberToCheck <= top);
    } // Checks if an int is in a range

    private string[] PickLetters() // Picks random 6 letters
    {
        string[] possibleLetters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "k", "l", "m", "n", "o", "p", "r", "s", "t", "u", "v", "w", "y" };
        string[] letters = new string[6];

        for (int i = 0; i < 6; i++)
        {
            int b = PickDifferentLetter(possibleLetters, letters);

            letters[i] = ChancesMapi(possibleLetters, b);
        }

        return letters;
    }

    private void CheckWords() // Checks the list with words
    {
        ListOfPossibleWords = new List<string>();
        ListOfPossibleWords.Clear();

        print("Check Words Started!");

        foreach (var word in words)
        {
            StartCoroutine(OnResponse(CreateQuery(word), word));
        }
    }

    private string ChancesMapi(string[] possibleLetters, int b)
    {
        if (TestRange(b, 1, 20)) // a -> 11%
        {
            return possibleLetters[0];
        }
        else if (TestRange(b, 21, 23)) // b -> 1%
        {
            return possibleLetters[1];
        }
        else if (TestRange(b, 24, 28)) // c -> 3%
        {
            return possibleLetters[2];
        }
        else if (TestRange(b, 29, 36)) // d -> 4%
        {
            return possibleLetters[3];
        }
        else if (TestRange(b, 37, 62)) // e -> 14%
        {
            return possibleLetters[4];
        }
        else if (TestRange(b, 63, 63)) // f -> 0.5%
        {
            return possibleLetters[5];
        }
        else if (TestRange(b, 64, 67)) // g -> 2%
        {
            return possibleLetters[6];
        }
        else if (TestRange(b, 68, 68)) // h -> 0.5%
        {
            return possibleLetters[7];
        }
        else if (TestRange(b, 69, 80)) // i -> 7%
        {
            return possibleLetters[8];
        }
        else if (TestRange(b, 81, 81)) // k -> 0.5%
        {
            return possibleLetters[9];
        }
        else if (TestRange(b, 82, 88)) // l -> 4%
        {
            return possibleLetters[10];
        }
        else if (TestRange(b, 89, 92)) // m -> 2%
        {
            return possibleLetters[11];
        }
        else if (TestRange(b, 93, 99)) // n -> 4%
        {
            return possibleLetters[12];
        }
        else if (TestRange(b, 100, 107)) // o -> 4%
        {
            return possibleLetters[13];
        }
        else if (TestRange(b, 108, 113)) // p -> 3%
        {
            return possibleLetters[14];
        }
        else if (TestRange(b, 114, 131)) // r -> 10%
        {
            return possibleLetters[15];
        }
        else if (TestRange(b, 132, 157)) // s -> 14%
        {
            return possibleLetters[16];
        }
        else if (TestRange(b, 158, 173)) // t -> 8%
        {
            return possibleLetters[17];
        }
        else if (TestRange(b, 174, 176)) // u -> 2%
        {
            return possibleLetters[18];
        }
        else if (TestRange(b, 177, 177)) // v -> 0.5%
        {
            return possibleLetters[19];
        }
        else if (TestRange(b, 178, 179)) // w -> 1%
        {
            return possibleLetters[20];
        }
        else if (TestRange(b, 180, 180)) // y -> 0.5%
        {
            return possibleLetters[21];
        }

        return "e";
    } // Gets the letter according to the predefined chances for each letter and their coresponding ranges

    private int PickDifferentLetter(string[] possible, string[] letters) // Picks a unique letter, which is absent from the given array
    {
        int q = 0;

        while (true)
        {
            q = Random.Range(1, 180);

            if (!IsInArray(ChancesMapi(possible, q), letters))
            {
                break;
            }
        }
        return q;
    }

    private bool IsInArray(string letter, string[] array) // Checks if a letter is in an array
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (letter == array[i])
            {
                return true;
            }
        }

        return false;
    }

    private string ArrayToString(string[] array)
    {
        string res = "";

        for (int i = 0; i < array.Length; i++)
        {
            res += array[i];
        }

        return res;
    } // Makes an array of strings into a whole single string

    private void CheckThePossibleLetters()
    {
        notAppropriateWords = new List<string>();

        foreach (var word in ListOfPossibleWords)
        {
            for (char i = 'a'; i < 'z'; i++)
            {
                if (word.Contains(i.ToString()) && !IsInArray(i.ToString(), letters))
                {
                    notAppropriateWords.Add(word);
                }
            }
        }

        foreach (string word in notAppropriateWords)
        {
            ListOfPossibleWords.Remove(word);
            print("Found not invited word: " + word);
        }
    } // Checks if the passed words accidently contain a wrong letter

    private IEnumerator AveiabilityTest() // Checks if the words are enough to move to the next scene
    {
        while (ListOfPossibleWords.Count <= 5)
        {
            // If it hasn't found at least 22 real words, then it picks new 6 random letters and repeat again

            words = new List<string>();

            yield return new WaitForSeconds(0.5f);

            // Picks random 6 letters
            letters = PickLetters();

            // Gets all possible variations in a list
            Variations(words, ArrayToString(letters), 3, "");
            Variations(words, ArrayToString(letters), 4, "");
            Variations(words, ArrayToString(letters), 5, "");

            // Starts checking all the variations for possible words
            CheckWords();

            yield return new WaitForSeconds(8f); // How much time it will wait for checking the words

            CheckThePossibleLetters();

            // If it have found at least 22 real English words thats considered as enough for the game's pruposes
            if (ListOfPossibleWords.Count >= 22)
            {
                break;
            }

            possibleWords = 0;
            words.Clear();
        }

        // Clears all old varioations
        words.Clear();

        print(Time.realtimeSinceStartup);

        SceneManager.LoadScene("WordCreation");

    } // Searches for the combo of  letters that will be able to form at least 22 words

    public void GoToMenu()
    {
        PersonalProgram pp = GameObject.FindGameObjectWithTag("pp").GetComponent<PersonalProgram>();
        Destroy(pp.gameObject);
        StopAllCoroutines();

        SceneManager.LoadScene("Menu");
    }

    private string Router()
    {
        switch (Random.Range(1, 11))
        {
            case 1:
                return API_KEY1;
            case 2:
                return API_KEY2;
            case 3:
                return API_KEY3;
            case 4:
                return API_KEY4;
            case 5:
                return API_KEY5;
            case 6:
                return API_KEY6;
            case 7:
                return API_KEY7;
            case 8:
                return API_KEY8;
            case 9:
                return API_KEY9;
            case 10:
                return API_KEY10;
            default:
                return API_KEY1;
        }
    } // Picks a random API_KEY--
}
