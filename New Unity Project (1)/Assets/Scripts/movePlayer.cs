using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class movePlayer : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveValue;
    public SteamVR_Action_Boolean jump;
    public SteamVR_Action_Boolean turnLeft;
    public SteamVR_Action_Boolean turnRight;
    public Rigidbody Rb;
    
    private bool isGrounded;
    private bool isjumping;
    
    public float speed = 10.0f;
    public float airVelocity = 8f;
    public float gravity = 10.0f;
    public float maxVelocityChange = 10.0f;
    public float jumpHeight = 2.0f;
    public float maxFallSpeed = 20.0f;
    public float rotateSpeed = 25f; //Speed the player rotate
    private Vector3 moveDir;
    private Rigidbody rb;

    private float distToGround;

    private bool canMove = true; //If player is not hitted
    private bool isStuned = false;
    private bool wasStuned = false; //If player was stunned before get stunned another time
    private float pushForce;
    private Vector3 pushDir;

    public Vector3 checkPoint;
    private bool slide = false;

    //    void Start()
    //    {
    //        // get the distance to ground
    //        distToGround = GetComponent<Collider>().bounds.extents.y;
    //        Debug.Log("1 : " + GetComponent<Collider>());
    //        Debug.Log("2 : " + GetComponent<Collider>().bounds);
    //        Debug.Log("3 : " + GetComponent<Collider>().bounds.extents);
    //        Debug.Log("4 : " + GetComponent<Collider>().bounds.extents.y);
    //    }

    //    bool IsGrounded()
    //    {
    //        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    //    }

    //    void Awake()
    //    {
    //        rb = GetComponent<Rigidbody>();
    //        rb.freezeRotation = true;
    //        rb.useGravity = false;

    //        checkPoint = transform.position;
    //    }

    //    void FixedUpdate()
    //    {
    //        if (transform.position.y <= -20) {
    //            LoadCheckPoint();
    //        }
    //        if (canMove)
    //        {
    //            if (Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y) + Mathf.Abs(moveDir.z) > 0.3)
    //            {
    //                if (IsGrounded())
    //                {
    //                    // Calculate how fast we should be moving
    //                    Vector3 targetVelocity = moveDir;
    //                    targetVelocity *= speed;

    //                    // Apply a force that attempts to reach our target velocity
    //                    Vector3 velocity = rb.velocity;
    //                    if (targetVelocity.magnitude < velocity.magnitude) //If I'm slowing down the character
    //                    {
    //                        targetVelocity = velocity;
    //                        rb.velocity /= 1.1f;
    //                    }
    //                    Vector3 velocityChange = (targetVelocity - velocity);
    //                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
    //                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
    //                    velocityChange.y = 0;
    //                    if (!slide)
    //                    {
    //                        if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
    //                            rb.AddForce(velocityChange, ForceMode.VelocityChange);
    //                    }
    //                    else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
    //                    {
    //                        rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
    //                    }
    //                    // Jump
    //                    Jump(velocity);
    //                }
    //                else//공중??
    //                {
    //                    if (!slide)
    //                    {
    //                        Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
    //                        Vector3 velocity = rb.velocity;
    //                        Vector3 velocityChange = (targetVelocity - velocity);
    //                        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
    //                        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
    //                        rb.AddForce(velocityChange, ForceMode.VelocityChange);
    //                        if (velocity.y < -maxFallSpeed)
    //                            rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
    //                    }
    //                    else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
    //                    {
    //                        rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            rb.velocity = pushDir * pushForce;
    //        }
    //        // We apply gravity manually for more tuning control
    //        rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
    //    }
    //    private void Jump(Vector3 velocity) {
    //        if ((jump.GetStateDown(SteamVR_Input_Sources.RightHand) || jump.GetStateDown(SteamVR_Input_Sources.LeftHand)))
    //        {
    //            if (IsGrounded())
    //            {
    //                rb.velocity = new Vector3(velocity.x, 10, velocity.z);
    //            }
    //        }
    //    }
    //    private void Update()
    //    {
    //        float h = Input.GetAxis("Horizontal");
    //        float v = Input.GetAxis("Vertical");
    //        moveDir = Player.instance.hmdTransform.TransformDirection(new Vector3(moveValue.axis.x, 0, moveValue.axis.y));


    //        RaycastHit hit;
    //        if (Physics.Raycast(transform.position, -Vector3.up, out hit, distToGround + 0.1f))
    //        {
    //            if (hit.transform.tag == "Slide")
    //            {
    //                slide = true;
    //            }
    //            else
    //            {
    //                slide = false;
    //            }
    //        }
    //    }
    //    float CalculateJumpVerticalSpeed()
    //    {
    //        // From the jump height and gravity we deduce the upwards speed 
    //        // for the character to reach at the apex.
    //        return Mathf.Sqrt(2 * jumpHeight * gravity);
    //    }

    //    public void HitPlayer(Vector3 velocityF, float time)
    //    {
    //        rb.velocity = velocityF;

    //        pushForce = velocityF.magnitude;
    //        pushDir = Vector3.Normalize(velocityF);
    //        StartCoroutine(Decrease(velocityF.magnitude, time));
    //    }

    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        checkPoint = transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("groundCount : " + isGrounded);
        Debug.Log("groundCount : " + Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f));
        moveDir = Player.instance.hmdTransform.TransformDirection(new Vector3(moveValue.axis.x, 0, moveValue.axis.y));

        if ((jump.GetStateDown(SteamVR_Input_Sources.RightHand) || jump.GetStateDown(SteamVR_Input_Sources.LeftHand)))
        {
            isjumping = true;
        }
    }
    void FixedUpdate()
    {
        Move();
        if (IsGrounded())
        {
            Jump();
        }
        if (transform.position.y <= -10)
        {
            LoadCheckPoint();
        }
    }
    public void LoadCheckPoint()
    {
        transform.position = checkPoint;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    void Move()
    {
        if (Mathf.Abs(moveDir.x) + Mathf.Abs(moveDir.y) + Mathf.Abs(moveDir.z) > 0.3)
        {
            transform.position += Vector3.ProjectOnPlane(Time.deltaTime * moveDir * 7.0f, Vector3.up);
        }
    }
    void Jump()
    {
        if (!isjumping) return;
        Rb.AddForce(new Vector3(0, 250, 0));
        isjumping = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("ground"))
        {
            isGrounded = false;
        }
    }
}
