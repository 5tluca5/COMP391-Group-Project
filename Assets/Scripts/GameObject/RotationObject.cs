using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationObject : MonoBehaviour
{
    protected Transform target;
    protected float angle;
    protected bool isFlipped = false;

    public float rotationSpeed = 10f;
    public bool flipEnabled = true;

    private float curScaleX = 1.5f;

    protected virtual void Start()
    {
        if (target == null)
            target = transform;

        curScaleX = transform.localScale.x;
    }
    void Update()
    {
        // Get the mouse position on the screen
        Vector3 mouseScreenPos = Input.mousePosition;

        // Convert the mouse position to world position
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);

        // Calculate the direction from the object to the mouse position
        Vector3 direction = mouseWorldPos - target.position;

        // Calculate the angle between the object's forward vector and the direction
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Check if rotation angle is over 180 degrees
        if (flipEnabled)
        {
            if (Mathf.Abs(angle) > 90f && !isFlipped)
            {
                FlipObject();
                isFlipped = true;
            }
            else if (Mathf.Abs(angle) <= 90f && isFlipped)
            {
                FlipObject();
                isFlipped = false;
            }

            if(isFlipped)
            {
                angle += 180;
            }
        }

        // Rotate the object around the z-axis
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

    }

    void FlipObject()
    {
        // Flip the object horizontally
        transform.localScale = new Vector3(curScaleX, transform.localScale.y, transform.localScale.z);
        transform.DOScaleX(-curScaleX, 0.2f);

        curScaleX = -curScaleX;
    }
}