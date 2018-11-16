using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int health;
    public int armor;
    public GameUI gameUI;
    private GunEquipper gunEquipper;
    private Ammo ammo;

	void Start () {
        ammo = GetComponent<Ammo>();
        gunEquipper = GetComponent<GunEquipper>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount)
    {
        int healthDamage = amount;

        if(armor > 0)
        {
            int effectiveArmor = armor * 2;
            effectiveArmor -= healthDamage;

            if(effectiveArmor > 0)
            {
                armor = effectiveArmor / 2;
                return; //if still armor, don't need to process health damage
            }
            armor = 0;
        }

        health -= healthDamage;
        Debug.Log("Health is " + health);

        if(health <= 0)
        {
            Debug.Log("GameOver");
        }
    }

    private void PickupHealth()
    {
        health += 50;
        if(health > 200)
        {
            health = 200;
        }
    }

    private void PickupArmor()
    {
        armor += 15;
    }

    private void PickupAssaultRifleAmmo()
    {
        ammo.AddAmmo(Constants.AssaultRifle, 50);
    }

    private void PickupPistolAmmo()
    {
        ammo.AddAmmo(Constants.Pistol, 20);
    }

    private void PickupShotgunAmmo()
    {
        ammo.AddAmmo(Constants.Shotgun, 10);
    }

    public void PickupItem(int pickupType)
    {
        switch (pickupType)
        {
            case Constants.PickUpArmor:
                PickupArmor();
                break;
            case Constants.PickUpHealth:
                PickupHealth();
                break;
            case Constants.PickUpAssaultRifleAmmo:
                PickupAssaultRifleAmmo();
                break;
            case Constants.PickUpPistolAmmo:
                PickupPistolAmmo();
                break;
            case Constants.PickUpShotgunAmmo:
                PickupShotgunAmmo();
                break;
            default:
                Debug.LogError("Back pickup type passed " + pickupType);
                break;
        }
    }
}
