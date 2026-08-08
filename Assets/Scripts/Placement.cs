using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;
using RangeAttribute = UnityEngine.RangeAttribute;

public class Placement : MonoBehaviour
{
    public CellSystem CellSystem;

    [SerializeField] private InputActionReference _placeAction;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _placeholderPlant;
    [SerializeField] private LayerMask _groundLayer;

    private GameObject _ghostPlant;
    private List<GameObject> _placedPlants = new List<GameObject>();
    private const float k_MaxRayDistance = 5f;
    
    private Vector3 RaycastToGrid()
    {
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(
            _camera.transform.position,
            _camera.transform.forward,
            out hitInfo,
            k_MaxRayDistance,
            _groundLayer
        );

        if (!hit)
        {
            Vector3 point = _camera.transform.position + _camera.transform.forward * k_MaxRayDistance;
            return CellSystem.PointToCell(point);
        }

        return CellSystem.PointToCell(hitInfo.point);
    }

    private void OnClick()
    {
        GameObject newPlant = Instantiate(_placeholderPlant, RaycastToGrid(), Quaternion.identity);
        _placedPlants.Add(newPlant);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _placeAction.action.Enable();
        _placeAction.action.performed += ctx => OnClick();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gridPosition = RaycastToGrid();

        // Create the ghost plant if it doesn't exist.
        Destroy(_ghostPlant);
        _ghostPlant = Instantiate(_placeholderPlant, gridPosition, Quaternion.identity);

        // Set the transparency of the ghost plant to 50%.
        Renderer ghostPlantRenderer = _ghostPlant.GetComponent<Renderer>();
        Color currentColor = ghostPlantRenderer.material.color;
        ghostPlantRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);

        _ghostPlant.SetActive(true);
        _ghostPlant.transform.position = gridPosition;
    }
}
