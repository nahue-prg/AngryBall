using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanque : MonoBehaviour
{
    //public Rigidbody rb;
    //public float speed = 10f;
    //public float turnSpeed = 100f;
    public Rigidbody rb;
    public float speed = 10f;
    public float turnSpeed = 100f;
    public float maxGroundAngle = 45f;
    public float maxSpeedOnAir = 2f;
    public LayerMask groundLayer;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
        rb = GetComponent<Rigidbody>();
        //rb.drag = 0.1f;
        //rb.angularDrag = 0.5f;
        rb.centerOfMass = new Vector3(0f, -0.5f, 0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movimiento();
    }

    public void Movimiento()
    {
        //float forward = Input.GetAxis("Vertical") * speed;
        //float turn = Input.GetAxis("Horizontal") * turnSpeed;
        //rb.AddRelativeForce(0f, 0f, forward);
        //rb.AddTorque(0f, turn, 0f);

        // detectar si el tanque está en contacto con el suelo
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f, groundLayer))
        {
            float groundAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (groundAngle <= maxGroundAngle)
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }

        // movimiento del tanque
        float forward = Input.GetAxis("Vertical") * speed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;
        if (isGrounded)
        {
            rb.AddRelativeForce(0f, 0f, forward);
            rb.AddTorque(0f, turn, 0f);
        }
        else
        {
            float maxAirSpeed = maxSpeedOnAir * speed;
            Vector3 airVelocity = rb.velocity + transform.forward * forward * Time.deltaTime;
            airVelocity.x = Mathf.Clamp(airVelocity.x, -maxAirSpeed, maxAirSpeed);
            airVelocity.z = Mathf.Clamp(airVelocity.z, -maxAirSpeed, maxAirSpeed);
            rb.velocity = airVelocity;
            rb.angularVelocity = Vector3.zero;
        }
    }
}