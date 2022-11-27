using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject player;
    public float speed = 5;

    private new Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = player.transform.position;
        Vector3 zombiePosition = transform.position;
        float distance = Vector3.Distance(zombiePosition, playerPosition);
        if (distance > 2.5f)
        {
            Vector3 direction = (playerPosition - zombiePosition).normalized;
            Vector3 velocity = direction * speed * Time.deltaTime;
            Vector3 position = rigidbody.position + velocity;
            rigidbody.MovePosition(position);
            Quaternion newRotation = Quaternion.LookRotation(direction);
            rigidbody.MoveRotation(newRotation);
        }
    }
}
