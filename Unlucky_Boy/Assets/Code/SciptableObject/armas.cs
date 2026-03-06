using UnityEngine;

[CreateAssetMenu(fileName = "armas", menuName = "Scriptable Objects/armas")]
public class armas : ScriptableObject
{
    public new string name;
    //Daño del arma
    public float damage;
    //Distancia de disparo
    public float distance;
    //booleana si es arma de fuero o cuerpo a cuerpo
    public bool canShoot;
    //Numeros de balas que tiene antes de recargar
    public float bullet;
    //Tiempo de recarga
}
