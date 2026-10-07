using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
//Creamos una interfaz para que cada parte del cuerpo pueda recibir el disparo
public interface IsShooteable
{
    //Pasamos el punto de impacto
    public void RecibeShoot(Vector3 hitpoint, float damage);
}
public interface IsPusheable
{
    public void RecibePush(float distance);
}
public class Enemy : MonoBehaviour
{
    public float life = 100;
    [SerializeField]private Rigidbody rb;
    public void EmpujeEnemy(float distance)
    {
        
        Vector3 dirEmpuje = -transform.forward * distance;
        rb.AddForce(dirEmpuje, ForceMode.Impulse);
    }
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
