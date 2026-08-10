using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{

    
    [SerializeField] private InputActionReference _interactAction;
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private float _maxInteractDistance = 5f;

    public RaycastHit? CurrentHit {get; private set;} //Allows other scripts to read without breaking data
    public Iinteractable currentInteractable {get; private set;}
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _interactAction.action.Enable();
        _interactAction.action.performed += ctx => TryInteract();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLookTarget();
    }

    private void UpdateLookTarget()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(_camera.transform.position,
        _camera.transform.forward, 
        out hitInfo, 
        _maxInteractDistance, 
        _interactableLayer);

        if (!hit)
        {
            CurrentHit = null;
            currentInteractable = null;
            return;
        }

        CurrentHit = hitInfo;
        currentInteractable = hitInfo.collider.GetComponent<Iinteractable>();
    }

    private void TryInteract()
    {
        currentInteractable?.Interact();
    }


}
