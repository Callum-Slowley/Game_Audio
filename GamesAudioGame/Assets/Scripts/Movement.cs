using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{
    
    //Terrain 
    private enum CURRENT_TERRAIN{STONE,SAND,GRASS};
    [SerializeField]
    private CURRENT_TERRAIN currentTerrain;
    
    public FMOD.Studio.EventInstance DragonFootsteps;
    
    private FMOD.Studio.EventInstance idle_lines;
    
    public Rigidbody rb;
    public bool isFlying;
    public float movementSpeed = 100f;
    public float resetSpeed = 100f;
    public float shiftSpeed = 150f;
    public float controlSpeed = 50f;
    public GameObject Camera;
    public Animator dragonAnimator;

    public float horizSen = 2f;
    public float vertSen = 2f;

    public float yaw = 0f;
    private float minYaw = -60f;
    private float maxYaw = 60f;
    public float pitch = 0f;
    private float minPitch = -90;
    private float maxPitch = 90f;
    
    public float speed;
    public float maxHeight,minHeight;
    public float height;
    public float idleCoolDownMax= 10;
    public float cooldownTimer=0; 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            flying();
            rb.useGravity = false;
            dragonAnimator.SetBool("isFlying", isFlying);
        }
        else
        {
            rb.useGravity = true;
            dragonAnimator.SetBool("isFlying", isFlying);
        }
        DetermineTerrain();
        speed = Mathf.Round(rb.velocity.magnitude * 1000f) / 1000f;

        cooldownTimer += 1*Time.deltaTime;
        if(cooldownTimer>idleCoolDownMax){
            PlayLine(height);
            cooldownTimer=0;
        }
        
    }

    void flying()
    {
        yaw += horizSen * Input.GetAxis("Mouse X");
        pitch -= vertSen * Input.GetAxis("Mouse Y");
        //commented out atm cus you cant turn all the way round
        //yaw = Mathf.Clamp(yaw, minYaw, maxYaw);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = shiftSpeed;
        }
        else
        {
            movementSpeed = resetSpeed;
        }
        //Currently cant move left our right think that would be better just for the floor?
        //transform.position += transform.right * Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed;
        Vector3 dir= transform.forward * Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed;
        rb.AddForce(dir,ForceMode.VelocityChange);
    }

    private void DetermineTerrain()
    {
        RaycastHit[] hit;

        // Originally set at 10.0f, but needs to be set to 0.25 for Robot scenario due to how the level is built.
        hit = Physics.RaycastAll(transform.position, Vector3.down, 10f);

        foreach (RaycastHit rayhit in hit)
        {
            //Debug.Log("HIT");
            if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Stone"))
            {
                currentTerrain = CURRENT_TERRAIN.STONE;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Sand"))
            {
                currentTerrain = CURRENT_TERRAIN.SAND;
                break;
            }
            else if (rayhit.transform.gameObject.layer == LayerMask.NameToLayer("Grass"))
            {
                currentTerrain = CURRENT_TERRAIN.GRASS;
            }
        }
    }
    
    private void PlayFootstep(int terrain){
        DragonFootsteps = FMODUnity.RuntimeManager.CreateInstance("event:/DragonSounds/DragonFootsteps");
        DragonFootsteps.setParameterByName("DragonFootSteps", terrain);
        DragonFootsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.gameObject));
        DragonFootsteps.start();
        DragonFootsteps.release();
    }

    public void SelectAndPlayFootstep()
    {
        switch (currentTerrain)
        {
            case CURRENT_TERRAIN.STONE:
                Debug.Log("Walking on stone");
                PlayFootstep(1);
                break;

            case CURRENT_TERRAIN.GRASS:
                Debug.Log("Walking on grass");
                PlayFootstep(0);
                break;

            case CURRENT_TERRAIN.SAND:
                Debug.Log("Walking on Sand");
                PlayFootstep(2);
                break;
            default:
                PlayFootstep(0);
                break;
        }
    }
    public void PlayFireSound(){
        DragonFootsteps = FMODUnity.RuntimeManager.CreateInstance("event:/DragonSounds/DragonFire");
        DragonFootsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.gameObject));
        DragonFootsteps.start();
        DragonFootsteps.release();
    }
    public void PlayFlyingSound(){
        DragonFootsteps = FMODUnity.RuntimeManager.CreateInstance("event:/DragonSounds/DragonWings");
        DragonFootsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.gameObject));
        DragonFootsteps.start();
        DragonFootsteps.release();
    }
    
        public void SelectAndPlayLine()
    {
        PlayLine(height);
    }
        private void PlayLine(float height)
    {
        Debug.Log("Height " + height);
        idle_lines = FMODUnity.RuntimeManager.CreateInstance("event:/DragonSounds/DragonDialogue");
        idle_lines.setParameterByName("Height", height);
        idle_lines.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(Camera.gameObject));
        if (speed == 0)
        {
            idle_lines.start();
            idle_lines.release();
        }
    }
    
    void OnDrawGizmosSelected(){
                Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down* 10f);
    }
}

