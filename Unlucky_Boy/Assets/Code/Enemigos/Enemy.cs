using UnityEngine;
//Creamos una interfaz para que cada parte del cuerpo pueda recibir el disparo
public interface IsShooteable
{
    //Pasamos el punto de impacto
    public void RecibeShoot(Vector3 hitpoint, float damage);
}
public interface IsPusheable
{
    public void Knockback(Vector3 dir, float distance);
}
public interface IsFire
{
    public void Fire(Vector3 hitpoint, float damage, float time, float inteval);
}
public class Enemy : MonoBehaviour
{
    public float stunTime = .25f;
    public float life = 100;
    [SerializeField]private Rigidbody rb;
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
