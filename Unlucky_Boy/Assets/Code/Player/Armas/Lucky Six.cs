using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckySix : MonoBehaviour
{
    //Efecto de las balas
    //- 1 -> bala débil
    //- 2 -> bala normal
    //- 3 -> doble daño
    //- 4 -> bala explosiva
    //- 5 -> atraviesa enemigos
    //- 6 -> crítico masivo

    //Hacemos 2 listas que contengan las balas del revolver, una de las listas se va a utilizar para ir quitando y añadiendo componentes
    [SerializeField] List<int> ListatoRemove = new List<int> {};
    [SerializeField] List<int> ListaReal = new List<int> { 1,2,3,4,5,6};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ListatoRemove = ListaReal;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //Bala
        }
    }
    public void GablingBuller()
    {
        int balaAleatoria = Random.Range(0, ListatoRemove.Count);
    }

}
