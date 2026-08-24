using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet_Enemy : MonoBehaviour
{
    //El pool al que pertenece este objeto
    public ObjectPool<Bullet_Enemy> pool;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float damage = 10;
    [SerializeField] private float timer = 0.25f;
    private float time;
    private bool isReleased = false;
    private void Start()
    {
        time = timer;
    }
    private void Update()
    {
        // Si ya devolvimos la bala en este ciclo, no procesamos nada
        if (isReleased) return;

        // El temporizador se resta frame a frame dentro de Update
        time -= Time.deltaTime;
        if (time <= 0)
        {
            ReturnToPool();
        }
    }
    // Al sacarlo del pool o crearlo por primera vez
    private void OnEnable()
    {
        // Reseteamos el contador cada vez que la bala se activa
        time = timer;
        isReleased = false;
    }
    public void Shoot(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    //cuando usas el pooling, se tiene que reiniciar la velocidad del Rigidbody que se conserva cuando es desactivado
    public void ResetVelocity()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isReleased) return;
        if (collision.gameObject.TryGetComponent(out Player_Life playerLife))
        {
            Debug.Log("Toma baklazofsklñadg");
            playerLife.TakeDamage(damage);
            pool.Release(this);
        }
        ////cuando choca contra algo, se devuelve a s� mismo al pool
    }
    private void ReturnToPool()
    {
        // Evita llamar a pool.Release() múltiples veces y rompes el pool
        if (isReleased) return;

        isReleased = true;

        // NO hace falta hacer gameObject.SetActive(false); 
        // El propio ObjectPool de Unity lo desactiva automáticamente con ReleaseBullet
        pool.Release(this);
    }
}

