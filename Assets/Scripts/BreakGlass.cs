using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGlass : MonoBehaviour
{
    /*
     * Replace the glass prefab with the broken glass prefab
     */
    public GameObject brokenGlassPrefab;
    public bool destroy = false;

    void Start()
    {
        if (destroy)
        {
            Instantiate(brokenGlassPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }
}
