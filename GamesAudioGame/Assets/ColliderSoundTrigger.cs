using UnityEngine;

public class ColliderSoundTrigger : MonoBehaviour
{
    private FMOD.Studio.EventInstance CollisonSound;

    // Fmod Varaibles in strings because I'm bang tidy
    private string FmodEventName = "event:/DragonSounds/DragonHittingIntoStuff",
        FmodImpactVolume = "DragonSpeed",
        FmodImpactType = "ImpactLayer";

    public GameObject MainCamera;
    public float velocity;
    
    void Update()
    {
        velocity=GetComponent<MusicPasses>().velocity;    
    }
    // Basic Collider Script
    void OnCollisionEnter(Collision col)
    {
        // Basic collider for triggering sounds 
        // I'll do the FMOD for hitting different surfaces
        int colliderCollisionValue = col.collider.gameObject.layer;
        int TriggeredContacted = FlightCollider(colliderCollisionValue);

        if (TriggeredContacted == 5) ;
        else
        {
            CollisonSound = FMODUnity.RuntimeManager.CreateInstance(FmodEventName);
            CollisonSound.setParameterByName(FmodImpactVolume, 1);
            CollisonSound.setParameterByName(FmodImpactType, TriggeredContacted);
            CollisonSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(MainCamera.gameObject));

            //Play Sound
            CollisonSound.start();
            CollisonSound.release();
        }

    }

    // Bull to map it to the correct event
    int FlightCollider(int FlightNumber)
    {
        Debug.Log(FlightNumber);
        if (FlightNumber == 7)
        {
            return 0;
        }
        else if (FlightNumber == 8)
        {
            return 1;
        }
        else if (FlightNumber == 9)
        {
            return 2;
        }
        else if (FlightNumber == 10)
        {
            return 3;
        }
        else if (FlightNumber == 4)
        {
            return 4;
        }
        else
        {
            return 5;
        }
    }
    
}
