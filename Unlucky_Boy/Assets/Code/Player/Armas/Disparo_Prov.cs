using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public class Disparo_Prov : MonoBehaviour
{
    //Aqui ponemos el scriptable object de la arma que se va a usar
    public armas arma;
    private Camera cam;
    public float range;
    public float damage;
    public float bullet;
    //Collider de la hitbox
    public GameObject hitbox;
    public float Temporizador;
    public float time = 0.5f;
    private void Start()
    {
        cam = Camera.main;
        Temporizador = time;
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
            //Igualamos el numero de balas, rango y daño del scriptable object al script
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
