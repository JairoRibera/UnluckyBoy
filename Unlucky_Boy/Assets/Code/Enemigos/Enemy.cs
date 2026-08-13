using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Creamos una interfaz para que cada parte del cuerpo pueda recibir el disparo
public interface IsShooteable
{
    //Pasamos el punto de impacto
    public void RecibeShoot(Vector3 hitpoint);
}
public class Enemy : MonoBehaviour
{
    public float life = 100;

    public void recibirdano(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Muelto();
        }
    }
    public void Muelto()
    {
        gameObject.SetActive(false);
    }

}
