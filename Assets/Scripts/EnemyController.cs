using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public Transform player; 
    public float moveSpeed = 3f; 
    public Collider2D areaBatas; // Batas

    private Rigidbody2D rb;

    private Vector3 OriginalPosition;

    public SpriteRenderer spriteRenderer;

    private float stopdistance = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        OriginalPosition = transform.position;
    }

    void Update()
    {
        //  posisi player berada dalam area batas
        if (areaBatas == null || areaBatas.OverlapPoint(player.position))
        {
            float distanceToTarget = Vector2.Distance(transform.position, player.position);
            if (distanceToTarget > stopdistance)
            {
                Vector2 direction = (player.position - transform.position).normalized;

                // Gerakkan enemy ke arah player
                if (direction.x < 0)
                {
                    //flip sptitenya
                    spriteRenderer.flipX = true;
                }
                else if (direction.x > 0)

                {

                    //unflip sptitenya
                    spriteRenderer.flipX = false;

                }
                rb.velocity = direction * moveSpeed;
            }
            // Hitung arah menuju player
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
        else
        {
            float distanceToTarget = Vector2.Distance(transform.position, OriginalPosition);

            // hentikan enemy jika diluar batas
            if (distanceToTarget > stopdistance)
            {
                Vector2 direction = (OriginalPosition - transform.position).normalized;
                if (direction.x < 0)
                {
                    //flip sptitenya
                    spriteRenderer.flipX = true;
                }
                else if (direction.x > 0)

                {

                    //unflip sptitenya
                    spriteRenderer.flipX = false;

                }
                rb.velocity = direction * moveSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;

            }

        }
    }
}