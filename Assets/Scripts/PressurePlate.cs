using UnityEngine;

/*
 * Script which deals with the pressure plate logic
 */
public class PressurePlate : MonoBehaviour
{
    public GameObject puzzle;
    void Start()
    {
        gameObject.GetComponent<Renderer>().material.color = Color.red;

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PressureBox")
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            puzzle.SetActive(true);
        }
    }

}
