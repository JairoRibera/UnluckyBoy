using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Enemy_Body : MonoBehaviour, IsShooteable, IsFire
{
    public ENEMY_PART part;
    public float damage;
    float finalDamage;
    private bool isBurning; 
    public void RecibeShoot(Vector3 hitpoint, float damage)
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
    public void Fire(Vector3 hitpoint, float damage, float time, float interval)
    {
        StartCoroutine(BurningCo(hitpoint, damage, time, interval));
    }
    private IEnumerator BurningCo(Vector3 hitpoint, float damage, float time, float interval)
    {

        Enemy enemyParent = GetComponentInParent<Enemy>();
        if (enemyParent == null) yield break;

        // Calculamos cuántos ticks y cuánto daño hace cada tick
        int ticks = Mathf.FloorToInt(time / interval);
        if (ticks <= 0) ticks = 1;

        float damagePerTick = damage / ticks; // Usa el parámetro damage recibido

        for (int i = 0; i < ticks; i++)
        {
            // 1. Aplicamos el daño a la vida del enemigo
            enemyParent.recibirdano(damagePerTick);

            // 3. Esperamos el intervalo antes del siguiente pulso
            yield return new WaitForSeconds(interval);
        }

    }
}
