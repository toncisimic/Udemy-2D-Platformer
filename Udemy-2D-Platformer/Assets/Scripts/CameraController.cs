using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform farBc, middleBc;
    public float parallaxFactor = 0.5f; 
    public float maxHeight = 8.0f;
    public float minHeight = -12.0f;
    public float abovePlayer = 2.0f;

    // Update is called once per frame
    void Update()
    {

        float middlePosY = Mathf.Clamp(target.position.y, minHeight, maxHeight);

        transform.position = new Vector3(target.position.x, middlePosY + abovePlayer, transform.position.z);

        farBc.position = new Vector3(target.position.x, farBc.position.y, farBc.position.z);

        middleBc.position = new Vector3(farBc.position.x * parallaxFactor, farBc.position.y, middleBc.position.z);
    }
}
