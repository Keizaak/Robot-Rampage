using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawn : MonoBehaviour {

    [SerializeField]
    private GameObject[] pickups;

    void SpawnPickup()
    {
        GameObject pickup = Instantiate(pickups[Random.Range(0, pickups.Length)]);
        pickup.transform.position = transform.position;
        pickup.transform.parent = transform;
    }

    IEnumerator RespawnPickup()
    {
        yield return new WaitForSeconds(20);
        SpawnPickup();
    }

	void Start () {
        SpawnPickup();
	}
	
	public void PickupWasPickedUp()
    {
        StartCoroutine("RespawnPickup");
    }
}
