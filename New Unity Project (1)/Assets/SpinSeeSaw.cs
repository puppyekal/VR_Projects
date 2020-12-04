using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSeeSaw : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.rotation == Quaternion.Euler(new Vector3(0, 0, 0))) {
            GetComponent<Rigidbody>().WakeUp();
        }
        if (transform.rotation.z > 0.5 || transform.rotation.z < -0.5)
        {
            Debug.Log(transform.rotation.z);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().Sleep();
        }
    }
}
