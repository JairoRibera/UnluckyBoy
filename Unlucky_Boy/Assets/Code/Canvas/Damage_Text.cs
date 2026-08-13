using TMPro;
using UnityEngine;
using UnityEngine.Pool;
public class Damage_Text : MonoBehaviour
{

    //El pool al que pertenece este objeto
    public ObjectPool<Damage_Text> pool;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float AnimationSpeed;
    [SerializeField] private float fadeDuration = 0.8f;
    private float timer = 0;
    //private float DurationTimer = 1.5f;
    private Color textColor;
    private void Awake()
    {
        // Guardamos el color con el que configuraste el TextMeshPro en la UI
        if (textMesh != null)
        {
            textColor = textMesh.color;
        }
    }
    //Este es un metodo que se activa cuando consigamos disparar a un enemigo
    public void SetUp(int damage, Vector3 startPosition)
    {
        timer = 0f;
        //igualamos la pos del texto a la posicion del disparo 
        transform.position = startPosition;
        //convertimos el valor del daño en un texto 
        textMesh.text = damage.ToString();
        //Forzamos alpha a 1 para que vuelva a ser visible al salir del pool
        textMesh.color = new Color(textColor.r, textColor.g, textColor.b, 1f);
    }
    public void AnimacionText()
    {
        //Iniciar animacion del objeto, movimiento hacia arriba
        transform.position += Vector3.up * (AnimationSpeed * Time.deltaTime);
        //Esperamos un tiempo
        timer += Time.deltaTime;
        if(timer >= fadeDuration)
        {
            float t = timer / fadeDuration;
            float alpha = Mathf.Lerp(1f, 0f, t);
            textMesh.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            if(alpha <= 0f)
            {
                //Volvemos el objeto al pool
                pool.Release(this);
            }
        }
    }
    private void Update()
    {
        AnimacionText();
    }
}
