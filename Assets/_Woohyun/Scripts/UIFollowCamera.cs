using UnityEngine;

public class UIFollowCamera : MonoBehaviour
{
    public Transform cameraTransform;  // Reference to the camera's transform
    public Vector3 offset = new Vector3(0, 0, 2);  // Offset from the camera

    void Update()
    {
        // Make the UI follow the camera position with an offset
        transform.position = cameraTransform.position + cameraTransform.forward * offset.z + cameraTransform.right * offset.x + cameraTransform.up * offset.y;

        // Make the UI face the camera
        transform.LookAt(cameraTransform.position);
        transform.Rotate(0, 180, 0);  // Adjust rotation if needed
    }
}
