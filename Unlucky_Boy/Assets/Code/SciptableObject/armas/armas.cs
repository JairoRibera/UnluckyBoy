using UnityEngine;

[CreateAssetMenu(fileName = "armas", menuName = "Scriptable Objects/armas")]
public class armas : ScriptableObject
{
    public bool canShoot = true;
    public float range = 100f;

    [Header("Munición y Tiempos")]
    public int maxAmmo = 15;
    public float fireRate = 0.5f; // Límite de disparos por segundo (0.5s = 2 clicks por segundo)
    public float reloadTime = 1f;
    public float overheatTime = 2f; // Penalización por pasarse de 21
}
