using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private new Rigidbody rigidbody;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction, float speed)
    {
        Vector3 velocity = direction * speed * Time.deltaTime;
        Vector3 position = rigidbody.position + velocity;
        rigidbody.MovePosition(position);
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion newRotation = Quaternion.LookRotation(direction);
        rigidbody.MoveRotation(newRotation);
    }
}
