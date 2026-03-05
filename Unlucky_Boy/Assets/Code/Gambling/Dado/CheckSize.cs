using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckSize : MonoBehaviour
{
    public GameObject dadoPos;
    public static CheckSize Instance;
    private Vector3 posFinal;
    public LayerMask Cara;
    private ID_Size_Dado dadoId;
    public int cara;
    private Rigidbody rb;
    private void Awake()
    {
        Instance = this;
    }
    public void ComprobarNumero()
    {
        //Indicamos que la posicion final es la misma que la del dado en x z en la y le ponemos 4
        posFinal = new Vector3(dadoPos.transform.position.x, 4, dadoPos.transform.position.z);
        //Igualamos la posicion del objeto a la posicion final
        transform.position = posFinal;
        RaycastHit hit;
        //Lanzamos un raycast desde la posición del objeto hacia abajo, detectando la layer Cara
        if (Physics.Raycast(transform.position, Vector3.down * 4 , out hit, Cara))
        {
            rb = dadoPos.GetComponent<Rigidbody>();
            //Obtenemos el componente ID_Size_Dado para obtener el número de la cara
            dadoId = hit.collider.GetComponent<ID_Size_Dado>();
            if (dadoId != null)
            {
                //Si el valor no es null entonces igualamos el int al numero de la cara del dado
                cara = dadoId.ID_Size;
                Debug.Log("La cara numero " + cara);
            }
            else
            {
                //Si el dado se queda quiero sobre una arista, le añadimos una fuerza para evitar ese problema
                rb.AddForce(Vector3.right * 0.001f, ForceMode.Impulse);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * 4);
    }
}
