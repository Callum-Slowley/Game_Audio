using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonSoundHelper : MonoBehaviour
{
    public Movement dragonMovementScript;
    
    // Start is called before the first frame update
    void Start()
    {
        dragonMovementScript = transform.parent.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
