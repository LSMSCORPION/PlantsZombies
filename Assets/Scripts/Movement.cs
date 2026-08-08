using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpStrength = 5f;
    [SerializeField] private LayerMask groundLayer;

    private void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump pressed at: " + Time.time);

        bool hit = Physics.Raycast(
            rigidbody.transform.position,
            Vector3.down,
            1.1f,
            groundLayer
        );
        if (!hit) return;

        rigidbody.AddForce(new Vector3(0, _jumpStrength, 0), ForceMode.Impulse);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        move.action.Enable();
        jump.action.Enable();

        jump.action.started += Jump;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 input = move.action.ReadValue<Vector2>();
        if (input.magnitude == 0)
            return;

        Vector3 forwardVector = rigidbody.transform.forward;
        Vector3 rightVector = rigidbody.transform.right;
        Vector3 moveDirection = (forwardVector * input.y + rightVector * input.x) * _moveSpeed;
        rigidbody.AddForce(moveDirection);
    }
}
