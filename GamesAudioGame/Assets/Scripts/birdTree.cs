using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdTree : MonoBehaviour
{
    public bool isOnFire = false;
    public GameObject[] fires;
    public float fireTimer =0;
    public float fireTimerMax=6;
    public string soundLocation; 
    public FMOD.Studio.EventInstance birdSound;
    public Renderer rend;

    void Start() {
        if(soundLocation != ""){
            birdSound = FMODUnity.RuntimeManager.CreateInstance(soundLocation);
            birdSound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            birdSound.setParameterByName("buildingFire", 0);
            birdSound.start();
            birdSound.release();
    }
                
        rend = GetComponent<Renderer>();
        rend.enabled = true;
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
            birdSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            birdSound.release();
            birdSound.release();
            rend.enabled = false;
            foreach (GameObject fire in fires)
            {
                fire.SetActive(false);
            }
            
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
                birdSound.setParameterByName("buildingFire", 1);
                birdSound.start();
                birdSound.release();
            }
        }
    }
}
