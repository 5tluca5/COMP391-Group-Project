using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trees : MonoBehaviour
{
    Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        originPos = this.transform.localPosition;
        ResetZPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SetPosition(Vector3 pos)
    {
        originPos = pos;
        this.transform.localPosition = pos;
        ResetZPosition();
    }

    public void ResetZPosition()
    {
        this.transform.localPosition = originPos + new Vector3(0, 0, transform.localPosition.y+1.5f);

    }
}
