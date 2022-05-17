using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageLadyDialogue : MonoBehaviour
{
    private FMOD.Studio.EventInstance CabbageLines;
    public float CoolDownMax = 10;
    public float cooldownTimer = 0;
    public GameObject Camera;
    public GameObject Turnips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += 1 * Time.deltaTime;
        if (cooldownTimer >= CoolDownMax)
        {
            PlayLine();
            cooldownTimer = 0;
        }
    
    }


    // Bool for fire and how far away a character is from Cabbages

    private void PlayLine()
    {
        CabbageLines = FMODUnity.RuntimeManager.CreateInstance("event:/NPC_Dialogue/TurnipLady");
        CabbageLines.setParameterByName("LittleBoy", 0);
        CabbageLines.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.gameObject));
        CabbageLines.start();
        CabbageLines.release();
    }
}
