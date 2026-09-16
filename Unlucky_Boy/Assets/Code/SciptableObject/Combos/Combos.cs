using UnityEngine;
using System.Collections.Generic;
using System.Collections;
[System.Serializable]
public struct DatosCombo
{
    //esto es como una ficha técnica para crear un tipo de dato
    //Con esto el diccionario devuelve todos los datos de golpe
    public string nombreCombo;       // Ej: "Trío"
    public string secuenciaClave;    // Ej: "III" o "IDID"
    public int puntosBarra;        // Ej: 50
    public float multiplicadorDaño;  // Ej: 3
}
