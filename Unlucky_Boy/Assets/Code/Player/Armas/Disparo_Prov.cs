using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Disparo_Prov : MonoBehaviour
{
    public armas arma;
    private Camera cam;
    public float range;
    public float damage;
    public float bullet;
    private void Start()
    {
        cam = Camera.main;
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            
            Shoot();
        }
    }

    void Shoot()
    {
        if (arma.canShoot == true)
        {
            bullet = arma.bullet;
            range = arma.distance;
            damage = arma.damage;
            Debug.Log("Pium pium");
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Debug.Log("Golpeaste a: " + hit.transform.name);
            }

        }
        else Debug.Log("Es arma cuerpo a cuerpo");
    }
}
