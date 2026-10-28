using System;
using UnityEngine;

public class Ammunition_System : MonoBehaviour
{
    private ArmType type;
    public static Ammunition_System Instance { get;  set; }
    public float Granade_Ammo;
    public float GunAmmo;
    public float MachineGunAmmo;
    public void Awake()
    {
        // Patrón Singleton para evitar duplicados si volvemos a la escena inicial
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Evita que este objeto se destruya al cambiar de escena
    }
    
    public void AddAmmo(float new_bull, ArmType type)
    {
        //Hacemos un switch que le daremos como valor el ArmType
        //Dependiendo de el tipo de bala recargara una arma u otra
        switch (type)
        {
            case ArmType.Gun: GunAmmo += new_bull;
                break;
            case ArmType.MachineGun: MachineGunAmmo += new_bull;
                break;
            case ArmType.Granade: Granade_Ammo += new_bull;
                break;
            default:
                break;
        }

    }
    public float GetAmmo(ArmType type)
    {
        float currentAmmo = 0;
        switch (type)
        {
            case ArmType.Gun: currentAmmo = GunAmmo;
                break;
            case ArmType.MachineGun: currentAmmo = MachineGunAmmo;
                break;
            case ArmType.Granade: currentAmmo = Granade_Ammo;
                break;
            default:
                break;
        }
        return currentAmmo;
    }
    public float UseAmmo(float ammoNeeded, ArmType type)
    {
        float currentAmmo = GetAmmo(type);
        float ammoToTake = Mathf.Min(ammoNeeded, currentAmmo);
        //El Mathf.Min(a,b)es una función matemática propia de Unity que compara dos o más números y devuelve únicamente el valor más pequeño (el mínimo) de entre todos ellos.
        //Si necesitas 10 balas y tienes 50 balas te devuelve 10 las que necesitas
        //Si necesitas 10 balas y tienes 3 devuelve solo 3
        switch (type)
        {
            case ArmType.Gun: GunAmmo -= ammoToTake;
                break;
            case ArmType.MachineGun: MachineGunAmmo -= ammoToTake;
                break;
            case ArmType.Granade: Granade_Ammo -= ammoToTake;
                break;
            default:
                break;
        }
        return ammoToTake;    
    }
}
