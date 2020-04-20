using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualForce : MonoBehaviour
{
    Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rig.AddForce(Vector3.up * Input.GetAxis("Vertical"));
    }
}
