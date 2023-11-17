using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    public int value { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        RandomValue();
    }

    void RandomValue()
    {
        var rand = Random.Range(0, 10);
        value = GameConstant.Drop_Resource_Max;

        foreach (var i in GameConstant.Drop_Resource_Ratio)
        {
            if (rand < i) break;

            value--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.CollectItem(this);
            Destroy(this.gameObject);
        }
    }
}
