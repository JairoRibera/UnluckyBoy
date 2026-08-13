using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy_Body : MonoBehaviour, IsShooteable
{
    public ENEMY_PART part;
    public float damage;
    float finalDamage;
    public void RecibeShoot(Vector3 hitpoint)
    {
        // Si impacta en la cabeza, aplicamos multiplicador crítico (ej. x2)
        if (part == ENEMY_PART.Head)
        {
            finalDamage = damage * 2f;
            
        }
        if(part == ENEMY_PART.Body)
        {
            finalDamage = damage;
        }
            Enemy enemyParent = GetComponentInParent<Enemy>();
        if (enemyParent != null) 
        {
            enemyParent.recibirdano(finalDamage);
        }
        //Mostrar el texto flotante en la posición del impacto
        if (Text_Pooling.Instance != null)
        {
            // Convertimos float a int para la UI
            Text_Pooling.Instance.ShowDamage((int)finalDamage, hitpoint);
        }

    }
}
