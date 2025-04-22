using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPalyer : MonoBehaviour
{
    public MapPoint currentPoint;
    public float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        if (Input.GetAxisRaw("Horizontal") > .5f)
        {
           
            if (currentPoint.right != null)
            {
                Debug.Log("DOSAO");
                SetNextPoint(currentPoint.right);
            }
        }

        if (Input.GetAxisRaw("Horizontal") < -.5f)
        {
            if (currentPoint.left != null)
            {
                SetNextPoint(currentPoint.left);
            }
        }

        if (Input.GetAxisRaw("Vertical") > .5f)
        {
            if (currentPoint.up != null)
            {
                SetNextPoint(currentPoint.up);
            }
        }

        if (Input.GetAxisRaw("Vertical") < -.5f)
        {
            if (currentPoint.down != null)
            {
                SetNextPoint(currentPoint.down);
            }
        }
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
    }
}
