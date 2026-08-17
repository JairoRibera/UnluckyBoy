using UnityEngine;

[CreateAssetMenu(fileName = "NuevaLlave", menuName = "Inventario/Llave")]
public class ItemLlave : ScriptableObject
{
    public string nombreLlave = "Llave Roja";
    public Sprite icono; // opcional, para mostrar en UI
    [TextArea] public string descripcion;
}
