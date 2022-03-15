using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonControler : MonoBehaviour
{
    //public varibles visable in the editor 
    //public int walkingSpeed;
    public float speed;
    public float flyingForce;
    public int maxHp;
    public int currentHp;
    public int flameAttackDmg;
    public int tailAttackDmg;
    public int biteAttackDmg;


    private Rigidbody rb;
    private bool isFLying = false;
    //These 3 are not likely needed (could be for sound?)
    private bool flameAttack= false;
    private bool tailAttack = false;
    private bool biteAttack= false;

    // Start is called before the first frame update
    void Start()
    {
        //Starting at full hp
        currentHp=maxHp;
        //Setting the rigidbody of the player
        rb= this.GetComponent<Rigidbody>();
    }

    // FixeedUpdate is called at the end of a frame better for physics 
    void Update()
    {
        movment();
    }

    void movment()
    {
        //movement in x and z 
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 dragonMovemnet = new Vector3(hor*speed,0f,ver*speed);
        //When space is pressed we move up in the y axis
        if(Input.GetKey(KeyCode.Space))
        {
            //dragonMovemnet = new Vector3(dragonMovemnet.x,flySpeed,dragonMovemnet.y);
            rb.AddForce(Vector3.up * flyingForce);
        }
        
        transform.Translate(dragonMovemnet*Time.deltaTime,Space.World);
    }
    void flyingMode()
    {
        //dragons flying tag is set to true 
        isFLying = true;
        //Gravity is turned off so we can fly
        rb.useGravity = false;
    }

    void attacking(string attack)
    {
        switch(attack)
        {
            case "flameAttack":
                flameAttack = true;
                break;

            case "tailAttack":
                tailAttack = true;
                break;
            
            case "biteAttack":
                biteAttack = true;
                break;
        }
    }
}
