using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [Header("Patrol")]
    public float moveSpeed;
    public Transform leftPoint, rightPoint;
    [Range(0, 100)] public float chanceToDrop;

    private bool movingRight;
    private Rigidbody2D rb;

    public Transform Mouse;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);

            Mouse.localScale = new Vector3(1f, 1f, 1f);

            if (transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            Mouse.localScale = new Vector3(-1f, 1f, 1f);

            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            if (transform.position.x < leftPoint.position.x)
            {
                movingRight = true;
            }
        }
    }

}
