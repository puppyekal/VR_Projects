using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinArea : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime / 0.01f, 0, Space.Self);
    }
}
