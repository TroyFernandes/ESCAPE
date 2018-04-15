using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;

/*
 * Script to handle shuffling of the puzzle
 */
public class PuzzleShuffle : MonoBehaviour
{
    public GameObject[] puzzlePieces;
    //Available slots a puzzle piece can go to
    public List<Vector3> availableLocations = new List<Vector3>();
    static System.Random random;
    //Save the correct locations the puzzle pieces should be in
    public Dictionary<string, Vector3> correctLocations = new Dictionary<string, Vector3>();

    bool openGates = false;


    Dictionary<string, int> indexLookup = new Dictionary<string, int>()
    {
        { "PuzzlePiece0", 0 },
        { "PuzzlePiece1", 1 },
        { "PuzzlePiece2", 2 },
        { "PuzzlePiece3", 3 },
        { "PuzzlePiece4", 4 },
        { "PuzzlePiece5", 5 },
        { "PuzzlePiece6", 6 },
        { "PuzzlePiece7", 7 }
    };



    void Start()
    {
        random = new System.Random();
        //Find the puzzle pieces in the scene
        for (int i = 0; i <= 7; i++)
        {
            //print("Finding: PuzzlePiece" + i.ToString());
            puzzlePieces[i] = GameObject.Find("PuzzlePiece" + i.ToString());
        }

        //Add the puzzle piece's transform to list
        foreach (GameObject obj in puzzlePieces)
        {
            //print(obj.name + " " + obj.transform.localPosition);
            availableLocations.Add(obj.transform.localPosition);
        }
        //Add the puzzle pieces to the correct locations
        for (int i = 0; i <= 7; i++)
        {

            correctLocations.Add(puzzlePieces[i].name, availableLocations[i]);
        }

        //Shuffle the puzzle
        shufflePieces();

    }

    void Update()
    {
        //check if the puzzle is correct
        InvokeRepeating("checkLoop", 0, 20);
    }


    void shufflePieces()
    {
        //Choose a random location from the availableLocations array, place it at that location, then remove from the availableLocation arrary
        for (int i = 0; i <= 7; i++)
        {
            int r = random.Next(availableLocations.Count);
            puzzlePieces[i].transform.localPosition = new Vector3(availableLocations[r].x, availableLocations[r].y, availableLocations[r].z);
            availableLocations.RemoveAt(r);
        }
    }

    void checkLoop()
    {
        foreach (GameObject p in puzzlePieces)
        {
            isCorrect(p.name, p.transform.localPosition);
        }
        return;
    }


    //Change the tag on the gameobject if the piece is in the correct location or not
    void isCorrect(string key, Vector3 comparison)
    {
        Vector3 correctLocationVector;
        correctLocationVector = correctLocations[key];

        if (comparison == correctLocationVector)
        {
            puzzlePieces[indexLookup[key]].tag = "CorrectLocation";
        }
        else
        {
            puzzlePieces[indexLookup[key]].tag = "IncorrectLocation";
        }
    }

}
