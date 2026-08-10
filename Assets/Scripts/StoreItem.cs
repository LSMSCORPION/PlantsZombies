using UnityEngine;
 
public class StoreItem : MonoBehaviour, Iinteractable
{
    [SerializeField] private GameObject _functionalPlantPrefab;
    
    private GameObject _player; 
    private HoldSystem _holdSystem;
 
    private void Awake()
    {
        _player = GameObject.FindWithTag("Player"); 
        
        if (_player != null)
        {
            _holdSystem = _player.GetComponent<HoldSystem>();
        }
    }
 
    public void Interact()
    {
        if (_holdSystem != null)
        {
            _holdSystem.PickUp(_functionalPlantPrefab);
        }
    }
}