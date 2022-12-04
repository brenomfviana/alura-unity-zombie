using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 30;

    private MovementController movement;

    void Start()
    {
        movement = GetComponent<MovementController>();
    }

    void FixedUpdate()
    {
        Vector3 direction = transform.forward;
        movement.Move(direction, Speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Inimigo")
        {
            ZombieController enemy = other.GetComponent<ZombieController>();
            enemy.TakeDamage(1);
        }
        Destroy(gameObject);
    }
}
