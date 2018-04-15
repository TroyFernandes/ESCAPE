using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script to handle the touchpad input on the controller
 */

public class TouchpadRight : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device device;
    private SteamVR_TrackedController controller;

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        controller = GetComponent<SteamVR_TrackedController>();
        controller.PadClicked += Controller_PadClicked;
    }

    private void Controller_PadClicked(object sender, ClickedEventArgs e)
    {
        RaycastHit hit;

        if (device.GetAxis().x != 0 || device.GetAxis().y != 0)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                //Debug.Log("Touchpad is hitting: " + hit.transform.name);
                PuzzleScript puzzlePiece = hit.transform.GetComponent<PuzzleScript>();
                if (puzzlePiece != null)
                {
                    puzzlePiece.movePiece();

                }
            }
        }
    }

    void Update()
    {
        device = SteamVR_Controller.Input((int)trackedObject.index);
        //Debug.DrawRay(transform.position, transform.forward *10 , Color.green);

    }

}
