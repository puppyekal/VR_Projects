using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class movePlayer : MonoBehaviour
{
    public SteamVR_Action_Vector2 moveValue;
    public SteamVR_Action_Boolean jump;
    public Rigidbody Rb;

    private Vector3 movementDir;

    private bool isGrounded;
    private bool isjumping;

    // Update is called once per frame
    void Update()
    {
        Debug.Log("groundCount : " + isGrounded);
        movementDir = Player.instance.hmdTransform.TransformDirection(new Vector3(moveValue.axis.x, 0, moveValue.axis.y));

        if ((jump.GetStateDown(SteamVR_Input_Sources.RightHand) || jump.GetStateDown(SteamVR_Input_Sources.LeftHand)))
        {
            isjumping = true;
        }
    }
    void FixedUpdate() {
        Move();
        Jump();
    }
    void Move() {
        if (Mathf.Abs(movementDir.x) + Mathf.Abs(movementDir.y) + Mathf.Abs(movementDir.z) > 0.3)
        {
            transform.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDir * 5.0f, Vector3.up);
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
