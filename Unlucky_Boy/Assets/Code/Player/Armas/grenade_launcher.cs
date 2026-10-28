using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
public class grenade_launcher : MonoBehaviour
{
    public armas ArmData;
    ArmType type = ArmType.Granade;
    private float maxAmmo;
    private float currentAmmo;
    public UnityAction<float, ArmType> SignRecharge;
    public float speed;
    public float ForceUp;
    float nextTimeToShoot = 0f;
    public float fireRate; // Cadencia: dispara cada 1.5 segundos
    [SerializeField] Transform ShootOrigin;
    public UnityAction<BulletType> ChangeBullet;
    //guarda la bala seleccionada actualmente
    private DropItem currentBullet;
    //Un struct (estructura) en C# es una estructura de datos personalizada que agrupa varias variables relacionadas bajo un mismo nombre.
    [System.Serializable]
    public struct DropItem
    {
        //Indicamos el nombre
        public string name;
        //Este es el valor al "peso" que va a tener cada objeto 
        [Range(0, 100)] public float weight;
        //indicamos los tipo de balas que tienen
        public BulletType Type;
    }
    private List<DropItem> balasTragaperras = new List<DropItem> 
    {
        new DropItem {name =  "Proyectil granada", weight = 40f, Type = BulletType.Rebote},
        new DropItem {name =  "Proyectil incendiario", weight = 30f,Type = BulletType.Fire},
        new DropItem {name =  "Proyecil misil", weight = 30f, Type = BulletType.Misil},
    };
    [SerializeField]int[] Resultados;
    private void Start()
    {
        currentAmmo = ArmData.maxAmmo;
        maxAmmo = ArmData.maxAmmo;
        fireRate = ArmData.fireRate;
        // Forzamos la selección de la primera bala al iniciar el juego
        GetRandomBullet();
    }
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
                // 1. Guardamos la bala seleccionada
                currentBullet = item;
                ChangeBullet?.Invoke(currentBullet.Type);
                Debug.Log(item.name);
                return currentBullet;
            }
            randomValue -= item.weight;
        }
        // Por si acaso, fallback a la primera bala
        currentBullet = balasTragaperras[0];
        ChangeBullet?.Invoke(currentBullet.Type);
        //Por si la lista está vacía o hay algun error entonces devolvemos el primer objeto de la lista
        return currentBullet; 
    }

    // Update is called once per frame
    void Update()
    {
        Recharge();
        Shoot();
    }
    public void Shoot()
    {
        if (Input.GetButtonDown("Fire1")&& Time.time >= nextTimeToShoot&& currentAmmo > 0)
        {
            nextTimeToShoot = Time.time + fireRate;
            // Le pedimos la bala al Pool centralizado pasando nuestra posición actual
            Bullet_PlayerPooling.Instance.SpawnBullet(currentBullet.Type ,ShootOrigin.position, ShootOrigin.rotation, speed, ForceUp);
            currentAmmo--;

        }
    }
    public void Recharge() 
    {
        if (currentAmmo < maxAmmo && Input.GetKeyDown(KeyCode.F)) 
        {
            // Comprobamos que el Singleton exista antes de invocarlo
            if (Ammunition_System.Instance != null)
            {
                float ammoNeed = maxAmmo - currentAmmo;
                float ammoToTake = Ammunition_System.Instance.UseAmmo(ammoNeed, ArmType.Granade);
                if(ammoToTake > 0)
                {
                    currentAmmo += ammoToTake;
                    GetRandomBullet();
                }
                else
                {
                    Debug.Log("No hay balas");
                }

                Debug.Log("Recargar lanzagranadas");
            }
        }
        
    }

}
