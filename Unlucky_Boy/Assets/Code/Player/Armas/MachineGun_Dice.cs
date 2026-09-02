using UnityEngine;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine.Rendering;

public class MachineGun_Dice : MonoBehaviour
{
    public enum ShootMode{Normal, Auto}
    private Camera cam;
    [SerializeField] private armas armaData;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] public ShootMode currentShootMode = ShootMode.Normal;

    [Header("Damage (3d6 Dice)")]
    [SerializeField] private int initialDamage = 15; // Daño base si no se usan dados
    public int finalDamage;

    [Header("Datos")]
    int[] CaraDados = new int[] { 1, 2, 3, 4, 5, 6}; 
    public float shootDelay = 0f;
    public bool isShooting = false;
    public int currentAmmo;
    private bool isReloading = false;
    private float nextShootTime = 0.2f;
    [SerializeField] private float timeToTriggerAuto = 0.2f;
    private float holdTimer = 0f;
    private bool isHoldingTrigger = false;
    private void Start()
    {
        cam = Camera.main;
        currentAmmo = armaData.maxAmmo;
        finalDamage = initialDamage;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading) return;

        // Gestión de recarga
        if (currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < armaData.maxAmmo))
        {
            StartCoroutine(ReloadRoutine());
            return;
        }
        //Primer Click (Disparo Semiautomático Instante)
        if (Input.GetButtonDown("Fire1"))
        {
            isHoldingTrigger = true;
            holdTimer = 0f;
            currentShootMode = ShootMode.Normal;

            if (Time.time >= nextShootTime)
            {
                Shoot();
            }
        }

        //Mantener Pulsado (Evaluación de transición a Automático)
        if (Input.GetButton("Fire1") && isHoldingTrigger)
        {
            holdTimer += Time.deltaTime;

            // Si pasa el tiempo límite, se convierte en Automático
            if (holdTimer >= timeToTriggerAuto)
            {
                currentShootMode = ShootMode.Auto;
            }

            // Si ya es automático, dispara siguiendo la cadencia (shootDelay)
            if (currentShootMode == ShootMode.Auto && Time.time >= nextShootTime)
            {
                Shoot();
            }
        }

        //Soltar el Gatillo (Reseteo)
        if (Input.GetButtonUp("Fire1"))
        {
            isHoldingTrigger = false;
            holdTimer = 0f;
            currentShootMode = ShootMode.Normal; // Volvemos a esperar un click normal
        }
    }
    private void PerformRaycast(int damageValue)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, armaData.range))
        {
            if (hit.collider.TryGetComponent(out IsShooteable isShooteable))
            {
                Debug.Log($"Golpeaste a {hit.transform.name} haciendo {damageValue} de daño.");
                isShooteable.RecibeShoot(hit.point, damageValue);
                Debug.Log("DisparoNOrmal");

                // NOTA: Si tu interfaz IsShooteable soporta daño, pásaselo aquí:
                // isShooteable.RecibeShoot(hit.point, damageValue);
            }
            else
            {

            }
        }
    }

    public void Shoot()
    {
    
        nextShootTime = Time.time + shootDelay;
        currentAmmo--;
        // Dibuja una línea ROJA en la ventana de Escena durante 0.1 segundos
        Debug.DrawRay(cam.transform.position, cam.transform.forward * armaData.range, Color.red, 0.1f);
        PerformRaycast(finalDamage);
    }
    private IEnumerator ReloadRoutine()
    {
        isReloading = true;
        Debug.Log("Recargando arma...");

        yield return new WaitForSeconds(3);

        ActualizarDamage(); // El subsistema calcula el daño y cadencia de las próximas balas
        currentAmmo = armaData.maxAmmo;
        isReloading = false;
        Debug.Log("¡Recarga Completa! Dados lanzados.");
    }
    public void ActualizarDamage()
    {
        int dado_1 = Random.Range(0, CaraDados.Length);
        int dado_2 = Random.Range(0, CaraDados.Length);
        int dado_3 = Random.Range(0, CaraDados.Length);
        finalDamage = dado_1 + dado_2 + dado_3;
    }
}

