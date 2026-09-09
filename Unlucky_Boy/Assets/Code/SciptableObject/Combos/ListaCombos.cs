using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NuevoBancoCombos", menuName = "Combos/Banco de Combos")]

public class ListaCombos : ScriptableObject
{
    //Un scriptable object con todas las fichas de combos que hay, tanto sus nombres, ID, puntos etc
    public List<DatosCombo> listaDeCombos;
}
