using System.Threading;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float range;
    private float damage = 10;
    public float bullet;
    public LayerMask Player;
    public float fireRate = 1.5f; // Cadencia: dispara cada 1.5 segundos
    private float nextTimeToShoot = 0f;
    private void Update()
    {
        Shoot();
    }
    public void Shoot()
    {
        // Solo dispara si ha pasado el tiempo necesario desde el último tiro
        if (Time.time >= nextTimeToShoot)
        {
            nextTimeToShoot = Time.time + fireRate;
            Debug.Log("Pium pium");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, range, Player))
            {
                Debug.Log("Golpeaste al player");
                if (hit.collider.TryGetComponent(out Player_Life playerlife))
                {
                    playerlife.TakeDamage(damage);
                }

            }
        }

    }
}
