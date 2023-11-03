using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    public Animator anim;
    public float chaseSpeed = 0.5f;

    private Transform playerTransform;
    private Vector3 startingPosition;
    private BoxCollider2D hitbox;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = this.transform.position;
        hitbox = GetComponentInChildren<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        Vector3 delta = playerTransform.position - this.transform.position;

        Vector3 chasingDirection = (delta.normalized);

        anim.SetFloat("Speed", Mathf.Abs(chasingDirection.x));
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
    }
}
