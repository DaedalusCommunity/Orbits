using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeOrbit : MonoBehaviour
{
    LineRenderer line;
    [SerializeField] GameObject body;
    [SerializeField] float alpha;
    private float a;
    private float b;
    private float c;
    Vector3 center;
    public Vector3 apoapsis;
    public Vector3 periapsis;

    public float distanceFromFocus;
    [SerializeField] float error;
    [SerializeField] GameObject ApoSign; //Apoapsis Indicator
    [SerializeField] GameObject PeriSign; //Periapsis Indicator
    public float beta;
    public float gamma;
    public float delta;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        a = body.GetComponent<Orbit>().a;
        b = body.GetComponent<Orbit>().b;
        c = body.GetComponent<Orbit>().c;
        center = body.GetComponent<Orbit>().center;
        beta = body.GetComponent<Orbit>().beta;
        gamma = body.GetComponent<Orbit>().gamma;
        delta = body.GetComponent<Orbit>().delta;
        body.GetComponent<Orbit>().orb = transform.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        a = body.GetComponent<Orbit>().a; //To Update in Real Time (there's a better way!!!)
        b = body.GetComponent<Orbit>().b;
        c = body.GetComponent<Orbit>().c;
        center = body.GetComponent<Orbit>().center;
        beta = body.GetComponent<Orbit>().beta;
        gamma = body.GetComponent<Orbit>().gamma;
        delta = body.GetComponent<Orbit>().delta;

        for (int i=0; i < line.positionCount; i++)
        {
            
            alpha = alpha + 360 / (line.positionCount -1) * Mathf.Deg2Rad;
            //line.SetPosition(i, new Vector3(center.x + a * Mathf.Cos(alpha), 0, center.z + b * Mathf.Sin(alpha)));
            Vector3 point = new Vector3(center.x + a * Mathf.Cos(alpha), 0, center.z + b * Mathf.Sin(alpha));
            line.SetPosition(i, Quaternion.Euler(gamma, beta, delta) *point);

            distanceFromFocus = Vector3.Distance(new Vector3(center.x + a * Mathf.Cos(alpha), 0, center.z + b * Mathf.Sin(alpha)), body.GetComponent<Orbit>().focus1.position);

            if (a>=b)
            {
                
                if(distanceFromFocus < a + c + error&& distanceFromFocus > a + c - error)
                {
                    apoapsis = Quaternion.Euler(gamma, beta, delta) * point;
                    ApoSign.transform.position = apoapsis;
                }

            }
            else {
                if (distanceFromFocus < b + c + error && distanceFromFocus > b + c - error)
                {
                    apoapsis = Quaternion.Euler(gamma, beta, delta) * point;
                    ApoSign.transform.position = apoapsis;
                }

            }

            if (a >= b)
            {
                if (distanceFromFocus < a - c + error && distanceFromFocus > a - c - error)
                {
                    periapsis = Quaternion.Euler(gamma, beta, delta) * point;
                    PeriSign.transform.position = periapsis;
                }

            }
            else
            {
                if (distanceFromFocus < b - c + error && distanceFromFocus > b - c - error)
                {
                    periapsis = Quaternion.Euler(gamma, beta, delta) * point;
                    PeriSign.transform.position = periapsis;
                }

            }

        }
        alpha = 0;
    }
}
