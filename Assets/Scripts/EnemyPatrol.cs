using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {   
        targetPoint = 0;
        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
         // Check if we've reached the target point
        if (transform.position == patrolPoints[targetPoint].position)
        {
            increaseTargetInt();
        }
        // Move towards the target point
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed  * Time.deltaTime);
        
        // Flip the sprite based on movement direction
        // Check if the next target point is to the right
        if (patrolPoints[targetPoint].position.x > transform.position.x)
        {
            // Flip the sprite to face right
            spriteRenderer.flipX = false; 
        }
        else if (patrolPoints[targetPoint].position.x < transform.position.x)
        {
            // Flip the sprite to face left
            spriteRenderer.flipX = true; 
        }
    }

    void increaseTargetInt()
    {
        targetPoint++;
        if (targetPoint >= patrolPoints.Length)
        {
            targetPoint = 0;
        }
    }
}
