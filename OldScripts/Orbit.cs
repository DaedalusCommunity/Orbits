using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    public GameObject orb;
    public float a;
    public float b;
    public float c;
    [SerializeField] float alpha;
    [SerializeField] float deltaAlpha;
    public Vector3 center;
    public Transform focus1;
    [SerializeField] float G;
    [SerializeField] float M;
    float r;
    Rigidbody rb;
    [SerializeField] Vector3 Velocity;
    [SerializeField] float speed;
    [SerializeField] float XZAngle;
    [SerializeField] float XZGrad;
    [SerializeField] float tanXZ;
    Vector3 periapsis;
    Vector3 apoapsis;
    public float beta;
    public float ArgOfPer;
    VisualizeOrbit VO;

    public float gamma;
    public float delta;

    public TextMesh tm;
    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        //VO = orb.GetComponent<VisualizeOrbit>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*periapsis = VO.periapsis;
        apoapsis = VO.apoapsis;*/

       
       

        MoveObject();
        speed = Mathf.Sqrt(G * M * ((2 / r) - (1 / a)));
        Vector3 backRot = Quaternion.Euler(-gamma, -beta, -delta) * transform.position;

        XZAngle = Mathf.Atan(-((b * (backRot.x - center.x)) / (2 * a * Mathf.Sqrt(a * a - (backRot.x - center.x) * (backRot.x - center.x)))));
        XZGrad = XZAngle * Mathf.Rad2Deg;
        tanXZ = -((b * (backRot.x - center.x)) / (2 * a * Mathf.Sqrt(a * a - (backRot.x - center.x) * (backRot.x - center.x))));
        Velocity = new Vector3(speed * Mathf.Cos(XZAngle),0 ,speed * Mathf.Sin(XZAngle));
        //Debug.DrawLine(transform.position, transform.position + Velocity*10000);
        //tm.text = XZGrad.ToString();
    }
    void CalculateVelocity()
    {

    }

    void MoveObject()
    {
        if (a >= b)
        {
            c = Mathf.Sqrt(Mathf.Abs(a * a - b * b));
            center = new Vector3(focus1.position.x + c, 0, focus1.position.z);

            r = Vector3.Distance(focus1.position, transform.position);
            Vector3 point = new Vector3(center.x + a * Mathf.Cos(alpha), 0, center.z + b * Mathf.Sin(alpha));
            rb.MovePosition(Quaternion.Euler(gamma, beta, delta) * point);


            deltaAlpha = (Mathf.Sqrt(Mathf.Abs(2 * G * M * (1 / r - 1 / (2 * a))) * 180 * Time.deltaTime) / Mathf.PI * (3 * (a + b) - Mathf.Sqrt((3 * a + b) * (a + 3 * b))));

            alpha += deltaAlpha;
        }
        else
        {

            c = Mathf.Sqrt(Mathf.Abs(b * b - a * a));
            center = new Vector3(focus1.position.x, 0, focus1.position.z + c);

            r = Vector3.Distance(focus1.position, transform.position);
            Vector3 point = new Vector3(center.x + a * Mathf.Cos(alpha), 0, center.z + b * Mathf.Sin(alpha));
            rb.MovePosition(Quaternion.Euler(gamma, beta, delta) * point);


            deltaAlpha = (Mathf.Sqrt(Mathf.Abs(2 * G * M * (1 / r - 1 / (2 * b))) * 180 * Time.deltaTime) / Mathf.PI * Mathf.Sqrt(Mathf.Abs((a * a + b * b) / 2)));

            alpha += deltaAlpha;
        }
    }
}
