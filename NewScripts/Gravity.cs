using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    // Start is called before the first frame update
    public bool center;

    public PhysicsManager Manager;
    float G;
    InfluentBody[] InfluentBodies;
     public Vector3 acceleration;
    public Vector3 velocity;
     public Vector3 position;
    Vector3 StartPosition;
    Rigidbody rig;
    [HideInInspector] public Vector3 RigAcc;
    Vector3 OldRigVel;
    InfluentBody ib;
    public float[] distances;
    void Start()
    {
        
        position = transform.position;
        StartPosition = position;
        G = Manager.G;
        InfluentBodies = Manager.InfluentBodies;
        distances = new float[InfluentBodies.Length];
        rig = GetComponent<Rigidbody>();
        if (GetComponent<InfluentBody>() != null)
            ib = GetComponent<InfluentBody>();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!center)
        {
            for (int i = 0; i < Manager.warp; i++)
            {
                Newton();
            }
            RigAcc = RigAcceleration();
            OldRigVel = rig.velocity;
            var newPosition = Manager.space.position + position;

            rig.MovePosition(newPosition);
        }
        else
        {
            transform.position = StartPosition;
            for (int i = 0; i < Manager.warp; i++)
            {
                Newton();
            }
            RigAcc = RigAcceleration();
            OldRigVel = rig.velocity;
            Manager.space.position = -position;
        }
        
    }

    Vector3 RigAcceleration()
    { 
        return (rig.velocity - OldRigVel) / Time.deltaTime;
    }
    void Newton()
    {
        acceleration = RigAcc;
        for(int i = 0; i < InfluentBodies.Length; i++)
        {
            if(ib!=null)
            {
                if(InfluentBodies[i] != ib)
                {
                    Vector3 r = InfluentBodies[i].transform.position - (Manager.space.position + position);
                    acceleration += (r.normalized) * (G * InfluentBodies[i].mass) / Mathf.Pow(Vector3.Distance((Manager.space.position + position), InfluentBodies[i].transform.position), 2);
                }
            }
            else
            {
                Vector3 r = InfluentBodies[i].transform.position - (Manager.space.position + position);
                acceleration += (r.normalized) * (G * InfluentBodies[i].mass) / Mathf.Pow(Vector3.Distance((Manager.space.position + position), InfluentBodies[i].transform.position), 2);
            }
            distances[i] = Vector3.Distance(position, InfluentBodies[i].transform.position);
        }
        Vector3 nextv = velocity + Time.deltaTime * acceleration;
        Vector3 nextpos = position + Time.deltaTime * nextv;
        position = nextpos;
        velocity = nextv;
        
    }
}
