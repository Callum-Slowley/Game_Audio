using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidWobble : MonoBehaviour
{
    Renderer rend;
    Vector3 lastPos;
    Vector3 velocity;
    Vector3 lastRot;
    Vector3 angularVelocity;
    public float MaxWobble =0.03f;
    public float WobbleSpeed =1f;
    public float Recovery =1f;
    public float wobbleAmountX;
    public float wobbleAmountZ;
    float wobbleAmountIncreaseX;
    float wobbleAmountIncreaseZ;
    float pulse;
    float time =0.5f;


    // Start is called before the first frame update
    void Start()
    {
        //get the renderer
        rend = GetComponent<Renderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        //increase time
        time+=Time.deltaTime;
        //Decrease the wobble amount over time
        wobbleAmountIncreaseX = Mathf.Lerp(wobbleAmountIncreaseX,0,Time.deltaTime*Recovery);
        wobbleAmountIncreaseZ = Mathf.Lerp(wobbleAmountIncreaseZ,0,Time.deltaTime*Recovery);
        //sine wave of decreasing wobble
        pulse = 2*Mathf.PI *WobbleSpeed;
        wobbleAmountX = wobbleAmountIncreaseX *Mathf.Sin(pulse*time);
        wobbleAmountZ = wobbleAmountIncreaseZ *Mathf.Sin(pulse*time);
        //Sending through to the renderer
        rend.material.SetFloat("_WobbleX",wobbleAmountX);
        rend.material.SetFloat("_WobbleZ",wobbleAmountZ);
        //Setting velocity
        velocity =(lastPos-transform.position)/Time.deltaTime;
        angularVelocity = transform.rotation.eulerAngles - lastRot;
        //Clamped wobble values
        wobbleAmountIncreaseX += Mathf.Clamp((velocity.x+(angularVelocity.z*0.2f))*MaxWobble,-MaxWobble,MaxWobble);
        wobbleAmountIncreaseZ += Mathf.Clamp((velocity.z+(angularVelocity.x*0.2f))*MaxWobble,-MaxWobble,MaxWobble);
        //Keep last pos
        lastPos = transform.position;
        lastRot = transform.rotation.eulerAngles;
    }
}
