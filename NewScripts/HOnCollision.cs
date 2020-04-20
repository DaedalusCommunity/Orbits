using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOnCollision : MonoBehaviour
{
    
    Gravity grav;
    GravitySimple gravs;
    public float CR;
    float mass;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Gravity>() != null)
            grav = GetComponent<Gravity>();
        else
            gravs = GetComponent<GravitySimple>();
        mass = GetComponent<Rigidbody>().mass;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<HOnCollision>() != null) {
            
        if(collision.gameObject.GetComponent<Gravity>() != null)
            {
                Gravity other = collision.gameObject.GetComponent<Gravity>();
                other = collision.gameObject.GetComponent<Gravity>();
                var MCR = (CR + other.GetComponent<HOnCollision>().CR) / 2;
                var mb = other.GetComponent<Rigidbody>().mass;
                var ua = grav.velocity;
                var ub = other.velocity;
                var va = (MCR * mb * (ub - ua) + mass * ua + mb * ub) / (mass + mb);

                var dva = va - ua;


                var Fa = mass * (dva / Time.deltaTime);

                GetComponent<Rigidbody>().AddForceAtPosition(Fa, collision.GetContact(0).point);
            }
            else
            {
                GravitySimple other = collision.gameObject.GetComponent<GravitySimple>();
                other = collision.gameObject.GetComponent<GravitySimple>();
                var MCR = (CR + other.GetComponent<HOnCollision>().CR) / 2;
                var mb = other.GetComponent<Rigidbody>().mass;
                var ua = gravs.velocity;
                var ub = other.velocity;
                var va = (MCR * mb * (ub - ua) + mass * ua + mb * ub) / (mass + mb);

                var dva = va - ua;


                var Fa = mass * (dva / Time.deltaTime);

                GetComponent<Rigidbody>().AddForceAtPosition(Fa, collision.GetContact(0).point);
            }
       
        //other.GetComponent<Rigidbody>().AddForceAtPosition(Fb, collision.GetContact(0).point);
        }
        else if(collision.gameObject.GetComponent<InfluentBody>() != null) //This should not happen in theory
        {
            if(grav != null)
            {
                grav.enabled = false;
            }
            else
            {
                gravs.enabled = false;
            }
            
            GetComponent<Rigidbody>().isKinematic = true;

        }
    }
}
