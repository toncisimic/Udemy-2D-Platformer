using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform farBc, middleBc;
    public float parallaxFactor = 0.5f; // Faktor brzine za middleBc
    public float maxHeight = 8.0f;
    public float minHeight = -12.0f;

    // Update is called once per frame
    void Update()
    {

        float middlePosY = Mathf.Clamp(target.position.y, minHeight, maxHeight);

        transform.position = new Vector3(target.position.x, middlePosY, transform.position.z);

        // Far background se pomera istom brzinom kao kamera
        farBc.position = new Vector3(target.position.x, farBc.position.y, farBc.position.z);


        // Middle background se pomera brže, zavisno od parallaxFactor
        middleBc.position = new Vector3(farBc.position.x * parallaxFactor, farBc.position.y, middleBc.position.z);
    }
}
