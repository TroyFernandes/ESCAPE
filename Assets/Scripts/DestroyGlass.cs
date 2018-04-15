using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Delete the individual glass pieces every .2 seconds
 */
public class DestroyGlass : MonoBehaviour
{

    float timeToDestroy = 1.0f;


    void Update()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject, timeToDestroy);
            timeToDestroy += 0.2f;
        }

    }
}
