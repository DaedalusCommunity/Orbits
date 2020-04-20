using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfluentBody : MonoBehaviour
{
    public float SurfaceAccel;
    public float mass;
    public PhysicsManager pm;
    // Start is called before the first frame update
    void Start()
    {
       if(SurfaceAccel != 0)
        {
            var r = transform.lossyScale.x / 100;
            mass = (SurfaceAccel * r * r) / pm.G;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
