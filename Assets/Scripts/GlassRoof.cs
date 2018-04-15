using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script to handle the glass roof over the jail cell
 */

public class GlassRoof : MonoBehaviour
{
    public bool breakglass = false;

    public float hitNumber = 3;
    public GameObject brokenGlassPrefab;
    public float enableCollision = 2.0f;
    public bool takeHit = true;

    private float nextActionTime = 3.0f;
    public float period = 0.1f;

    void Update()
    {
        if (breakglass)
        {
            FindObjectOfType<AudioManager>().Play("Glass Break");

            Instantiate(brokenGlassPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        //Limit the amount of hits in a period of time. Without this, the glass would break in one hit because of the multiple collisions
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            takeHit = true;
        }


    }
    public void TakeHit()
    {
        hitNumber -= 1;
        if (hitNumber <= 0 && takeHit)
        {
            //play breaking glass sound
            FindObjectOfType<AudioManager>().Play("Glass Break");

            Instantiate(brokenGlassPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
            takeHit = false;
        }
    }
    //Only allow the hammer to break the glass
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            TakeHit();
        }
    }


}
