using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckSize : MonoBehaviour
{
    public GameObject dadoPos;
    public static CheckSize Instance;
    private Vector3 posFinal;
    public LayerMask Cara;
    private void Awake()
    {
        Instance = this;
    }
    public void ComprobarNumero()
    {
        posFinal = new Vector3(dadoPos.transform.position.x, 4, dadoPos.transform.position.z);
        transform.position = posFinal;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down * 4 , out hit, Cara))
        {
            Debug.Log("Pium Pium");
            Debug.Log("Golpeé: " + hit.transform.name); // Muestra el nombre del objeto golpeado
            //Debug.Log("Punto de impacto: " + hit.point); // Punto exacto en 3D [3]
            //Debug.DrawRay(transform.position, dadoPos.transform.position, Color.green);
            hit.collider.GetComponent<ID_Size_Dado>();
            //Debug.Log("Cara numero" + hit.collider.ID_Size);
        }
    }
}
