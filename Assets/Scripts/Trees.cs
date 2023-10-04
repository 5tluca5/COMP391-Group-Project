using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = transform.position + new Vector3(0, 0, transform.position.y+1.5f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
