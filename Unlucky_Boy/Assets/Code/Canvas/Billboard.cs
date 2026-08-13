using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Billboard : MonoBehaviour
{
    //Posicion de la camara
    private Transform mainCamera;
    private void Awake()
    {
        //Obtenemos la posicion de la camara principal
        mainCamera = Camera.main.gameObject.transform;
    }
    private void Update()
    {
        //Hacemos que mire siempre a la cámara, girando en el eje Y
        transform.LookAt(mainCamera);
        //transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
