using UnityEngine;
 
//DATA CONTAINER
[System.Serializable]
public class PlantData
{
    //Plant Stats
    public string plantName;
    public float health = 100f;
    public float fireRate = 1.0f;
 
   //Projectile Stats
    public float proj_Speed = 10f;
    public float proj_Damage = 20f;
}