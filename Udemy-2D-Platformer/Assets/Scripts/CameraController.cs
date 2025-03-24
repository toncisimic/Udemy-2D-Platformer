using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform farBc, middleBc;
    public float parallaxFactor = 0.5f; 
    public float maxHeight = 8.0f;
    public float minHeight = -12.0f;
    public float abovePlayer = 2.0f;
    public bool follow;

    public static CameraController instance;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float middlePosY = Mathf.Clamp(target.position.y, minHeight, maxHeight);

        if (!follow)
        {
            transform.position = new Vector3(target.position.x, middlePosY + abovePlayer, transform.position.z);

            farBc.position = new Vector3(target.position.x, farBc.position.y, farBc.position.z);

            middleBc.position = new Vector3(farBc.position.x * parallaxFactor, farBc.position.y, middleBc.position.z);
        } else
        {
            // Na kraju levela, pomakni kameru prema lijevoj strani (prije sredine)
            Vector3 targetPosition = new Vector3(target.position.x + 1.5f, middlePosY + abovePlayer, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.02f);

            // Parallax efekt
            farBc.position = new Vector3(target.position.x, farBc.position.y, farBc.position.z);
            middleBc.position = new Vector3(farBc.position.x * parallaxFactor, farBc.position.y, middleBc.position.z);
        }
    }
}
