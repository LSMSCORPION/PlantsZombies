using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private InputActionReference move;
    [SerializeField] private InputActionReference jump;
    [SerializeField] private float _jumpStrength = 5f;
    [SerializeField] private float _maxSpeed = 3f;
    [SerializeField] private float _acceleration = 20f;
    [SerializeField] private float _deceleration = 10f;
    [SerializeField] private float rate;
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


    private void FixedUpdate()
    {
      SuperSmooth();
    }

    private void SuperSmooth()
    {
        Vector2 input = move.action.ReadValue<Vector2>();

        Vector3 forward = rigidbody.transform.forward;
        Vector3 right = rigidbody.transform.right;

        Vector3 targetDirection = (forward * input.y + right * input.x).normalized; 
        Vector3 targetVelocity = targetDirection * _maxSpeed;

        Vector3 currentVelocity = rigidbody.linearVelocity;
        Vector3 currentHorizontal = new Vector3(currentVelocity.x, 0f, currentVelocity.z);

        if (input.magnitude > 0)
        {
            rate = _acceleration;
        }
        else
        {
           rate = _deceleration; 
        }

        Vector3 newHorizontal = Vector3.MoveTowards(
            currentHorizontal, 
            targetVelocity, 
            rate*Time.fixedDeltaTime);

        rigidbody.linearVelocity = new Vector3(newHorizontal.x, currentVelocity.y, newHorizontal.z);
    }

}
