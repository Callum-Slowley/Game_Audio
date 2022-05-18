using UnityEngine;

public class ColliderSoundTrigger : MonoBehaviour
{
    private FMOD.Studio.EventInstance CollisonSound;

    // Fmod Varaibles in strings because I'm bang tidy
    private string FmodEventName = "event:/NPC_Dialogue/LittleBoy";
    private string FmodParameter = "LittleBoy";

    public GameObject MainCamera;
    public GameObject TextureOne;
    


    // Basic Collider Script
    void OnCollisionEnter(Collision col)
    {
        // Basic collider for triggering sounds 
        // I'll do the FMOD for hitting different surfaces
        string CollisionName = col.collider.name;
        CollisonSound = FMODUnity.RuntimeManager.CreateInstance(FmodEventName);
        SetParameterSheet(CollisionName);
        CollisonSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(MainCamera.gameObject));
        CollisonSound.start();
        CollisonSound.release();


    }
    
    // Wondering if we can set this up using prefabs to trigger different FMOD events.
    // Could maybe move this into movement and sent the dragon speed control at impact as volume
    void SetParameterSheet(string ParameterName)
    {
        //
        if (TextureOne.name != ParameterName)
        {
            CollisonSound.setParameterByName(FmodParameter, 0);
        }
        else
        {
            CollisonSound.setParameterByName(FmodParameter, 1);
        }

    }


}
