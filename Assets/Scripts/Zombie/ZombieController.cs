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

        // Check if health is 0 and trigger death
        if (health <= 0)
        {
            TriggerDeath();
            return;
        }

        // Attack player if in range, if not follow if in find range
        float dist = Vector3.Distance(transform.position, target.position);
        if (dist <= attackRange)
        {
            if (!animator.GetBool("isAttacking"))
            {
                Attack();
            }
        }
        else
        {
            // Stop attacking if player is out of range
            if (animator.GetBool("isAttacking"))
            {
                StopAttack();
            }

            if (!animator.GetBool("isDead"))
            {
                animator.SetBool("isWalking", true);
                Vector3 targetPosition = target.position;
                targetPosition.y = transform.position.y;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);
            }
        }

        // Rotate towards player
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection);

    }

    // Attack Player if in range
    private void Attack()
    {
        // Implement attack and attack animation
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
    }

    // Stop attacking
    private void StopAttack()
    {
        animator.SetBool("isAttacking", false);
        animator.SetBool("isWalking", true);

    }

    private void TriggerDeath()
    {
        if (!animator.GetBool("isDead"))
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("isDead", true);
            animator.Play("Death_Tumor_H", 0, 0f);
            StartCoroutine(Death());
        }
    }

    public void Hit()
    {
        // Implement hit and hit animation
        health--;
        if (health >= 1)
        {
            // animator.SetBool("isWalking", false);
            // animator.SetBool("isAttacking", false);
            // animator.SetBool("isHit", true);

        }
    }

    // Let animation play before destorying
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2.5f);

        Destroy(gameObject);
    }
}
