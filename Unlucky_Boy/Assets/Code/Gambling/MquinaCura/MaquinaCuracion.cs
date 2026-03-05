using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class MaquinaCuracion : MonoBehaviour
{
    //Array con los premios
    [SerializeField] int[] SimbolosMaquina = new int[] {1,2,3,4,5,6,7,8,9,10};
    //Array con los resultados
    [SerializeField] int[] Resultados;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Giro();
        }
    }

    void Giro()
    {
        //Comprobamos si el tamaño del array es 3, si es asi borramos los componentes del array
        if (Resultados.Length == 3)
        {
            System.Array.Clear(Resultados, 0, Resultados.Length);
        }
        //Calculamos los numeros
        int a = Random.Range(0, SimbolosMaquina.Length);
        int b = Random.Range(0, SimbolosMaquina.Length);
        int c = Random.Range(0, SimbolosMaquina.Length);
        //Asignamos los valores a los resultados
        Resultados[0] = a;
        Resultados[1] = b;
        Resultados[2] = c;

        //Comparamos los resultados
        if(a == b && b == c)
        {
            //Si todos los resultados coinciden entonces damos la mayor recompensa
            Debug.Log("Cura total");
        }
        else if (a == b || b == c || a == c)
        {
            //Si solo coinciden 2 de 3 resultados entonces damos una recompensa menor
            Debug.Log("Media Cura");
        }
        else
        { 
            //Si no coincide nada entonces damos migajas
            Debug.Log("Cura Parcial");
        }
    }
}
