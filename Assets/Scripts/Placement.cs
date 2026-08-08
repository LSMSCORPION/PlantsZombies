using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Placement : MonoBehaviour
{
    public int GridSize = 1;

    [SerializeField] private InputActionReference _placeAction;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _placeholderPlant;
    [SerializeField] private GameObject _ghostPlant;
    [SerializeField] private LayerMask _groundLayer;

    private List<GameObject> _placedPlants = new List<GameObject>();
    private const float k_MaxRayDistance = Mathf.Infinity;
    
    private Vector3? RaycastToGrid()
    {
        // Raycast.
        RaycastHit hitInfo;
        bool hit = Physics.Raycast(
            _camera.transform.position,
            _camera.transform.forward,
            out hitInfo,
            k_MaxRayDistance,
            _groundLayer
        );
        if (!hit) return null;

        // Snap the hit position to the grid.
        Vector3 gridPosition = new Vector3(
            Mathf.Floor(hitInfo.point.x / GridSize) * GridSize,
            1f,
            Mathf.Floor(hitInfo.point.z / GridSize) * GridSize
        );

        // Make sure plant hasn't already been placed here.
        foreach (GameObject plant in _placedPlants)
        {
            if (plant.transform.position == gridPosition)
                return null;
        }

        return gridPosition;
    }

    private void OnClick()
    {
        Vector3? gridPosition = RaycastToGrid();
        if (gridPosition == null) return;
        GameObject newPlant = Instantiate(_placeholderPlant, (Vector3)gridPosition, Quaternion.identity);
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
        Vector3? gridPosition = RaycastToGrid();
        if (gridPosition == null) return;
        _ghostPlant.transform.position = (Vector3)gridPosition;
    }
}
