using UnityEngine;
using UnityEngine.UI;

public class Barra_Vida : MonoBehaviour
{
    [SerializeField] private Slider barravida;
    public void iniciarBarraVida(float vidaMax)
    {
        barravida.maxValue = vidaMax;
        barravida.value = vidaMax;
    }
    public void actualizarBarraVida(float vidaActual)
    {
        barravida.value = vidaActual;
    }
}
