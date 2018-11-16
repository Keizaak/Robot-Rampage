using UnityEngine;

public class Shotgun : Gun
{

    protected override void Update()
    {
        base.Update();
        //Shotgun & Pistol have semi-auto fire rate
        //checks whether enough time has elapsed between shots to allow for another one
        if (Input.GetMouseButtonDown(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}