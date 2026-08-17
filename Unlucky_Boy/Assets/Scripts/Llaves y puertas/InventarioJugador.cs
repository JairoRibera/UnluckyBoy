using System.Collections.Generic;
using UnityEngine;

public class InventarioJugador : MonoBehaviour
{
    private List<ItemLlave> llaves = new List<ItemLlave>();

    public void AgregarLlave(ItemLlave llave)
    {
        if (!llaves.Contains(llave))
            llaves.Add(llave);
    }

    public bool TieneLlave(ItemLlave llave)
    {
        return llaves.Contains(llave);
    }

}
