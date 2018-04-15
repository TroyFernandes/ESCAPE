using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Game over display
 */
public class Gameover : MonoBehaviour
{
    public GameObject gameOverMessage;
    //Game over message when the player enters the exit zone
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SteamVRPlayer" || collision.gameObject.tag == "Player")
        {
            Debug.Log("collision");
            gameOverMessage.SetActive(true);
        }
    }
}
