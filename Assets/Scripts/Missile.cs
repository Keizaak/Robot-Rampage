using System.Collections;
using UnityEngine;

public class Missile : MonoBehaviour {

    public float speed = 30f;
    public int damage = 10;

	void Start () {
        StartCoroutine("DeathTimer");
	}
	
	void Update () {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

    //if the missile doesn't hit the player, it should auto-destruct
    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collider)
    {
        if(collider.gameObject.GetComponent<Player>() != null && collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
