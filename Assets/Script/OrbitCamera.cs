using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField]
    float rotationSpeed = 5f;
    [SerializeField]
    float smoothFactor = 5f;

    Vector3 offsetCam;
    private void Start()
    {
        offsetCam = transform.position - Vector3.zero;
        transform.LookAt(Vector3.zero);
    }

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotationSpeed, Vector3.up);
            offsetCam = camTurnAngle * offsetCam;
            transform.position = Vector3.Slerp(transform.position, offsetCam, smoothFactor);
            transform.LookAt(Vector3.zero);
        }
    }
}
