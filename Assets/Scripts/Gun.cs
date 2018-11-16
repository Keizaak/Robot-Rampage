using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float fireRate;
    protected float lastFireTime;

    public Ammo ammo;
    public AudioClip liveFire;
    public AudioClip dryFire;

    public float zoomFactor;
    public int range;
    public int damage;

    private float zoomFOV;
    private float zoomSpeed = 6;

	void Start () {
        zoomFOV = Constants.CameraDefaultZoom / zoomFactor;
        lastFireTime = Time.time - 10; //sets lastFireTime 10s ago => when the game starts, the player will be able to fire the gun immediatly
	}
	
	protected virtual void Update () {
        //right click => zoom
        if (Input.GetMouseButton(1))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, zoomFOV, zoomSpeed * Time.deltaTime);
        } else
        {
            Camera.main.fieldOfView = Constants.CameraDefaultZoom;
        }
	}

    protected void Fire()
    {
        if (ammo.HasAmmo(tag))
        {
            GetComponent<AudioSource>().PlayOneShot(liveFire);
            ammo.ConsumeAmmo(tag);
        } else
        {
            GetComponent<AudioSource>().PlayOneShot(dryFire); 
        }
        GetComponentInChildren<Animator>().Play("Fire");

        //a ray fires out at the range of the gun
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        //cheks if it collides with a GameObject
        if(Physics.Raycast(ray, out hit, range))
        {
            ProcessHit(hit.collider.gameObject);
        }
    }

    //passes the damage to the correct GameObject
    private void ProcessHit(GameObject hitObject)
    {
        if(hitObject.GetComponent<Player>() != null)
        {
            hitObject.GetComponent<Player>().TakeDamage(damage);
        }

        if(hitObject.GetComponent<Robot>() != null)
        {
            hitObject.GetComponent<Robot>().TakeDamage(damage);
        }
    }
}
