using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObjects : MonoBehaviour
{
    public bool isOnFire = false;
    public GameObject[] fires;

    // Update is called once per frame
    void Update()
    {
        if(isOnFire)
        {
            foreach(GameObject fire in fires){
                fire.SetActive(true);
            }
        }
    }
}
