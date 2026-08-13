using UnityEngine;

public class Player_Life : MonoBehaviour
{
    [SerializeField] private float maxLife = 100f;
    [SerializeField] public float actuaLife;
    [SerializeField] Barra_Vida barravidaRef;
    private void Start()
    {
        actuaLife = maxLife;
        barravidaRef.iniciarBarraVida(maxLife);
    }
    private void Update()
    {
        barravidaRef.actualizarBarraVida(actuaLife);    
    }
    public void TakeDamage(float damage)
    {
        actuaLife -= damage;
        if(actuaLife <= 0)
        {
            Debug.Log("Estas muelto mijo ponte pilas apá");
            barravidaRef.actualizarBarraVida(actuaLife);
        }
    }
}