using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mountinDist : MonoBehaviour
{
    public GameObject player;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
    
        distance = Vector3.Distance(player.transform.position,this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
            distance = Vector3.Distance(player.transform.position,this.transform.position);
    }
}
