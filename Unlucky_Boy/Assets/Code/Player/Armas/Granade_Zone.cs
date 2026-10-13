using UnityEngine;
using System.Collections;
public class Granade_Zone : MonoBehaviour
{
    public Vector2 RandomExplotion;
    public float Push = 5;
    //Daño
    public float damage;
    //Lista de enemigos
    //Metodo para activar 
    public float RandomNumber()
    {
        float Number = Random.Range(RandomExplotion.x, RandomExplotion.y);
        return Number;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ExplotioasngfoaidksbgnfÑASHJ");
        if (other.gameObject.TryGetComponent(out IsPusheable enemy))
        {
            Debug.Log("PUM");
            enemy.Knockback(transform.position, RandomNumber());
        }
        if(other.gameObject.TryGetComponent(out IsShooteable enemySho))
        {
            enemySho.RecibeShoot(other.transform.position, damage);
        }
    }
}
