using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Enemy patrol script
 */


public class EnemyPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;

    //Array with the empty gameobjects which hold the location to move to
    public Transform[] moveLocations;
    public bool stationary = false;
    private int spot;

    public float rotationSpeed;

    private Quaternion lookRotation;
    private Vector3 direction;

    private GameObject player;
    private float distanceAway;
    private float verticalDistance;
    public GameObject alertMarker;

    public List<GameObject> Children;

    private bool created = false;
    public bool remove = false;
    public bool chasing = false;

    //offsetAmount makes it so the robot is hovering over the point and wont move up and down. 
    //Set this variable in the editor to the actual Y height in the game inspector
    public float offsetAmount;

    void Start()
    {
        waitTime = startWaitTime;
        spot = 0;
        //Below is for the player with the SteamVR camera
        player = GameObject.FindGameObjectWithTag("SteamVRPlayer");

        //Below is for the player with the regular camera
        //player = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    void Update()
    {

        //If the enemy has no patrol locations, it is stationary
        if (stationary || chasing)
        {
            goto detection;
        }


        //Move towards the patrol location

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(moveLocations[spot].transform.position.x, offsetAmount, moveLocations[spot].transform.position.z), speed * Time.deltaTime);


        //Get the direction of the patrol location
        direction = ((new Vector3(moveLocations[spot].transform.position.x, offsetAmount, moveLocations[spot].transform.position.z)) - transform.position).normalized;

        //A fix to stop the enemy from rotating once it reached the patrol location
        if (direction != Vector3.zero)
        {
            lookRotation = Quaternion.LookRotation(direction);
        }
        //Over time, look towards the patrol location
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);


        //Check if the enemy has reached the patrol location
        if (Vector3.Distance(transform.position, new Vector3(moveLocations[spot].transform.position.x, offsetAmount, moveLocations[spot].transform.position.z)) <= 1)
        {
            //Once enough time has elapsed, update to the next location
            if (waitTime <= 0)
            {
                UpdateSpot();
                waitTime = startWaitTime;
            }
            else
            {
                //waiting at the spot for a given time
                waitTime -= Time.deltaTime;
            }
        }

        detection:
        //Calculate distance from player to enemy
        distanceAway = Vector3.Distance(transform.position, player.transform.position);
        //Debug.Log(distanceAway);

        //Calculate vertical distance from player to enemy
        verticalDistance = transform.position.y - player.transform.position.y;

        //if the player is near the enemy
        if (distanceAway < 15 && verticalDistance < 5)
        {
            //Debug.Log("Alerted");
            //Alert the enemy
            CreateAlert();
            //start chasing the player
            chasing = true;
            ChasePlayer();
        }
        else
        {
            //Remove the alerted status
            RemoveAlert();
            chasing = false;
        }


    }

    //Get the next patrol location
    void UpdateSpot()
    {
        if (spot + 1 >= moveLocations.Length)
        {
            spot = 0;
            return;
        }
        spot++;
    }

    //Remove the alert marker prefab from the enemy
    void RemoveAlert()
    {
        foreach (Transform child in transform)
        {
            if (child.tag == "AlertMarker")
            {

                Destroy(child.gameObject);
                created = false;
            }
        }
    }

    void CreateAlert()
    {
        //Check to see if an alert icon is already created
        if (!created)
        {
            //Instantiate a new alert marker object and assign it as a child to the enemy
            var go = Instantiate(alertMarker, transform.position + new Vector3(0, 1f, 0), transform.rotation) as GameObject;
            go.transform.parent = transform;
            Children.Add(go);

            //play alert sound
            FindObjectOfType<AudioManager>().Play("Detected");
            //Alert marker is created
            created = true;

        }

    }
    void ChasePlayer()
    {
        //Copy the players position vector but ignore the Y variable.
        //This makes it so the robot doesn't follow a player into their head
        Vector3 ignoreY = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(ignoreY);

        //check distance between player and enemy
        if (Vector3.Distance(ignoreY, transform.position) > 7.5)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //You can put a game over conditon here 
        }
    }

    //Disable the alert marker. Not in use currently
    public void disableAlertMarker()
    {
        //Destroy(alertMarker);
    }

}
