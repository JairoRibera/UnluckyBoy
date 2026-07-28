using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Disparo_Prov : MonoBehaviour
{
    //Aqui ponemos el scriptable object de la arma que se va a usar
    public armas arma;
    private Camera cam;
    public float range;
    private float damage =  10;
    public float bullet;
    public LayerMask Enemy;

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
        //Comrpobamos si el arma que se está es arma de fuego o cuerpo a cuerpo
        if (arma.canShoot == true)
        {
            Debug.Log("Pium pium");
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, Enemy))
            {
                Debug.Log("Golpeaste a: " + hit.transform.name);
                hit.collider.GetComponent<Enemy>().recibirdano(damage);
            }

        }
    }
}
