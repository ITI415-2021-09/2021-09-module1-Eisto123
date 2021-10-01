using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    static private Player S;
    public float speed;
    public bool controlMode;
    public Rigidbody rb;
    public bool onGround = true;
    public static Vector3 initialPosition = new Vector3(-120, 6, 0);


    private void Start()
    {
        S = this;
    }
    private void FixedUpdate()
    {
        if (controlMode)
        {
            Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            movement.Normalize();
            movement *= speed * Time.deltaTime;
            transform.Translate(movement.y, 0, -movement.x);
        }
    }
    void Update()
    {
        if(Input.GetButtonDown("Jump") && onGround)
            {
                rb.AddForce(new Vector3(0, 55, 0), ForceMode.Impulse);
                onGround = false;
            }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            Prototype.CubeCollected();
        }
    }

    public static void enableControl()
    {
        S.controlMode = true;
    }
    public static void disableControl()
    {
        S.controlMode = false;
    }


}
