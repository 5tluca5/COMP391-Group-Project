using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    public float pushRecoverySpeed = 0.2f;

    private Vector3 moveDelta;
    private BoxCollider2D boxCollider;

    protected Vector3 pushDirection;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1f;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //bool canMoveHorizontally = true;
        //bool canMoveVertically = true;


        // Change moving direction
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Add push vector, if any
        moveDelta += pushDirection;

        // Reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Debug.Log("moveDelta" + moveDelta);
        //Debug.Log("PushDir" + pushDirection);

        transform.Translate(moveDelta * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        // Check what player are colliding
        //boxCollider.OverlapCollider(new ContactFilter2D(), hits);

        //for (int i = 0; i < hits.Length; i++)
        //{
        //    if (hits[i] == null) continue;

        //    // do sth
        //    // ...

        //    hits[i] = null;
        //}

        // Check if there're any blocking object [left / right]
        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor", "Stairs"));

        //if (hit.collider == null && canMoveHorizontally)
        //{
        //    // Can move!
        //    transform.Translate(new Vector3(moveDelta.x * Time.deltaTime, 0, 0));
        //    getBlockedHorizontally = false;
        //}
        //else
        //{
        //    getBlockedHorizontally = true;
        //    blockingObject = hit.collider.name;

        //}

        // Check if there're any blocking object [top / bottom]
        //hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Blocking", "Actor"));

        //if (hit.collider == null && canMoveVertically)
        //{
        //    // Can move!
        //    transform.Translate(new Vector3(0, moveDelta.y * Time.deltaTime, 0));
        //    getBlockedVertically = false;
        //}
        //else
        //{
        //    getBlockedVertically = true;
        //    blockingObject = hit.collider.name;
        //}
    }
}
