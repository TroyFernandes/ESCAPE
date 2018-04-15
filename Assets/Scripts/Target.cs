using UnityEngine;

/*
 * Script which handles the behaviour of the enemy being a Target
 */
public class Target : MonoBehaviour
{

    //Enemy health
    public float health = 75f;
    int i = 0;
    //Array for random sounds to play
    string[] soundNames = { "RobotDamage1", "RobotDamage2", "RobotDamage3" };
    public EnemyPatrol enemyPatrolScript;
    public bool disable = false;
    public GameObject enemyEye;
    public GameObject animator;

    public Transform hitParticleSystem;
    void Start()
    {
        enemyPatrolScript = GetComponent<EnemyPatrol>();

        animator = gameObject.transform.parent.gameObject;



        foreach (Transform child in transform)
        {
            if (child.name == "Eye")
            {
                enemyEye = child.gameObject;
                return;
            }
        }
    }

    public void DecreaseHealth(float amount)
    {
        hitParticleSystem.GetComponent<ParticleSystem>().Play();
        health -= amount;
        Debug.Log(gameObject.name + " Taking: " + amount + " Health Remaining: " + health);
        //Play a "take damage" soundclip
        FindObjectOfType<AudioManager>().Play(chooseSoundClip());
        if (health <= 0)
        {
            Kill();
        }
    }

    //Kill the enemy
    public void Kill()
    {
        //enemyPatrolScript.disableAlertMarker();
        Debug.Log("Killing: " + gameObject.name);
        Destroy(enemyEye);
        animator.GetComponent<Animator>().enabled = false;
        FindObjectOfType<AudioManager>().Play("Explosion");
        enemyPatrolScript.enabled = false;
        gameObject.AddComponent<Rigidbody>();
        //Destroy(gameObject);
    }

    //Choose a "take damage" soundclip to play
    string chooseSoundClip()
    {
        i++;
        if (i > 2)
        {
            i = 0;
        }
        return soundNames[i];
    }

}
