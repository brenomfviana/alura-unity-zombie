using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    private static string IS_ATTACKING = "IsAttacking";
    private static string MOVING = "Moving";

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack(bool state)
    {
        animator.SetBool(IS_ATTACKING, state);
    }

    public void Move(float speed)
    {
        animator.SetFloat(MOVING, speed);
    }
}
