using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFenceManager : MonoBehaviour
{
    public GameObject treePrefab;

    public Vector3 horizontalOffset;
    public Vector3 verticalOffset;

    public Transform leftTop;
    public Transform rightTop;
    public Transform leftBottom;
    public Transform rightBottom;

    public Transform topParent;
    public Transform bottomParent;
    public Transform leftParent;
    public Transform rightParent;


    Vector3 curSpot;
    // Start is called before the first frame update
    void Start()
    {
        curSpot = leftTop.localPosition;

        do
        {
            curSpot += horizontalOffset;

            var tree = Instantiate(treePrefab, topParent).GetComponent<Trees>();
            tree.transform.localPosition = curSpot;
        } while (curSpot.x < rightTop.localPosition.x && curSpot.y > rightTop.localPosition.y);

        curSpot = leftBottom.localPosition;

        do
        {
            curSpot += horizontalOffset;

            var tree = Instantiate(treePrefab, bottomParent).GetComponent<Trees>();
            tree.transform.localPosition = curSpot;
            //tree.GetComponent<Trees>().ResetZPosition();
        }
        while (curSpot.x < rightBottom.localPosition.x && curSpot.y > rightBottom.localPosition.y);

        curSpot = leftTop.localPosition;

        do
        {
            curSpot += verticalOffset;

            var tree = Instantiate(treePrefab, leftParent).GetComponent<Trees>();
            tree.transform.localPosition = curSpot;
        } while (curSpot.x > leftBottom.localPosition.x && curSpot.y > leftBottom.localPosition.y);


        curSpot = rightTop.localPosition;

        do
        {
            curSpot += verticalOffset;

            var tree = Instantiate(treePrefab, rightParent).GetComponent<Trees>();
            tree.transform.localPosition = curSpot;
        } while (curSpot.x > rightBottom.localPosition.x && curSpot.y > rightBottom.localPosition.y);
    }

}
