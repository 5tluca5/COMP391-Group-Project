using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : Mover
{
    public Animator anim;
    public float chaseSpeed = 0.5f;

    private Transform playerTransform;
    private Vector3 startingPosition;
    private BoxCollider2D hitbox;
    private Rigidbody2D rb;

    private float curHP;
    private bool isDead = false;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
        playerTransform = GameManager.Instance.player.transform;
        startingPosition = this.transform.position;
        hitbox = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (isDead) return;

        ChasePlayer();
    }

    private void ChasePlayer()
    {
        Vector3 delta = playerTransform.position - this.transform.position;

        Vector3 chasingDirection = (delta.normalized);

        anim.SetFloat("Speed", Mathf.Abs(Mathf.Max(Mathf.Abs(chasingDirection.x), Mathf.Abs(chasingDirection.y))));

        // Prevent infinite blocking
        //if (getBlockTimer > getBlockTime && blockingObject != "Player")
        //{
        //    if (getBlockedHorizontally && pushDirection == Vector3.zero)
        //    {
        //        chasingDirection.y = (chaseSpeed) * Mathf.Sign(getBlockDirection.y);
        //    }
        //    else if (getBlockedVertically && pushDirection == Vector3.zero)
        //    {
        //        chasingDirection.x = (chaseSpeed) * Mathf.Sign(getBlockDirection.x);
        //    }

        //    if (getBlockedHorizontally && getBlockedVertically)
        //    {
        //        // Step back
        //        chasingDirection = (delta.normalized) * -2;
        //    }
        //}

        this.UpdateMotor(chasingDirection * chaseSpeed);
        //rb.AddForce(chasingDirection * chaseSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //    Vector2 enemyPosition = new Vector2(transform.position.x, transform.position.y);
            //    Vector2 bulletPosition = new Vector2(collision.transform.position.x, collision.transform.position.y);

            //    pushDirection = (enemyPosition - bulletPosition).normalized * collision.gameObject.GetComponent<Bullet>().speed;
            pushDirection = collision.relativeVelocity.normalized * collision.gameObject.GetComponent<Bullet>().speed;
            Debug.Log("Bullet force: " + pushDirection);

            PerformDead();
        }
    }

    IEnumerator PerformDead()
    {
        if (isDead) yield return null;

        isDead = true;

        rb.isKinematic = true;
        rb.simulated = false;

        anim.SetTrigger("Dead");

        yield return new WaitForSeconds(1f);

        GetComponent<SpriteRenderer>().DOFade(0, 0.2f).onComplete += () => { Destroy(this); };
    }
}
