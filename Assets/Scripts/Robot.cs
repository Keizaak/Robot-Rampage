using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour {

    [SerializeField] //can acess it in the inspector but not from other scripts
    private string robotType;

    public int health;
    public int range; //distance the gun can fire
    public float fireRate;

    public Transform missileFireSpot;
    NavMeshAgent agent;

    private Transform player;
    private float timeLastFired;

    private bool isDead;

    public Animator robot;

    [SerializeField]
    GameObject missilePrefab;

    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private AudioClip fireSound;
    [SerializeField]
    private AudioClip weakHitSound;

	void Start () {
        isDead = false;
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	void Update () {
        if (isDead)
        {
            return;
        }

        transform.LookAt(player); //make the robot face the player

        agent.SetDestination(player.position); //use the navmesh to find the player

        //checks if the robot is within firing range and if there's been enough time between shots to fire again
        if(Vector3.Distance(transform.position, player.position) < range && Time.time - timeLastFired > fireRate)
        {
            timeLastFired = Time.time;
            Fire();
        }
	}

    private void Fire()
    {
        GameObject missile = Instantiate(missilePrefab);
        missile.transform.position = missileFireSpot.transform.position;
        missile.transform.rotation = missileFireSpot.transform.rotation;
        robot.Play("Fire");
        GetComponent<AudioSource>().PlayOneShot(fireSound);
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }

        health -= amount;

        if(health <= 0)
        {
            isDead = true;
            robot.Play("Die");
            StartCoroutine("DestroyRobot");
            GetComponent<AudioSource>().PlayOneShot(deathSound);
        } else
        {
            GetComponent<AudioSource>().PlayOneShot(weakHitSound);
        }
    }

    IEnumerator DestroyRobot()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
