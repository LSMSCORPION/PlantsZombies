using UnityEngine;
 
//MAIN
public class PlantClass : MonoBehaviour
{
    [SerializeField]
    private PlantData _plantData;
    private PlantProjectileHandler _projectileHandler;
 
    public float fireCooldown;
 
    private void Awake()
    {
        _projectileHandler = GetComponent<PlantProjectileHandler>();
    }
 
    void Update()
    {
        FireCooldown();
    }
 
    private void FireCooldown()
    {
        fireCooldown -= Time.deltaTime;
 
        if (fireCooldown <= 0)
        {
            Fire();
            fireCooldown = 1f / _plantData.fireRate;
        }
    }
 
    private void Fire()
    {
        Debug.Log(_plantData.plantName + " has fired");
        _projectileHandler.ProjectileShoot(_plantData.proj_Speed, _plantData.proj_Damage);
    }
}