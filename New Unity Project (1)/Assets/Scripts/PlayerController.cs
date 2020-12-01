using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    //public SteamVR_Input_Sources MovementHand;//Set Hand To Get Input From
    public SteamVR_Action_Vector2 touchpadInput;
    public Transform cameraTransform;
    private CapsuleCollider capsuleCollider;

    //public SteamVR_Action_Boolean JumpAction;//get jump action
    //public float jumpHeight;//set height in meters that we can jump

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        //Rigidbody RBody = GetComponent<Rigidbody>();

        Vector3 movementDir = Player.instance.hmdTransform.TransformDirection(new Vector3(touchpadInput.axis.x, 0, touchpadInput.axis.y));
        transform.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDir * 2.0f, Vector3.up);

        float distanceFromFloor = Vector3.Dot(cameraTransform.localPosition, Vector3.up);
        capsuleCollider.height = Mathf.Max(capsuleCollider.radius, distanceFromFloor);

        capsuleCollider.center = cameraTransform.localPosition - 0.5f * distanceFromFloor * Vector3.up;

        //if (JumpAction.GetStateDown(MovementHand))
        //{
        //    float jumpSpeed = Mathf.Sqrt(2 * jumpHeight * 9.81f);
        //    RBody.AddForce(0, jumpSpeed, 0, ForceMode.VelocityChange);
        //}
    }
}
