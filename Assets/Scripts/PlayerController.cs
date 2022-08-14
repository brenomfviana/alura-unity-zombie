using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;

    private Rigidbody rigidbody;
    private Vector3 direction;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float zAxis = Input.GetAxis("Vertical");

        direction = new Vector3(xAxis, 0, zAxis);

        if (direction != Vector3.zero)
        {
            GetComponent<Animator>().SetBool("IsRunning", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("IsRunning", false);
        }
    }

    void FixedUpdate()
    {
        Vector3 velocity = direction * speed * Time.deltaTime;
        Vector3 position = rigidbody.position + velocity;
        rigidbody.MovePosition(position);
    }
}
