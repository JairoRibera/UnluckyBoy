using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class HitBox_knuckle : MonoBehaviour
{
    public float damage = 30f;
    private Poker_knuckle pokerKnuckle_Ref;
    //El HasSet es similar a la List, pero con diferencias, no permite elementos repetidos, si intentas añadir algo que ya esta dentro de la lista lo ignora
    //Una busqueda más rapida de los elementos
    public HashSet<Enemy> enemigosGolpeados = new HashSet<Enemy>();
    private void Start()
    {
        pokerKnuckle_Ref = GetComponentInParent<Poker_knuckle>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy_Body parteCuerpo))
        {
            // Obtenemos la referencia al enemigo padre principal
            Enemy enemigoPadre = parteCuerpo.GetComponentInParent<Enemy>();
            // Si el enemigo NO está en la lista, le aplicamos daño y lo registramos
            if (enemigoPadre != null && !enemigosGolpeados.Contains(enemigoPadre))
            {
                enemigosGolpeados.Add(enemigoPadre); // Lo marcamos como golpeado

                // Aplicamos el daño (pasándole si fue crítico o qué parte tocó si lo necesitas)
                parteCuerpo.RecibeShoot(transform.position, damage);
                if (pokerKnuckle_Ref != null) pokerKnuckle_Ref.RegistrarGolpe();
            }
        }
    }
    public void ClearList()
    {
        enemigosGolpeados.Clear();
    }
}
