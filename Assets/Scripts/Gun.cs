using UnityEngine;

/*
 * UNUSED SCRIPT
 * Gun script which can be attached to a regular camera (not SteamVR)
 */
public class Gun : MonoBehaviour
{
    public Camera fpsCam;
    public float range = 100f;
    private AudioSource gunShot;
    public ParticleSystem muzzleFlash;
    public float ROF = 6f;
    public Animator anim;


    private float nextTimeToFire = 0f;
    void Start()
    {

        anim = GetComponent<Animator>();
        gunShot = GetComponent<AudioSource>();

    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / ROF;
            gunShot.Play();
            anim.Play("Kickback", -1, 0f);
            Shoot();
        }

    }

    void Shoot()
    {
        Debug.Log("firing");
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            GlassRoof roof = hit.transform.GetComponent<GlassRoof>();
            if (target != null)
            {
                Debug.Log("decreasing health");
                target.DecreaseHealth(25);
                //target.Kill();
            }
            if (roof != null)
            {
                roof.TakeHit();
            }

        }

    }
}
