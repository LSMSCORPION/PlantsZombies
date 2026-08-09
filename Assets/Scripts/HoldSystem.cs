using UnityEngine;
 
public class HoldSystem : MonoBehaviour
{
    public GameObject HeldPlantPrefab { get; private set; }
    public bool IsHolding => HeldPlantPrefab != null;
 
    public void PickUp(GameObject plantPrefab)
    {
        HeldPlantPrefab = plantPrefab;
        Debug.Log("Picked up: " + plantPrefab.name);
    }
 
    public void Clear()
    {
        HeldPlantPrefab = null;
    }
}
 