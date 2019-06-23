using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public PlayerController[] players;
    public GameObject interactButton;
    public GameObject instanceButton;
    public bool inDistance1, inDistance2, inDistance3, inDistance4;
    public string pInDistance;
    public Equipment item;
    public float coolDownTimer = 60;

    void Start()
    {
        players = new PlayerController[FindObjectsOfType<PlayerController>().Length];
        players = FindObjectsOfType<PlayerController>();
    }

    void Update()
    {
        ButtonPrompt();
        coolDownTimer -= Time.deltaTime;
        if (coolDownTimer <= 0)
        {
            players = new PlayerController[FindObjectsOfType<PlayerController>().Length];
            players = FindObjectsOfType<PlayerController>();
            coolDownTimer = 60;
        }
    }

    void ButtonPrompt()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (Vector3.Distance(players[i].transform.position, transform.position) <= players[i].itemDistanceCheck)
            {
                pInDistance = ("inDistance" + (i + 1));

                if (!GameObject.Find("InteractButton(Clone)"))
                {
                    instanceButton = Instantiate(interactButton, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), transform.rotation, transform);
                    instanceButton.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (pInDistance == "inDistance1")
                {
                    inDistance1 = true;
                }

                else if (pInDistance == "inDistance2")
                {
                    inDistance2 = true;
                }

                else if (pInDistance == "inDistance3")
                {
                    inDistance3 = true;
                }

                else if (pInDistance == "inDistance4")
                {
                    inDistance4 = true;
                }
            }
        }

        if (GameObject.Find("InteractButton(Clone)"))
        {
            if (!(Vector3.Distance(players[0].transform.position, transform.position) <= players[0].itemDistanceCheck) && inDistance1)
            {
                pInDistance = "";
                inDistance1 = false;
            }

            else if (!(Vector3.Distance(players[1].transform.position, transform.position) <= players[1].itemDistanceCheck) && inDistance2)
            {
                pInDistance = "";
                inDistance2 = false;
            }

            else if (!(Vector3.Distance(players[2].transform.position, transform.position) <= players[2].itemDistanceCheck) && inDistance3)
            {
                pInDistance = "";
                inDistance3 = false;
            }

            else if (!(Vector3.Distance(players[3].transform.position, transform.position) <= players[3].itemDistanceCheck) && inDistance4)
            {
                pInDistance = "";
                inDistance4 = false;
            }

            else if (!inDistance1 && !inDistance2 && !inDistance3 && !inDistance4)
            {
                Destroy(instanceButton.gameObject);
            }
        }
        
    }
}
