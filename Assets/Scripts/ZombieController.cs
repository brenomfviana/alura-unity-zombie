using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour, IKillable
{
    [HideInInspector]
    public Status status;

    private GameObject player;

    private MovementController movement;
    private AnimatorController animator;

    public float SightDistance = 15.0f;
    public float AttackDistance = 2.5f;
    public float WanderingRange = 10.0f;
    public int MinDamage = 20;
    public int MaxDamage = 30;
    public AudioClip DeathSound;

    private int MinZombieLook = 1;
    private int MaxZombieLook = 28;

    private Vector3 direction;
    
    private Vector3 randomPosition;
    private float wanderingTime = 0;
    private float positionGeneratingTime = 4;

    private static float DISTANCE_ERROR = 0.05f;

    void Start()
    {
        player = GameObject.FindWithTag(Tags.PLAYER);

        status = GetComponent<Status>();

        movement = GetComponent<MovementController>();
        animator = GetComponent<AnimatorController>();

        SetZombieLook();
    }

    void FixedUpdate()
    {
        if (CanAttack())
        {
            Attack();
        }
        else if (CanSee())
        {
            ChasePlayer();
        }
        else
        {
            Wander();
        }

        Move();
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
        return (target - Position());
    }

    float Distance(Vector3 target)
    {
        return Vector3.Distance(Position(), target);
    }

    bool CanAttack()
    {
        return Distance(player.transform.position) <= AttackDistance;
    }

    bool CanSee()
    {
        return Distance(player.transform.position) <= SightDistance;
    }

    void Attack()
    {
        animator.Attack(true);
    }

    void Move()
    {
        animator.Move(direction.magnitude);
    }

    void ChasePlayer()
    {
        direction = Direction(player.transform.position);

        Vector3 ndirection = direction.normalized;
        movement.Move(ndirection, status.Speed);
        movement.Rotate(ndirection);

        animator.Attack(false);
    }

    bool ReachPosition(Vector3 target)
    {
        return Distance(target) <= DISTANCE_ERROR;
    }

    void Wander()
    {
        wanderingTime -= Time.deltaTime;
        if (wanderingTime <= 0)
        {
            randomPosition = RandomPosition();
            wanderingTime += positionGeneratingTime;
        }
        if (!ReachPosition(randomPosition))
        {
            direction = Direction(randomPosition);
            Vector3 ndirection = direction.normalized;
            movement.Move(ndirection, status.Speed);
            movement.Rotate(ndirection);
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 position = Random.insideUnitSphere * WanderingRange;
        position += transform.position;
        position.y = transform.position.y;
        return position;
    }

    void HitPlayer()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
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
