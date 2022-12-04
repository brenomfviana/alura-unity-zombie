using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack(bool state)
    {
        animator.SetBool("IsAttacking", state);
    }

    public void Move(float speed)
    {
        animator.SetFloat("Moving", speed);
    }
}
