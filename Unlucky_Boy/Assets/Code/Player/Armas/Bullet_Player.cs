using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
public class Bullet_Player : MonoBehaviour
{
    //El pool al que pertenece este objeto
    public ObjectPool<Bullet_Player> pool;
    private bool isReleased = false;
    [SerializeField]private Rigidbody rb;
    [SerializeField] private float timer = 10f;
    private float time;
    private void Start()
    {
        time = timer;
    }
    // Update is called once per frame
    void Update()
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
    public void Shoot(float ForceUp, float speed)
    {
        Vector3 dirShoot = transform.forward * speed + transform.up * ForceUp;
        rb.AddForce(dirShoot, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (isReleased) return;
        ReturnToPool();
    }
    //cuando usas el pooling, se tiene que reiniciar la velocidad del Rigidbody que se conserva cuando es desactivado
    public void ResetVelocity()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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
