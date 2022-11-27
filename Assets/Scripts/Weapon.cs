using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float speed = 30;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 position = rigidbody.position;
        Vector3 direction = transform.forward;
        rigidbody.MovePosition(position + direction * speed * Time.deltaTime);
    }
}
