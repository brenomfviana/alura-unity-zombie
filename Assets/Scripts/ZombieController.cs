using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public GameObject Player;

    public float Speed = 5;

    private Animator animator;
    private Movement movement;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        movement = GetComponent<Movement>();
        int zombieType = Random.Range(1, 28);
        transform.GetChild(zombieType).gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 zombiePosition = transform.position;

        Vector3 direction = (playerPosition - zombiePosition).normalized;
        movement.Rotate(direction);

        float distance = Vector3.Distance(zombiePosition, playerPosition);
        if (distance > 2.5f)
        {
            movement.Move(direction, Speed);
            animator.SetBool("IsAttacking", false);
        }
        else
        {
            animator.SetBool("IsAttacking", true);
        }
    }

    void HitPlayer()
    {
        PlayerController pc = Player.GetComponent<PlayerController>();
        int damage = Random.Range(20, 30);
        pc.TakeDamage(damage);
    }
}
