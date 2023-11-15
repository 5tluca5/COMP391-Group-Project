using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    public float speed = 300;
    
    Rigidbody2D body;
    Animator animator;
    bool isFlipped;
    float originScale = 1;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originScale = transform.localScale.x;
    }

    public void SetRotation(float angle, bool isFlipped)
    {
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
        transform.localScale = new Vector3(isFlipped ? -originScale : originScale, transform.localScale.y, transform.localScale.z);
        this.isFlipped = isFlipped;
    }

    public void Shot()
    {
        //Debug.Log("transform right" + transform.right)
        //body.AddForce(transform.right * speed * (isFlipped ? -1 : 1));
        body.velocity = transform.right * speed * (isFlipped ? -1 : 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided with" + collision.gameObject.name);
        //collision.gameObject.SendMessage("", )
        //animator.SetTrigger("Hit");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with" + collision.gameObject.name);
    }
}
