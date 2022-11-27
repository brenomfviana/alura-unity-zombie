using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject player;
    public float speed = 5;

    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {   
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance > 2.5f) {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Vector3 currentPosition = rigidbody.position;
            rigidbody.MovePosition(currentPosition + direction * speed * Time.deltaTime);
            Quaternion newRotation = Quaternion.LookRotation(direction);
            rigidbody.MoveRotation(newRotation);
        }
    }
}
