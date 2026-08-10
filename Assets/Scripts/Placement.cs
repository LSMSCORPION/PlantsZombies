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
    private GameObject _ghostSourcePrefab;
    private GameObject _player;
    private HoldSystem _holdSystem;

    private List<GameObject> _placedPlants = new List<GameObject>();
    private const float k_MaxRayDistance = 5f;

     private void Awake()
     {
        FindPlayer();
     }

     private void FindPlayer()
    {
        _player = GameObject.FindWithTag("Player");
        Debug.Assert(_player != null, "Player doesn't exist.");
        
        _holdSystem = _player.GetComponent<HoldSystem>();
    }

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
        Debug.Assert(_holdSystem != null, "Hold system is null.");
        if (!_holdSystem.IsHolding) return;

        GameObject newPlant = Instantiate(_holdSystem.HeldPlantPrefab, RaycastToGrid(), Quaternion.identity);
        _placedPlants.Add(newPlant);

        _holdSystem.Clear();
        DestroyGhost();
    }

    private void DestroyGhost()
    {
        if (_ghostPlant != null)
        Destroy(_ghostPlant);

        _ghostPlant = null;
        _ghostSourcePrefab = null;
    }

    private void DisableGhostBehaviors(GameObject _ghost)
    {
        MonoBehaviour[] behaviors = _ghost.GetComponents<MonoBehaviour>();

        foreach (var behavior in behaviors)
        {
            behavior.enabled = false;
        }
        foreach (var collider in _ghost.GetComponents<Collider>())
        {
            collider.enabled = false;
        }
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

        if (!_holdSystem.IsHolding)
        {
            DestroyGhost();
            return;
        }


        if (_ghostSourcePrefab != _holdSystem.HeldPlantPrefab)
        {
            DestroyGhost();
 
            _ghostPlant = Instantiate(_holdSystem.HeldPlantPrefab);
            _ghostSourcePrefab = _holdSystem.HeldPlantPrefab;

            DisableGhostBehaviors(_ghostPlant);
 
            Renderer ghostPlantRenderer = _ghostPlant.GetComponent<Renderer>();
            Color currentColor = ghostPlantRenderer.material.color;
            ghostPlantRenderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.5f);
        }
 
        _ghostPlant.transform.position = RaycastToGrid();
    }
}
