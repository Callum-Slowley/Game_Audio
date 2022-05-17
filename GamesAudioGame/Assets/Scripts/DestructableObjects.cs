using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjects : MonoBehaviour
{
    public bool isOnFire = false;
    public GameObject[] fires;
    public FMOD.Studio.EventInstance TwistedFireStarter;

    // Update is called once per frame
    void Update()
    {
        TwistedFireStarter = FMODUnity.RuntimeManager.CreateInstance("event:/NPC_Dialogue/TurnipLady");
        if (isOnFire)
        {
            foreach (GameObject fire in fires)
            {
                fire.SetActive(true);
            }
            TwistedFireStarter.setParameterByName("TurnipOnFire", 1);
        }
        else
        {
            TwistedFireStarter.setParameterByName("TurnipOnFire", 1);
        }
    }
}
