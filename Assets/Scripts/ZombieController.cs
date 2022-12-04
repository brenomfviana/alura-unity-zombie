using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour, IKillable
{
    public GameObject Player;

    [HideInInspector]
    public Status status;

    private MovementController movement;
    private AnimatorController animator;

    public AudioClip DeathSound;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");

        status = GetComponent<Status>();

        movement = GetComponent<MovementController>();
        animator = GetComponent<AnimatorController>();

        SetZombieLook();
    }

    void FixedUpdate()
    {
        Vector3 direction = Direction(Player.transform.position);

        if (Distance(Player.transform.position) > 2.5f)
        {
            movement.Move(direction, status.Speed);
            animator.Attack(false);
        }
        else
        {
            animator.Attack(true);
        }

        movement.Rotate(direction);
    }

    private void SetZombieLook()
    {
        int zombieType = Random.Range(1, 28);
        transform.GetChild(zombieType).gameObject.SetActive(true);
    }

    Vector3 Position()
    {
        return transform.position;
    }

    Vector3 Direction(Vector3 target)
    {
        return (target - Position()).normalized;
    }

    float Distance(Vector3 target)
    {
        return Vector3.Distance(Position(), target);
    }

    void HitPlayer()
    {
        PlayerController pc = Player.GetComponent<PlayerController>();
        int damage = Random.Range(20, 30);
        pc.TakeDamage(damage);
    }

    public void TakeDamage(int damage)
    {
        status.Health -= damage;

        if (status.Health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
        AudioController.instance.PlayOneShot(DeathSound);
    }
}
