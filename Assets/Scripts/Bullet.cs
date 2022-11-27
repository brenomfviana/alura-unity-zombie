using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 direction = transform.forward;
        Vector3 velocity = direction * speed * Time.deltaTime;
        Vector3 position = rigidbody.position + velocity;
        rigidbody.MovePosition(position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Inimigo")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
