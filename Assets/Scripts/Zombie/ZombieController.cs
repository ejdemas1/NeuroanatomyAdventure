using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float health;
    public Transform target;
    public float speed;
    public float distance;

    Animator animator;

    public float attackRange = 5.0f;
    void Awake()
    {
        health = 3;
        animator = GetComponentInChildren<Animator>();
    }
    void Update()
    {
        // Destory if health is 0
        if (health <= 0)
        {
            // Death animation wait and then destory object
            Destroy(gameObject);
        }

        // Attack player if in range, if not follow if in find range
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= attackRange)
        {
            Attack();
        }
        else
        {
            // Follow and animate walk
            animator.SetBool("isWalking", true);
            Vector3 targetPosition = target.position;
            targetPosition.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
        }

        // Rotate towards player
        transform.LookAt(target);
    }


    // Attack Player if in range
    private void Attack()
    {
        // Implement attack and attack animation
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
    }
}
