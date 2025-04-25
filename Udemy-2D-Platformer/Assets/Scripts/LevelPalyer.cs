using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPalyer : MonoBehaviour
{
    public MapPoint currentPoint;
    public float moveSpeed = 10f;

    private bool canAcceptInput = true;
    public LSManager LSManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(
            transform.position,
            currentPoint.transform.position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.001f)
        {
            canAcceptInput = true;
        }

        if (canAcceptInput)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            if (h > 0.5f && currentPoint.right != null)
            {
                SetNextPoint(currentPoint.right);
            }
            else if (h < -0.5f && currentPoint.left != null)
            {
                SetNextPoint(currentPoint.left);
            }
            else if (v > 0.5f && currentPoint.up != null)
            {
                SetNextPoint(currentPoint.up);
            }
            else if (v < -0.5f && currentPoint.down != null)
            {
                SetNextPoint(currentPoint.down);
            }

            if (currentPoint.isLevel && currentPoint.levelToLoad != "" && !currentPoint.isLocked)
            {
                LSUIController.instance.ShowInfo(currentPoint);

                if (Input.GetButtonDown("Jump"))
                {
                    canAcceptInput = false;
                    LSManager.LoadLevel();
                }
            } else
            {
                LSUIController.instance.HideInfo();
            }
        }
    }

    public void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        // čim odaberemo novi čvor, blokiramo daljnji input dok ne stignemo na njega
        canAcceptInput = false;
    }
}
