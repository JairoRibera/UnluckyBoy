using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet_PlayerPooling : MonoBehaviour
{

    // Variable temporal que usará CreateBullet en el instante de la creación
    private Bullet_Player prefabParaInstanciar;
    [SerializeField] private grenade_launcher playerWeapon;
    public BulletType currentType = BulletType.Rebote;
    private Bullet_Player prefabActual;
    public static Bullet_PlayerPooling Instance { get; private set; }
    [System.Serializable]
    public struct BulletConfiguration 
    { 
        public BulletType balaType;//Indicamos la bala que se va a necesitar
        public Bullet_Player bulletPrefab;//Prefab que se va a usar
    }
    [Header("Configuracion Balas")]
    [SerializeField] private List<BulletConfiguration> bulletConfiguration;
    [Header("Ajustes del Pool")]
    [SerializeField] private int defaultCapacity = 100;
    [SerializeField] private int maxCapacity = 400;
    // Diccionario para almacenar un ObjectPool distinto por cada BulletType
    public Dictionary<BulletType,ObjectPool<Bullet_Player>> poolsDictionary = new Dictionary<BulletType, ObjectPool<Bullet_Player>>();
    // Diccionario auxiliar para saber qué prefab le pertenece a qué tipo de bala
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
        InitalizePool();
    }
    private void OnEnable()
    {
        if (playerWeapon != null)
        {
            // Suscribimos el método al UnityAction del arma
            playerWeapon.ChangeBullet += HandleBulletTypeChanged;
        }
    }
    private void OnDisable()
    {
        if (playerWeapon != null)
        {
            // Desuscribimos para evitar fugas de memoria
            playerWeapon.ChangeBullet -= HandleBulletTypeChanged;
        }
    }
    // Método que se ejecuta cada vez que el UnityAction es invocado
    private void HandleBulletTypeChanged(BulletType newType)
    {
        currentType = newType;
        Debug.Log($"El Pool ha sido notificado: Tipo de bala cambiado a {newType}");
    }
    void InitalizePool()
    {
        foreach (var config in bulletConfiguration)
        {
            // Evitamos agregar tipos duplicados
            if (poolsDictionary.ContainsKey(config.balaType)) continue;
            // Comprobación de seguridad: avisa si se te olvidó asignar el prefab en el Inspector
            if (config.bulletPrefab == null)
            {
                Debug.LogError($"[POOL ERROR] Falta asignar el Prefab para el tipo '{config.balaType}' en el Inspector de Bullet_PlayerPooling.");
                continue;
            }
            Bullet_Player prefab = config.bulletPrefab;
            ObjectPool<Bullet_Player> newPool = new ObjectPool<Bullet_Player>(createFunc: () => CreateBullet(prefab), GetBullet, ReleaseBullet, DestroyBullet, false, defaultCapacity, maxCapacity);
            poolsDictionary.Add(config.balaType, newPool);
        }
    }
    //esta funcion se llama al crear el pool por tantas veces como objetos pueda tener
    //por ejemplo, si se especifica un tamaño de 20 para el pool, llama a la funcion 20 veces
    private Bullet_Player CreateBullet(Bullet_Player prefab)
    {
        //crear un nuevo proyectil
        Bullet_Player bullet = Instantiate(prefab);        // Le asignamos el pool correspondiente desde el diccionario si ya existe
        //desactivar el proyectil para que este oculto
        bullet.gameObject.SetActive(false);
        return bullet;
    }
    //Se llama cada vez que se coja un texto del pool
    private void GetBullet(Bullet_Player bullet)
    {
        bullet.gameObject.SetActive(true);
    }
    //Se llama cada vez que un texto vuelve al pool
    private void ReleaseBullet(Bullet_Player bullet)
    {
        bullet.ResetVelocity();
        //desactivar el objeto al devolverlo al pool
        bullet.gameObject.SetActive(false);
    }
    private void DestroyBullet(Bullet_Player bullet)
    {
        Destroy(bullet.gameObject);
    }
    // Método helper público para pedir balas desde cualquier enemigo
    public Bullet_Player SpawnBullet(BulletType type,Vector3 SpawnPosition, Quaternion rotation, float Speed, float ForceUp)
    {
        //El Pool consulta su diccionario para obtener el pool de esa bala
        if (!poolsDictionary.TryGetValue(type, out ObjectPool<Bullet_Player> pool))
        {
            Debug.LogError($"El tipo de bala '{type}' no está registrado en el Pool Manager.");
            return null;
        }
        //Extrae la bala del pool correspondiente
        Bullet_Player bullet = pool.Get();
        //Le asigna la referencia a su pool para que la bala sepa a dónde volver al destruirse
        bullet.pool = pool;
        //Posiciona y dispara
        bullet.transform.SetPositionAndRotation(SpawnPosition, rotation);
        bullet.Shoot(Speed, ForceUp);

        return bullet;
    }
}
