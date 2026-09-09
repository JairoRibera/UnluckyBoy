using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poker_knuckle : MonoBehaviour
{
    //De momento el combo funciona
    //Hay que hacer varios temporizadores, uno para cuando des un puñetazo, otro para saber cuando hay que verificar el combo, y otro para saber si hemos dejado de golpear para verificar el combo
    public enum PunchType { Right , Left };//Esto son los 2 tipos de golpes que puedes lanzar
    private PunchType punch;
    [Header("Puñetazo")]
    public GameObject Hitbox;
    private float time = .25f;
    public float timer;
    public bool isHitting = false;

    [Header ("Lista de combos")]//Aqui ponemos los diferentes combos que hemos creado
    // La lista actual de golpes que va dando el jugador
    public List<PunchType> PlayerPunch = new List<PunchType>();
    public ListaCombos listacombos;

    // El diccionario interno (Clave: "IDID" -> Valor: Datos del combo)
    private Dictionary<string, DatosCombo> diccionarioCombos = new Dictionary<string, DatosCombo>();
    private void Awake()
    {
        // Pasamos los datos de la Lista al Diccionario al iniciar el juego
        foreach (var combo in listacombos.listaDeCombos)
        {
            //Si dentro del diccionario no hay alguna secuencia, la añadimos
            if (!diccionarioCombos.ContainsKey(combo.secuenciaClave))
            {
                diccionarioCombos.Add(combo.secuenciaClave, combo);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CheckCombo();
        }
        if (Input.GetButtonDown("Fire1") /*&& isHitting == false*/)
        {
            //Debug.Log("Golpeizquierda");
            isHitting = true;
            GolpeIzquierda(punch);
        }
        if (Input.GetButtonDown("Fire2") /*&& isHitting == false*/)
        {
            //Debug.Log("Golpederecha");
            isHitting = true;
            GolpeDerecha(punch);
        }
    }
    public void CheckCombo()
    {
        if (PlayerPunch.Count == 0) return;
        string ID_Final = SecuenciaActual();
        //Si en el diccionario hay alguna secuencia que sea igual a la que ha hecho el jugador saca la info
        if (diccionarioCombos.TryGetValue(ID_Final, out DatosCombo comboDetected))
        {
            // ¡COMBO ENCONTRADO! Aplicamos sus ventajas modificables
            Debug.Log($"¡Combo ejecutado: {comboDetected.nombreCombo}! Añade {comboDetected.puntosBarra} puntos a la barra.");
            // Aquí sumas los puntos del combo a tu barra de Berserker
        }
        else
        {
            Debug.Log("No hay combo");
        }
        //Esto es para limpiar la lista
        PlayerPunch.Clear();
    }
    public string SecuenciaActual()
    {
        string resultado = "";
        foreach (var golpes in PlayerPunch)
        {
            resultado += (golpes == PunchType.Left) ? "L" : "D";
        }
        //El signo ?: Significa "¿Se cumple la condición?".
        //Izquierda del : ("I"): Es lo que se añade a resultado si el golpe sí fue Izquierda.
        //Derecha del : ("D"): Es lo que se añade a resultado si el golpe no fue Izquierda (es decir, fue Derecha).
        //Esa línea hace exactamente esto
        //if (golpes == PunchType.Left)
        //{
        //    resultado = resultado + "L";
        //}
        //else
        //{
        //    resultado = resultado + "R";
        //}
        return resultado;
    }
    public void GolpeDerecha(PunchType punch)
    {
        punch = PunchType.Right;
        PlayerPunch.Add(punch);
    }
    public void GolpeIzquierda(PunchType punch)
    {
        punch = PunchType.Left;
        PlayerPunch.Add(punch);
    }


}
