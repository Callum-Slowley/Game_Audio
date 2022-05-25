using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseFire : MonoBehaviour
{   
    public bool isOnFire = false;
    public GameObject[] fires;
    public float fireTimer =0;
    public float fireTimerMax=6;
    public string soundLocation; 
    public FMOD.Studio.EventInstance fireSound;

    void Start() {
        if(soundLocation != ""){
            fireSound = FMODUnity.RuntimeManager.CreateInstance(soundLocation);
            fireSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            fireSound.setParameterByName("buildingFire", 0);
            fireSound.start();
            fireSound.release();
    }
}
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
            fireSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            fireSound.release();
            fireSound.release();
            Destroy(this.gameObject);
        }
    }
        private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fire"){
            isOnFire = true;
            if(soundLocation==""){
                return;
            }
            else{
                fireSound.setParameterByName("buildingFire", 1);
                fireSound.start();
                fireSound.release();
            }
        }
    }
    
}
