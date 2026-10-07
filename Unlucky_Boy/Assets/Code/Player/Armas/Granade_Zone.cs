using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class Granade_Zone : MonoBehaviour
{
    public float Push = 5;
    //Daño
    public float damage;
    //Lista de enemigos
    //Metodo para activar 
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out IsShooteable enemy))
        {
            Debug.Log("PUM");
            enemy.RecibeShoot(other.transform.position, damage);
        }
    }
}
