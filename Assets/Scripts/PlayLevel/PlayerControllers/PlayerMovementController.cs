using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float minX, maxX, minZ, maxZ;
}

public class PlayerMovementController : MonoBehaviour
{
    public Boundary boundary;
    public float speed, tilt;
    private Rigidbody rb;    
    private float finalSpeed;
    [SerializeField]
    private PlayLevelController controller;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    public void CalculateSpeed(float speedDeduction)
    {
        finalSpeed = speed * (1 - speedDeduction / 100.0f);
    }

    void FixedUpdate()
    {

        //transform.Translate(speed * Time.deltaTime * Input.GetAxis("Horizontal"), 0, 0);
        float horInput = Input.GetAxis("Horizontal");
        float verInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horInput, 0, verInput) * finalSpeed;
        rb.position = new Vector3
        (
            Mathf.Clamp(rb.position.x, boundary.minX, boundary.maxX),
            0,
            Mathf.Clamp(rb.position.z, boundary.minZ, boundary.maxZ)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            controller.Hit();
            Destroy(other.gameObject);
        }
    }

    public void Stop()
    {
        finalSpeed = 0;
        tilt = 0;
    }
}
