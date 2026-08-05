using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public float range;
    private float damage = 10;
    public float bullet;
    public LayerMask Player;

    public void Shoot()
    {
            Debug.Log("Pium pium");
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, range, Player))
            {
                Debug.Log("Golpeaste al player");
            }
    }
}
