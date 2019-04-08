using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public string colour;
    private RailsManagement rm;

    [SerializeField]
    private float moveSpeed = 2f; // Walk speed that can be set in Inspector
    [SerializeField]
    private float range = 1f; // Distance from areas, on which to change to route

    [SerializeField]
    private float rotationRate = 0.1f; // Distance from areas, on which to change to route

    public int currentPoint = 0; // Index of current waypoint from which the train goes

    private GameObject[] wayPoints = new GameObject[18];
    private TrainSpawner ts;

    private void Start()
    {
        rm = FindObjectOfType<RailsManagement>();
        ts = FindObjectOfType<TrainSpawner>();

        AssignWayPoints();
    }
    
    private void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (colour == collision.gameObject.tag)
        {
            ts.correctTrains++;
            ts.waitTime -= ts.waitTimeChange;
        }
        else
            ts.waitTime += ts.waitTimeChange;
        Destroy(this.gameObject);

    }

    private void AssignWayPoints() // Getting the way points in an array
    {
        for (int i = 0; i < 18; i++)
        {
            string area = i.ToString();
            wayPoints[i] = GameObject.Find(area);
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoints[currentPoint].transform.position, moveSpeed * Time.deltaTime);

        for (int i = 0; i < 18; i++)
        {
            if (wayPoints[i] != null)
            {
                float dis = Vector3.Distance(wayPoints[i].transform.position, this.transform.position);

                if (dis <= range)
                {
                    switch (wayPoints[i].name)
                    {
                        case "0":
                            if (wayPoints[i].GetComponent<SpriteRenderer>().sprite == rm.straight)
                            {
                                currentPoint = 9;
                            }
                            else
                            {
                                currentPoint = 1;
                                StartCoroutine(Rotate(90,0));
                            }
                            break;

                        case "1":
                            if (wayPoints[i].GetComponent<SpriteRenderer>().sprite == rm.straight)
                            {
                                currentPoint = 2;
                            }
                            else
                            {
                                currentPoint = 6;
                                StartCoroutine(Rotate(180, 90));
                            }
                            break;

                        case "2":
                            currentPoint = 3;
                            StartCoroutine(Rotate(180,90));
                            break;

                        case "3":
                            if (wayPoints[i].GetComponent<SpriteRenderer>().sprite == rm.straight)
                            {
                                currentPoint = 5;
                            }
                            else
                            {
                                currentPoint = 4;
                                StartCoroutine(Rotate(270, 180));
                            }
                            break;

                        case "6":
                            if (wayPoints[i].GetComponent<SpriteRenderer>().sprite == rm.straight)
                            {
                                currentPoint = 8;
                            }
                            else
                            {
                                currentPoint = 7;
                                StartCoroutine(Rotate(270, 180));
                            }
                            break;

                        case "9":
                            currentPoint = 10;
                            StartCoroutine(Rotate(90, 0));
                            break;

                        case "10":
                            if (wayPoints[i].GetComponent<SpriteRenderer>().sprite == rm.straight)
                            {
                                currentPoint = 11;
                            }
                            else
                            {
                                currentPoint = 14;
                                StartCoroutine(Rotate(0, 90));
                            }
                            break;

                        case "11":
                            if (wayPoints[i].GetComponent<SpriteRenderer>().sprite == rm.straight)
                            {
                                currentPoint = 12;
                            }
                            else
                            {
                                currentPoint = 13;
                                StartCoroutine(Rotate(0, 90));
                            }
                            break;

                        default:
                            break;

                        case "14":
                            currentPoint = 15;
                            StartCoroutine(Rotate(90, 0));
                            break;

                        case "15":
                            if (wayPoints[i].GetComponent<SpriteRenderer>().sprite == rm.straight)
                            {
                                currentPoint = 16;
                            }
                            else
                            {
                                currentPoint = 17;
                                StartCoroutine(Rotate(180, 90));
                            }
                            break;
                    }
                }
            }
        }
    } // Adjusting the train route depending on the connector rails positions

    private IEnumerator Rotate(int degree, int currentZ) // Rotating when changing the route | Can change i to adjust the rotating speed
    {
        if (degree > transform.rotation.z)
        {
            for (int i = (int)(currentZ); i < degree; i += 4)
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, i));
                yield return new WaitForSeconds(rotationRate);
            }
        }
        else
        {
            for (int i = degree; i > (int)(currentZ); i -= 4)
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, i));
                yield return new WaitForSeconds(rotationRate);
            }
        }

        transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, degree));
    }
}
