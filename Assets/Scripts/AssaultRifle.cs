using UnityEngine;

public class AssaultRifle : Gun {

    protected override void Update()
    {
        base.Update();
        //Automatic Fire
        //GetMouseButton instead of down => holding down button to auto-fire
        if(Input.GetMouseButton(0) && (Time.time - lastFireTime) > fireRate)
        {
            lastFireTime = Time.time;
            Fire();
        }
    }
}