using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Code to handle releasing the jail cell bars
 */
public class TriggerBars : MonoBehaviour
{
    public GameObject[] puzzlePieces;
    public Animator anim;
    public GameObject tilePuzzle;
    private bool lowered = false;
    public GameObject controllerLaser;
    public GameObject gun;
    public GameObject viveControllerModel;

    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        //Check if the pieces are in the correct spot
        if (!lowered && puzzlePieces[0].tag == "CorrectLocation" && puzzlePieces[1].tag == "CorrectLocation" && puzzlePieces[2].tag == "CorrectLocation" && puzzlePieces[3].tag == "CorrectLocation" &&
            puzzlePieces[4].tag == "CorrectLocation" && puzzlePieces[5].tag == "CorrectLocation" && puzzlePieces[6].tag == "CorrectLocation" &&
            puzzlePieces[7].tag == "CorrectLocation")
        {
            lowerBars();
        }
    }

    //Lower the jail cell bars
    public void lowerBars()
    {
        FindObjectOfType<AudioManager>().Play("Release Buzzer");
        anim.Play("Jail Release");
        lowered = true;
        print("removing");
        Destroy(controllerLaser);
        //enable the gun model
        gun.SetActive(true);
        viveControllerModel.SetActive(false);
        Destroy(tilePuzzle, 5f);

    }
}
