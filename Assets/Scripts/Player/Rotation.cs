using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField]
    Transform cameraTransform;

    float mouseSensitivity;

    public float MouseSensitivity
    {
        get => mouseSensitivity;
        set => mouseSensitivity = value;
    }

    float verticalRotation = 0f;

    void Start()
    {
        mouseSensitivity = IOManager.ReadSettings().MouseSensitivity;
        Cursor.lockState = CursorLockMode.Locked;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        verticalRotation -= y;
        verticalRotation = Mathf.Clamp(verticalRotation, -90, 70);

        transform.Rotate(Vector3.up * x);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
}
