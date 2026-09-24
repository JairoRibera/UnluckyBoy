using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Barra_Knuckle : MonoBehaviour
{
    [SerializeField] private Poker_knuckle pokerRef;
    void Start()
    {
        pokerRef.Aumentar_Barra += actualizarBarra;
    }
    [SerializeField] private Slider barravida;
    public void iniciarBarra(float vidaMax)
    {
        barravida.maxValue = vidaMax;
        barravida.value = vidaMax;
    }
    public void actualizarBarra()
    {
        //barravida.value = puntos;
        Debug.Log("Puntos");
    }
}
