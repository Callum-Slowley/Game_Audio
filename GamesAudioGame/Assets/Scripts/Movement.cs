using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody rb;
    public bool isFlying;
    public float movementSpeed = 100f;
    public float resetSpeed = 100f;
    public float shiftSpeed = 150f;
    public float controlSpeed = 50f;

    public float horizSen = 2f;
    public float vertSen = 2f;

    public float yaw = 0f;
    private float minYaw = -60f;
    private float maxYaw = 60f;
    public float pitch = 0f;
    private float minPitch = -90;
    private float maxPitch = 90f;

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
        }
        else
        {
            rb.useGravity = true;
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
}
