using UnityEngine;
/*
 * Script which handles the tile puzzle game
 */
public class PuzzleScript : MonoBehaviour
{

    public GameObject slot;
    float xTemp, yTemp, zTemp;

    //This function is used if you are using a mouse in a 2D or 3D game
    void OnMouseUp()
    {
        //print("Local Pos: " + transform.localPosition.ToString());
        //print("Slot Pos: " + slot.transform.localPosition.ToString());
        //print("Distance: " + Vector3.Distance(transform.localPosition, slot.transform.localPosition).ToString());

        //Check if the puzzle piece selected is next to the empty slot
        //If you scale the puzzle, you must change the value: 1 to whatever the resized puzzle piece was
        if (Vector3.Distance(transform.localPosition, slot.transform.localPosition) == 1)
        {

            //Swap with the empty slot
            xTemp = transform.localPosition.x;
            yTemp = transform.localPosition.y;
            zTemp = transform.localPosition.z;
            transform.localPosition = new Vector3(slot.transform.localPosition.x, slot.transform.localPosition.y, slot.transform.localPosition.z);
            slot.transform.localPosition = new Vector3(xTemp, yTemp, zTemp);

        }

    }


    public void movePiece()
    {
        //Check if the puzzle piece selected is next to the empty slot
        //If you scale the puzzle, you must change the value: 1 to whatever the resized puzzle piece was
        if (Vector3.Distance(transform.localPosition, slot.transform.localPosition) == 1)
        {
            //Play click sound
            FindObjectOfType<AudioManager>().Play("Click");
            //Swap with the empty slot
            xTemp = transform.localPosition.x;
            yTemp = transform.localPosition.y;
            zTemp = transform.localPosition.z;
            transform.localPosition = new Vector3(slot.transform.localPosition.x, slot.transform.localPosition.y, slot.transform.localPosition.z);
            slot.transform.localPosition = new Vector3(xTemp, yTemp, zTemp);

        }
    }
}
