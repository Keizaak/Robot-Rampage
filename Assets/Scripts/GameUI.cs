using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    [SerializeField] //declaring variable to be accessible from the Unity Inspector but not from other scripts
    //Sprite represents an imported texture meant to be used in 2D game or UI
    Sprite redReticle;
    [SerializeField]
    Sprite yellowReticle;
    [SerializeField]
    Sprite blueReticle;
    [SerializeField]
    //Image displays the Sprite to the screen
    Image reticle;

    //Change the Sprite to reflect the active gun
	public void UpdateReticle()
    {
        switch(GunEquipper.activeWeaponType){
            case Constants.Pistol:
                reticle.sprite = redReticle;
                break;
            case Constants.Shotgun:
                reticle.sprite = yellowReticle;
                break;
            case Constants.AssaultRifle:
                reticle.sprite = blueReticle;
                break;
            default:
                return;
        }
    }
}
