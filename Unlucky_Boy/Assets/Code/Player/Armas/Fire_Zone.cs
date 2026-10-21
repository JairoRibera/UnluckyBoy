using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fire_Zone : MonoBehaviour
{
    public float damage = 20;
    public float intervalo = 1f;
    public float time = 4f;
    //El HasSet es similar a la List, pero con diferencias, no permite elementos repetidos, si intentas añadir algo que ya esta dentro de la lista lo ignora
    //Una busqueda más rapida de los elementos
    private HashSet<Enemy> enemigosGolpeados = new HashSet<Enemy>();
    private void OnTriggerEnter(Collider other)
    {
        // Buscamos si el objeto que entró tiene la interfaz IsFire
        if (other.TryGetComponent(out IsFire isfire))
        {
            // Obtenemos el script Enemy principal en el objeto padre
            Enemy enemigoPadre = other.GetComponentInParent<Enemy>();

            // Si el enemigo no ha sido procesado aún por esta zona
            if (enemigoPadre != null && !enemigosGolpeados.Contains(enemigoPadre))
            {
                enemigosGolpeados.Add(enemigoPadre);

                // Aplicamos ÚNICAMENTE la quemadura
                isfire.Fire(other.transform.position, damage, time, intervalo);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Cuando el enemigo sale de la zona, lo quitamos de la lista para que pueda volver a quemarse si reingresa
        Enemy enemigoPadre = other.GetComponentInParent<Enemy>();
        if (enemigoPadre != null && enemigosGolpeados.Contains(enemigoPadre))
        {
            enemigosGolpeados.Remove(enemigoPadre);
        }
    }
}
