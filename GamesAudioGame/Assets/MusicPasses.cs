using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPasses : MonoBehaviour
{
    public float velocity;
    public Rigidbody rb;
    public FMOD.Studio.EventInstance musicEvent;

    // Start is called before the first frame update
    void Start()
    {
            rb = GetComponent<Rigidbody>();
            musicEvent = FMODUnity.RuntimeManager.CreateInstance("event:/SoundTrack/GAME");
            musicEvent.start();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = rb.velocity.magnitude;

        musicEvent.setParameterByName("Intensity", velocity);
    }
}
