using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 30;
    public int Damage = 1;

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
        if (other.tag == Tags.ENEMY)
        {
            ZombieController enemy = other.GetComponent<ZombieController>();
            enemy.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
