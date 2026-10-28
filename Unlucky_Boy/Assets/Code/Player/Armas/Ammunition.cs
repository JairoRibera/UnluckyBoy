using UnityEngine;
using UnityEngine.Events;

public class Ammunition : MonoBehaviour
{
    Ammunition_System Instance;
    public ArmType type;
    public float Ammo;
    public UnityAction<float, ArmType> Load_Ammo;
    private void OnTriggerEnter(Collider other)
    {
        // Comprobamos que el Singleton exista antes de invocarlo
        if (Ammunition_System.Instance != null)
        {
            Ammunition_System.Instance.AddAmmo(Ammo, type);
            Debug.Log($"Aumentando balas +{Ammo} del tipo {type}");

            // Opcional: Destruir el pick-up tras ser recolectado
            gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("No se encontró ninguna instancia de Ammunition_System en la escena.");
        }
    }
}
