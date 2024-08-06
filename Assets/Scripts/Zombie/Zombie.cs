using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float health;
    public Transform target;
    public float speed;
    public float distance;

    public float attackRange = 1.0f;
    void Awake()
    {
        health = 3;
    }
    void Update()
    {
        // Destory if health is 0
        if (health <= 0)
        {
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
            Vector3 targetPosition = target.position;
            targetPosition.y = transform.position.y; // Keep the y position the same
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed);


        }

        // Rotate towards player
        transform.LookAt(target);
    }


    // Attack Player if in range
    private void Attack()
    {
        // Implement attack and attack animation
        Debug.Log("Attack");
    }
}
