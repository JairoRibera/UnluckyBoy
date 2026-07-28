using UnityEngine;
using UnityEngine.Rendering;

public class Enemy : MonoBehaviour
{
    public float life = 100;

    public void recibirdano(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            Muelto();
        }
    }
    public void Muelto()
    {
        gameObject.SetActive(false);
    }

}
