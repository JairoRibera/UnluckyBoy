using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Uno : MonoBehaviour
{
    public static Bullet_PlayerPooling Instance { get; private set; }

    [System.Serializable]
    public struct BulletConfiguration
    {
        // Indicamos la bala que se va a necesitar
        public BulletType balaType;

        // Prefab que se va a usar
        public Bullet_Player bulletPrefab;
    }

    [Header("Configuracion Balas")]
    [SerializeField] private List<BulletConfiguration> bulletConfiguration;

    [Header("Ajustes del Pool")]
    [SerializeField] private int defaultCapacity = 100;
    [SerializeField] private int maxCapacity = 400;

    // Diccionario para almacenar un ObjectPool distinto por cada BulletType
    public Dictionary<BulletType, ObjectPool<Bullet_Player>> poolsDictionary = new Dictionary<BulletType, ObjectPool<Bullet_Player>>();

    private void Awake()
    {
        if (Instance == null)
        {
            //Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitializePool();
    }

    void InitializePool()
    {
        foreach (var config in bulletConfiguration)
        {
            // Evitamos agregar tipos duplicados
            if (poolsDictionary.ContainsKey(config.balaType)) continue;

            // Variables locales para evitar problemas de captura en closures
            Bullet_Player prefab = config.bulletPrefab;
            BulletType type = config.balaType;

            ObjectPool<Bullet_Player> newPool = new ObjectPool<Bullet_Player>(
                createFunc: () => CreateBullet(prefab, type),
                actionOnGet: GetBullet,
                actionOnRelease: ReleaseBullet,
                actionOnDestroy: DestroyBullet,
                collectionCheck: false,
                defaultCapacity: defaultCapacity,
                maxSize: maxCapacity
            );

            poolsDictionary.Add(type, newPool);
        }
    }

    private Bullet_Player CreateBullet(Bullet_Player prefab, BulletType type)
    {
        // Crear un nuevo proyectil
        Bullet_Player bullet = Instantiate(prefab);

        // Le asignamos el pool correspondiente desde el diccionario si ya existe
        if (poolsDictionary.TryGetValue(type, out var pool))
        {
            bullet.pool = pool;
        }

        // Desactivar el proyectil para que esté oculto
        bullet.gameObject.SetActive(false);
        return bullet;
    }

    private void GetBullet(Bullet_Player bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void ReleaseBullet(Bullet_Player bullet)
    {
        bullet.ResetVelocity();
        bullet.gameObject.SetActive(false);
    }

    private void DestroyBullet(Bullet_Player bullet)
    {
        Destroy(bullet.gameObject);
    }

    // Método helper público para pedir balas
    public Bullet_Player SpawnBullet(BulletType type, Vector3 SpawnPosition, Quaternion rotation, float ForceUp, float Speed)
    {
        if (!poolsDictionary.TryGetValue(type, out ObjectPool<Bullet_Player> pool))
        {
            Debug.LogError($"El tipo de bala '{type}' no está registrado en el Pool Manager.");
            return null;
        }

        Bullet_Player bullet = pool.Get();

        // Garantiza que la bala tenga la referencia al pool asignada
        bullet.pool = pool;

        bullet.transform.SetPositionAndRotation(SpawnPosition, rotation);
        bullet.Shoot(ForceUp, Speed);
        return bullet;
    }
}
