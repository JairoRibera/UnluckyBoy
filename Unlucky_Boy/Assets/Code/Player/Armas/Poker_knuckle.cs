using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Poker_knuckle : MonoBehaviour
{
    //De momento el combo funciona
    //Hay que hacer varios temporizadores, uno para cuando des un puñetazo, otro para saber cuando hay que verificar el combo, y otro para saber si hemos dejado de golpear para verificar el combo
    public enum PunchType { Right , Left };//Esto son los 2 tipos de golpes que puedes lanzar
    //private PunchType punch;
    [Header("Puñetazo")]
    public GameObject Hitbox;
    [SerializeField] private float time_Desactive = .1f;
    public bool isHitting = false;
    private PunchType ultimoGolpe;
    [Header ("Lista de combos")]//Aqui ponemos los diferentes combos que hemos creado
    // La lista actual de golpes que va dando el jugador
    public List<PunchType> PlayerPunch = new List<PunchType>();
    public ListaCombos listacombos;
    public HitBox_knuckle hitbox_Ref;
    [SerializeField]private float time = 10;
    public float timer;
    [SerializeField] private bool timerIsActive = false;
    private int puntos = 0;
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
    private void Start()
    {
        timer = time;
    }
    void Update()
    {
        Punch();
        IniciarTimer();
        FinalizarCombo();
    }

    public void FinalizarCombo()
    {
        //si la lista tiene 5 golpes la finalizamos
        if(PlayerPunch.Count >= 5)
        {
            CheckCombo();
        }
    }
    public void IniciarTimer()
    {
        if(timerIsActive == true)
        {
            timer -= Time.deltaTime;
            Debug.Log($"Tiempo restante de combo :{timer:F2}");
            if (timer <= 0)
            {
                Debug.Log("Tiempo Agotado, verificar combo");
                CheckCombo();
            }
        }
    }
    public void Punch()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            CheckCombo();
        }
        if (isHitting == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Golpe(PunchType.Left);
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                Golpe(PunchType.Right);
            }
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
            //puntos = puntos + comboDetected.puntosBarra;
            //Debug.Log($"El combo ha aumentado {comboDetected.puntosBarra} ahora la barra tiene {puntos} puntos" );
            //RellenarBarra?.Invoke();
            // Aquí sumas los puntos del combo a tu barra de Berserker
        }
        else
        {
            Debug.Log("No hay combo");
        }
        //Esto es para limpiar la lista
        PlayerPunch.Clear();
        timerIsActive = false;
        timer = time;
    }
    public string SecuenciaActual()
    {
        string resultado = "";
        foreach (var golpes in PlayerPunch)
        {
            resultado += (golpes == PunchType.Left) ? "L" : "R";
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
        Debug.Log(resultado);
        return resultado;
    }
    public void Golpe (PunchType punch)
    {
        isHitting = true;
        ultimoGolpe = punch;
        //Si ya hay un enemigo en la lista lo borra
        if(hitbox_Ref != null) hitbox_Ref.ClearList();
        Hitbox.SetActive(true);
        StartCoroutine(DesactiveHitboxCo(time_Desactive));
    }
    public void RegistrarGolpe()
    {
        PlayerPunch.Add(ultimoGolpe);
        timer = time;
        timerIsActive = true;
        Debug.Log("$¡Impacto confirmado! Golpe registrado: {ultimoGolpeLanzado}. Total en combo: {PlayerPunch.Count}");
    }
    private IEnumerator DesactiveHitboxCo(float time)
    {
        yield return new WaitForSeconds(time);
        Hitbox.SetActive(false);
        isHitting = false;
    }

}
