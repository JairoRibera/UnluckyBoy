using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GiroRuleta : MonoBehaviour
{
    //public Button botonGirar;
    public float duracionGiro = 4f;
    public int vueltasMin = 8;
    public int vueltasMax = 10;
    public int cantidadPremios = 6;
    private bool girando = false;
    void Start()
    {
        //botonGirar.onClick.AddListener(Girar);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Girar();
        }
    }

    void Girar()
    {
        if (!girando)
        {
            StartCoroutine(Giro());
        }
    }

    IEnumerator Giro()
    {
        girando = true;
        //Damos unas vueltas depensdiendo del valor minimo y maximo
        int vueltas = Random.Range(vueltasMin, vueltasMax);
        int premio = Random.Range(0, cantidadPremios);

        float anguloInicial = transform.eulerAngles.z;
        float anguloFinal = (360f / cantidadPremios) * premio;
        float anguloTotal = anguloInicial + (360f * vueltas) + anguloFinal;

        float tiempo = 0;

        while (tiempo < duracionGiro)
        {
            tiempo += Time.deltaTime;
            float progreso = tiempo / duracionGiro;

            float curva = Mathf.Lerp(anguloInicial, anguloTotal, Mathf.SmoothStep(0, 1, progreso));

            transform.eulerAngles = new Vector3(0, 0, -curva);
            yield return null;
        }

        girando = false;

        Debug.Log("Cambio de arma " + premio);
    }
}
