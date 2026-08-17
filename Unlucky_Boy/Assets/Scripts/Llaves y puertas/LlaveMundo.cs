using UnityEngine;

public class LlaveMundo : MonoBehaviour
{
    public ItemLlave datosLlave; // arrastras aquí el ScriptableObject correspondiente

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventarioJugador inventario = other.GetComponent<InventarioJugador>();
            if (inventario != null)
            {
                inventario.AgregarLlave(datosLlave);
                Debug.Log("Recogida: " + datosLlave.nombreLlave);
                Destroy(gameObject);
            }
        }
    }
}
