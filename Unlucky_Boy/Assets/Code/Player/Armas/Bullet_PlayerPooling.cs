using UnityEngine.Pool;
using UnityEngine.UIElements;
using UnityEngine;

public class Bullet_PlayerPooling : MonoBehaviour
{
    public static Bullet_PlayerPooling Instance { get; private set; }
    //Prefab que se va a usar
    [SerializeField] private Bullet_Player bulletPrefab;
    //Capacidad minima del pool que tiene por defecto
    [SerializeField] private int defaultCapacity = 100;
    //Capacidad maxima xD
    [SerializeField] private int maxCapacity = 400;
    // El pool del texto
    public ObjectPool<Bullet_Player> Bullet_Pool;
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
        Bullet_Pool = new ObjectPool<Bullet_Player>(CreateBullet, GetBullet, ReleaseBullet, DestroyBullet, false, defaultCapacity, maxCapacity);
    }
    //esta funcion se llama al crear el pool por tantas veces como objetos pueda tener
    //por ejemplo, si se especifica un tamaño de 20 para el pool, llama a la funcion 20 veces
    private Bullet_Player CreateBullet()
    {
        //crear un nuevo proyectil
        Bullet_Player bullet = Instantiate(bulletPrefab);
        //asignar el pool del proyectil
        bullet.pool = Bullet_Pool;
        //desactivar el proyectil para que este oculto
        bullet.gameObject.SetActive(false);
        return bullet;
    }
    //Se llama cada vez que se coja un texto del pool
    private void GetBullet(Bullet_Player bullet)
    {
        bullet.pool = Bullet_Pool; // <-- Asegura que la bala sepa a qué pool volver
        //al sacar un objeto del pool, lo principal es activarlo
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
    public void SpawnBullet(Vector3 SpawnPosition, Quaternion rotation, float Speed, float ForceUp)
    {
        Bullet_Player bullet = Bullet_Pool.Get();
        bullet.transform.SetPositionAndRotation(SpawnPosition, rotation);
        bullet.Shoot(Speed, ForceUp);
    }
}
