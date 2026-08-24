using System.Collections;
using UnityEngine;

public class BlackJack_Gun : MonoBehaviour
{
    [SerializeField] private armas armaData;
    [SerializeField] private LayerMask enemyLayer;
    [Header("Blackjack Mechanics")]
    public int maxChargeCards = 3;
    public float timeBetweenCards = 0.4f; // Tiempo de carga entre carta y carta
    private Coroutine chargeCoroutine;
    private int currentChargeSum = 0;
    private int cardsDrawn = 0;

    private Camera cam;
    private int CurrentAmmo;
    private float nextTimeToShoot = 0f;
    private bool isReloading = false;
    private bool isOverheated = false;

    private void Start()
    {
        cam = Camera.main;
        CurrentAmmo = armaData.maxAmmo;
    }
    void Update()
    {
        if (isReloading || isOverheated) return;

        // Disparo normal (Click Izquierdo)
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToShoot)
        {
            if (CurrentAmmo > 0)
            {
                nextTimeToShoot = Time.time + armaData.fireRate;
                normalShoot();
            }
            else
            {
                Debug.Log("Sin munición. Presiona R para recargar.");
            }
        }

        // Carga de BlackJack (Mantener Click Derecho)
        if (Input.GetButtonDown("Fire2") && CurrentAmmo >= 3)
        {
            chargeCoroutine = StartCoroutine(ChargeBlackJack());
        }

        // Soltar Click Derecho para realizar Disparo Cargado
        if (Input.GetButtonUp("Fire2") && chargeCoroutine != null)
        {
            StopCoroutine(chargeCoroutine);
            chargeCoroutine = null;
            ExecuteChargedShoot();
        }

        // Recarga (Tecla R)
        if (Input.GetKeyDown(KeyCode.R) && CurrentAmmo < armaData.maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }
    // Genera una carta
    private int GetRandomCardValue()
    {
        // Valores de Baraja: 2 al 10, J/Q/K (10) y As (11)
        int[] cardPool = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 10, 10, 10, 11 };
        return cardPool[Random.Range(0, cardPool.Length)];
    }
    void normalShoot()
    {
        CurrentAmmo--;
        int cardValue = GetRandomCardValue();
        Debug.Log($"Carta sacada: {cardValue} (Munición: {CurrentAmmo}/{armaData.maxAmmo})");

        PerformRaycast(cardValue);
    }
    // --- LÓGICA DE RAYCAST ---
    private void PerformRaycast(int damageValue)
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, armaData.range))
        {
            if (hit.collider.TryGetComponent(out IsShooteable isShooteable))
            {
                Debug.Log($"Golpeaste a {hit.transform.name} haciendo {damageValue} de daño.");
                isShooteable.RecibeShoot(hit.point, damageValue);

                // NOTA: Si tu interfaz IsShooteable soporta daño, pásaselo aquí:
                // isShooteable.RecibeShoot(hit.point, damageValue);
            }
            else
            {

            }
        }
    }
    // --- SUBSISTEMA BLACKJACK (DISPARO CARGADO) ---
    private IEnumerator ChargeBlackJack()
    {
        currentChargeSum = 0;
        cardsDrawn = 0;

        while (cardsDrawn < maxChargeCards)
        {
            yield return new WaitForSeconds(timeBetweenCards);

            int newCard = GetRandomCardValue();
            currentChargeSum += newCard;
            cardsDrawn++;
            CurrentAmmo--;

            Debug.Log($"Carta #{cardsDrawn} acumulada: {newCard} | Total acumulado: {currentChargeSum}");

            // Si sobrepasa 21 al acumular la 3ª carta, fuerza el disparo y sobrecalentamiento inmediato
            if (currentChargeSum > 21)
            {
                break;
            }
        }
    }

    private void ExecuteChargedShoot()
    {
        if (cardsDrawn == 0) return;

        int finalDamage = 0;

        if (currentChargeSum == 21) // BlackJack
        {
            finalDamage = 42; // Crítico x2
            Debug.Log($"<color=green>¡BLACKJACK! Daño crítico: {finalDamage}</color>");
            PerformRaycast(finalDamage);
        }
        else if (currentChargeSum < 21) // Daño menor de 21
        {
            finalDamage = currentChargeSum;
            Debug.Log($"Disparo Cargado acumulado: {finalDamage}");
            PerformRaycast(finalDamage);
        }
        else // Pasado de 21 (Bust)
        {
            Debug.Log("<color=red>¡TE PASASTE DE 21! Arma sobrecalentada.</color>");
            StartCoroutine(Overheat());
        }

        currentChargeSum = 0;
        cardsDrawn = 0;
    }
    // Recarga
    private IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Recargando...");
        yield return new WaitForSeconds(armaData.reloadTime);
        CurrentAmmo = armaData.maxAmmo;
        isReloading = false;
        Debug.Log("Recarga completa.");
    }

    private IEnumerator Overheat()
    {
        isOverheated = true;
        yield return new WaitForSeconds(armaData.overheatTime);
        isOverheated = false;
        Debug.Log("Arma enfriada.");
    }


        
}

