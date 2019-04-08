using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RailsManagement : MonoBehaviour
{
    public string key;

    public Sprite upLeft;
    public Sprite upRight;
    public Sprite straight;
    public Sprite downRight;

    private void OnMouseDown()
    {
        switch (key)
        {
            case "0":
                if (GetComponent<SpriteRenderer>().sprite == upLeft)
                    GetComponent<SpriteRenderer>().sprite = straight;
                else GetComponent<SpriteRenderer>().sprite = upLeft;
                break;

            case "1":
                if (GetComponent<SpriteRenderer>().sprite == straight)
                {
                    GetComponent<SpriteRenderer>().sprite = upRight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = straight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                }
                break;

            case "3":
                if (GetComponent<SpriteRenderer>().sprite == straight)
                {
                    GetComponent<SpriteRenderer>().sprite = downRight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = straight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                break;

            case "6":
                if (GetComponent<SpriteRenderer>().sprite == straight)
                {
                    GetComponent<SpriteRenderer>().sprite = downRight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = straight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                break;

            case "9":
                if (GetComponent<SpriteRenderer>().sprite == straight)
                {
                    GetComponent<SpriteRenderer>().sprite = upRight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = straight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                break;

            case "10":
                if (GetComponent<SpriteRenderer>().sprite == straight)
                {
                    GetComponent<SpriteRenderer>().sprite = upRight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = straight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                break;

            case "11":
                if (GetComponent<SpriteRenderer>().sprite == straight)
                {
                    GetComponent<SpriteRenderer>().sprite = upRight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = straight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                break;

            case "15":
                if (GetComponent<SpriteRenderer>().sprite == straight)
                {
                    GetComponent<SpriteRenderer>().sprite = upRight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    GetComponent<SpriteRenderer>().sprite = straight;
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
                break;



            default:
                break;
        }
    }
}