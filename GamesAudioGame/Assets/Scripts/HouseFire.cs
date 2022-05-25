using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFire : MonoBehaviour
{   
    public bool isOnFire = false;
    public GameObject[] fires;
    public float fireTimer =0;
    public float fireTimerMax=6;

    // Update is called once per frame
    void Update()
    {
        if (isOnFire)
        {
            fireTimer+= Time.deltaTime;

            foreach (GameObject fire in fires)
            {
                fire.SetActive(true);
            }
        }

        if(fireTimer >= fireTimerMax){
            isOnFire=false;
            Destroy(this.gameObject);
        }
    }
        private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fire"){
            isOnFire = true;
        }
    }
    
}
