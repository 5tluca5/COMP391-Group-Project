using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : Mover
{
    public Animator anim;
    public SpriteRenderer mainSprite;
    public BoxCollider2D attackDetecter;
    public float chaseSpeed = 2f;

    private Transform playerTransform;
    private Vector3 startingPosition;
    private BoxCollider2D hitbox;
    private Rigidbody2D rb;

    private ReactiveProperty<float> curHP = new ReactiveProperty<float>();
    private float damage;
    private bool isDead = false;
    private bool isBorn = false;
    private bool isAttacking = false;
    private bool canDestroy = false;

    protected override void Start()
    {
        base.Start();

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponentInChildren<BoxCollider2D>();

        playerTransform = GameManager.Instance.player.transform;
        startingPosition = this.transform.position;
        curHP.Value = GameConstant.Boss_HP;
        damage = GameConstant.Boss_Damage;
        pushRecoverySpeed = 0.9f;

        StartCoroutine(Born());
    }
    IEnumerator Born()
    {
        yield return new WaitForSeconds(1.5f);

        isBorn = true;
    }
    private void FixedUpdate()
    {
        if (isDead || !isBorn || isAttacking || GameManager.Instance.IsGameOvered()) return;

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

        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SendMessage("ReceiveDamage", damage);
        }
    }

    void ReceiveDamage(float dmg)
    {
        curHP.Value -= dmg;

        if (curHP.Value <= 0)
        {
            StartCoroutine(PerformDead());
        }
        else
        {
            anim.SetTrigger("Hit");
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

        GameManager.Instance.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PerformAttack());
        }
    }


    IEnumerator PerformAttack()
    {
        if (!isAttacking) yield return null;

        isAttacking = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.2f);
        attackDetecter.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        attackDetecter.gameObject.SetActive(false);

        isAttacking = false;
    }

    public ReactiveProperty<float> SubscribeHP()
    {
        return curHP;
    }
}
