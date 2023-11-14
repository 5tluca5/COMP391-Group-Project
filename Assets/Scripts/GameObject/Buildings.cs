using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = transform.position + new Vector3(0, 0, transform.position.y+2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
