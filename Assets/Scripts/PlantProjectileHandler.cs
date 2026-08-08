//PROJECTILE HANDLER
using UnityEngine;
 
public class PlantProjectileHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _projectilePrefab;
    public Transform prefab_FirePoint;

    public void ProjectileShoot(float speed, float damage)
    {
        GameObject projectile = Instantiate(
            _projectilePrefab,
            prefab_FirePoint.position,
            prefab_FirePoint.rotation);
 
        Rigidbody proj_Rigidbody = projectile.GetComponent<Rigidbody>();
 
        proj_Rigidbody.AddForce(
            prefab_FirePoint.forward
            * speed,
            ForceMode.Impulse);
    }
}
 