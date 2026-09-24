using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class grenade_launcher : MonoBehaviour
{
    //public GameObject bullet;
    public float force;
    public float timeTo_Reload;
    public float timer_Reload;
    [SerializeField] int[] tragaperras = new int[] {1,2,3,4};
    [SerializeField]int[] Resultados;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) { Tragaperras(); }
    }
    public void Tragaperras()
    {
        int bala = Random.Range(0, tragaperras.Length);
        if (bala == 1) Debug.Log("Proyectil granada");
        else if (bala == 2) Debug.Log("Proyectil incendiario");
        else if (bala == 3) Debug.Log("proyectil misil");
        else if (bala == 4) Debug.Log("Jackpot");
    }
}
