using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _sensitivity = 1f;
    [SerializeField] private InputActionReference _look;
    private float _pitch = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _look.action.Enable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        Vector2 mouseDelta = _look.action.ReadValue<Vector2>();
        if (mouseDelta.magnitude == 0)
            return;

        float xRotation = mouseDelta.x * _sensitivity;
        _playerTransform.Rotate(Vector3.up, xRotation);
        _pitch -= mouseDelta.y * _sensitivity;
        _pitch = Mathf.Clamp(_pitch, -90f, 90f);
        transform.localEulerAngles = new Vector3(_pitch, transform.localEulerAngles.y, 0f);
    }
}
