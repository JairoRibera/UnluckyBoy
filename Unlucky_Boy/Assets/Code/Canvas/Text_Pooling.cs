using Unity.VisualScripting;
using UnityEngine;
//Para poder usar el sistema de Pooling de Unity
using UnityEngine.Pool;
public class Text_Pooling : MonoBehaviour
{
    public static Text_Pooling Instance { get; private set; }
    //Prefab que se va a usar
    [SerializeField] private Damage_Text textPrefab;
    //Capacidad minima del pool que tiene por defecto
    [SerializeField] private int defaultCapacity = 20;
    //Capacidad maxima xD
    [SerializeField] private int maxCapacity = 40;
    // El pool del texto
    public ObjectPool<Damage_Text> Text_Pool;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // Si no hay instancia asignada, me asigno a mí mismo
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // Si hay un duplicado, lo destruyo
            Destroy(gameObject);
            return;
        }
        //Creamos un pool de objetos tipo Damage_Text y utiliza estas funciones que he escrito para saber qué hacer cuando necesites crear uno nuevo, activarlo o guardarlo
        Text_Pool = new ObjectPool<Damage_Text>(CreateText, GetText, ReleaseText, null, true, defaultCapacity, maxCapacity);
    }
    //esta funcion se llama al crear el pool por tantas veces como objetos pueda tener
    //por ejemplo, si se especifica un tamaño de 20 para el pool, llama a la funcion 20 veces
    private Damage_Text CreateText()
    {
        //crear un nuevo proyectil
        Damage_Text damage_Text = Instantiate(textPrefab);
        //asignar el pool del proyectil
        damage_Text.pool = Text_Pool;
        //desactivar el proyectil para que este oculto
        damage_Text.gameObject.SetActive(false);
        return damage_Text;
    }
    //Se llama cada vez que se coja un texto del pool
    private void GetText(Damage_Text damage_text)
    {
        //al sacar un objeto del pool, lo principal es activarlo
        damage_text.gameObject.SetActive(true);
        //movel el proyectil al punto de origen de disparo
        //damage_text.transform.position = la pos del enemigo
        // hacer la animacion
    }
    //Se llama cada vez que un texto vuelve al pool
    private void ReleaseText(Damage_Text damage_Text)
    {
        //desactivar el objeto al devolverlo al pool
        damage_Text.gameObject.SetActive(false);
    }
    //Funcion para hacer aparecer el objeto
    public void ShowDamage(int damage, Vector3 position)
    {
        //Pedimos un objeto al Pool (ejecuta GetText -> SetActive(true))
        Damage_Text text = Text_Pool.Get();

        //Le pasamos los datos y lo posicionamos con su SetUp
        text.SetUp(damage, position);
    }
}
