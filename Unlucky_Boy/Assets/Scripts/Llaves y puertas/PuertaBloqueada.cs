using System.Collections.Generic;
using UnityEngine;

public class PuertaBloqueada : MonoBehaviour
{
    public List<ItemLlave> llavesNecesarias; // arrastras aquí todas las llaves requeridas
    public Animator animator;
    public AudioSource audioApertura;

    private bool abierta = false;

    private void OnTriggerEnter(Collider other)
    {
        if (abierta || !other.CompareTag("Player")) return;

        InventarioJugador inventario = other.GetComponent<InventarioJugador>();
        if (inventario != null && TieneTodasLasLlaves(inventario))
        {
            AbrirPuerta();
        }
        else
        {
            Debug.Log("Puerta cerrada. Faltan llaves: " + LlavesFaltantes(inventario));
        }
    }

    private bool TieneTodasLasLlaves(InventarioJugador inventario)
    {
        foreach (ItemLlave llave in llavesNecesarias)
        {
            if (!inventario.TieneLlave(llave))
                return false;
        }
        return true;
    }

    private string LlavesFaltantes(InventarioJugador inventario)
    {
        List<string> faltantes = new List<string>();
        foreach (ItemLlave llave in llavesNecesarias)
        {
            if (!inventario.TieneLlave(llave))
                faltantes.Add(llave.nombreLlave);
        }
        return string.Join(", ", faltantes);
    }

    private void AbrirPuerta()
    {
        abierta = true;

        if (audioApertura != null)
            audioApertura.Play();

        if (animator != null)
            animator.SetTrigger("Abrir");
        else
            gameObject.SetActive(false);
    }
}
