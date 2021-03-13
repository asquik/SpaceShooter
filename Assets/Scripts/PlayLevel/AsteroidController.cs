using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    // Start is called before the first frame update
    public float tumble;
    private Rigidbody rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * tumble;
        rb.velocity = transform.forward * -1 * speed;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            return;
        }

        if (other.tag == "Player")
        {
            return;
        }

        GameObject.Destroy(other.gameObject);
        GameObject.Destroy(this.gameObject);
    }
}
