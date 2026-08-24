using System.Threading;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] Transform ShootOrigin;
    [SerializeField] float ShootForce = 15f;
    public float fireRate = 1.5f; // Cadencia: dispara cada 1.5 segundos
    private float nextTimeToShoot = 0f;
    public void Shoot()
    {
        if(Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + fireRate;
            // Le pedimos la bala al Pool centralizado pasando nuestra posición actual
            Bullet_EnemyPooling.Instance.SpawnBullet(ShootOrigin, ShootOrigin.rotation, ShootForce);
        }
    }
}
