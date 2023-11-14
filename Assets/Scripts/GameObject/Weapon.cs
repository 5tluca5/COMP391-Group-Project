using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : RotationObject
{
    public Transform owener;
    public Transform firePos;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    protected override void Start()
    {
        if (!owener)
            owener = GameObject.FindGameObjectWithTag("Player").transform;

        target = owener;

        base.Start();
    }

    public void SetPositionX(float posX)
    {
        transform.localPosition = new Vector3(posX, transform.localPosition.y);
    }

    public void SetPositionZ(float posZ)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posZ);
    }

    public void Fire()
    {
        var bullet = Instantiate(bulletPrefab).GetComponent<Bullet>();
        bullet.transform.position = firePos.position;
        bullet.SetRotation(angle, isFlipped);
        bullet.Shot();
    }
}
