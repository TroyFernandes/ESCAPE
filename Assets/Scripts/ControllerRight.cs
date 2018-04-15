using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Script for managing the right SteamVR Controller
 */
public class ControllerRight : MonoBehaviour
{

    public SteamVR_TrackedController controller;
    public Camera fpsCam;
    public float range = 100f;
    public AudioSource gunShot;
    public ParticleSystem muzzleFlash;
    public float ROF = 6f;
    private float nextTimeToFire = 0f;
    Animator animator;
    public GameObject gunPrefab;
    public GameObject gunBarrel;
    private SteamVR_TrackedObject trackedObj;


    void Start()
    {
        animator = gunPrefab.GetComponent<Animator>();
        gunShot = GetComponent<AudioSource>();
        controller = GetComponent<SteamVR_TrackedController>();
        if (controller == null)
        {
            controller = gameObject.AddComponent<SteamVR_TrackedController>();
        }
        controller.TriggerClicked += new ClickedEventHandler(timeToFire);
        trackedObj = controller.GetComponent<SteamVR_TrackedObject>();
    }

    //Limit the rate of fire of the gun
    void timeToFire(object sender, ClickedEventArgs e)
    {
        if (Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / ROF;
            gunShot.Play();
            animator.Play("Kickback", -1, 0f);
            Shoot();
        }
    }


    void Shoot()
    {
        //Vibrate the controller
        SteamVR_Controller.Input((int)trackedObj.index).TriggerHapticPulse(700);
        muzzleFlash.Play();
        RaycastHit hit;
        //if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit)) {
        if (Physics.Raycast(gunBarrel.transform.position, gunBarrel.transform.forward, out hit))
        {
            //Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.DecreaseHealth(25f);
                //target.Kill();
            }

        }

    }
}
