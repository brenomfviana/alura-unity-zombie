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

    public float AttackDistance = 2.5f;
    public int MinDamage = 20;
    public int MaxDamage = 30;
    public AudioClip DeathSound;

    private int MinZombieLook = 1;
    private int MaxZombieLook = 28;

    void Start()
    {
        Player = GameObject.FindWithTag(Tags.PLAYER);

        status = GetComponent<Status>();

        movement = GetComponent<MovementController>();
        animator = GetComponent<AnimatorController>();

        SetZombieLook();
    }

    void FixedUpdate()
    {
        Vector3 direction = Direction(Player.transform.position);

        if (IsCloseToAttack(Player.transform.position))
        {
            animator.Attack(true);
        }
        else
        {
            movement.Move(direction, status.Speed);
            animator.Attack(false);
        }

        movement.Rotate(direction);
    }

    private void SetZombieLook()
    {
        int look = Random.Range(MinZombieLook, MaxZombieLook);
        transform.GetChild(look).gameObject.SetActive(true);
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

    bool IsCloseToAttack(Vector3 target)
    {
        return Distance(Player.transform.position) <= AttackDistance;
    }

    void HitPlayer()
    {
        PlayerController pc = Player.GetComponent<PlayerController>();
        int damage = Random.Range(MinDamage, MaxDamage);
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
