using UnityEngine;

[CreateAssetMenu(fileName = "NewBulletData", menuName = "Weapon/GranadeData")]
public class Granade_BulletData : ScriptableObject
{
    public float Speed;
    public float ForceUp;
    public float damage;
    public float lifeTime;
    public PhysicsMaterial physicMaterial;
    //public GameObject impactVFX;
}
