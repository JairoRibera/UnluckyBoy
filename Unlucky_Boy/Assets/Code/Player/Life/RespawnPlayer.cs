using UnityEngine;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private Player_Life lifeRef;
    [SerializeField] private Barra_Vida barraRef;
    public GameObject player;
    public Transform spawnPoint;
    private void Update()
    {
        Respawn();
    }
    public void Respawn()
    {
        if(lifeRef.actuaLife <= 0 )
        {
            player.transform.position = spawnPoint.position;
            lifeRef.actuaLife = 100;
            barraRef.actualizarBarraVida(lifeRef.actuaLife);

        }
    }
}
