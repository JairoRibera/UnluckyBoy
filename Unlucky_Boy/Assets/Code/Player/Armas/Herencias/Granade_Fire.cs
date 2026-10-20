using UnityEngine;

public class Granade_Fire : Bullet_Player
{
    //Indicamos el tipo de bala que es
    public BulletType Bala;
    //indicamos el scriptable object para que obtenga las modificaciones necesarias
    public Granade_BulletData Data;
    public override void Shoot(float ForceUp, float speed)
    {
        if (Data != null)
        {
            ForceUp = Data.ForceUp;
            speed = Data.Speed;
        }
        base.Shoot(ForceUp, speed);
    }
}
