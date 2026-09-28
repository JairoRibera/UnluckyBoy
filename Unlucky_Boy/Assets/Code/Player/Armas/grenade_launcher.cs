using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class grenade_launcher : MonoBehaviour
{
    //Un struct (estructura) en C# es una estructura de datos personalizada que agrupa varias variables relacionadas bajo un mismo nombre.
    [System.Serializable]
    public struct DropItem
    {
        //Indicamos el nombre
        public string name;
        //Este es el valor al "peso" que va a tener cada objeto 
        [Range(0, 100)] public float weight;
    }
    public List<GameObject> listaBalas;
    private List<DropItem> balasTragaperras = new List<DropItem> 
    {
        new DropItem {name =  "Proyectil granada", weight = 30f},
        new DropItem {name =  "Proyectil incendiario", weight = 30f},
        new DropItem {name =  "Proyecil misil", weight = 30f},
        new DropItem {name =  "Jackpot", weight = 10f}
    };
    //public GameObject bullet;
    public float force;
    public float timeTo_Reload;
    public float timer_Reload;
    [SerializeField] int[] tragaperras = new int[] {1,2,3,4};
    [SerializeField]int[] Resultados;
    public DropItem GetRandomBullet()
    {
        //esto que se hace se le conoce como roulette wheel selection
        //Calcular el peso total
        float totalweight = 0f;
        foreach (var itemt in balasTragaperras)
        {
            //Suma todos los pesos de la lista ($40 + 40 + 20 = 100$). Esto define el tamaño total de la "ruleta".
            totalweight += itemt.weight;
        }
        //Obtenemos un valor random entre el 0 y el total weight (que es 100)
        float randomValue = Random.Range(0f, totalweight);

        foreach (var item in balasTragaperras)
        {
            //Al valor random, calculamos, si el valor random es meno al peso del objeto, 
            //Entonces eso quiere decir que el valor pertenece a ese objeto, 
            //De lo contrario, si no es menor, pues al valor random le restamos el peso del objeto y pasamos al siguiente objeto hasta que sea menor
            if (randomValue < item.weight)
            {
                Debug.Log(item.name);
                return item;
            }
            randomValue -= item.weight;
        }
        //Por si la lista está vacía o hay algun error entonces devolvemos el primer objeto de la lista
        return balasTragaperras[0]; 
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) GetRandomBullet();
    }
    public void Shoot()
    {

    }
    public void CambiarBala(int bala)
    {

    } 

}
