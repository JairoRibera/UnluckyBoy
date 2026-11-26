using UnityEngine;

public class WaveEnemyPool : MonoBehaviour
{
    public struct BulletConfiguration{
        public EnemyType enemyType;//Indicamos la bala que se va a necesitar
        public Enemy enemyPrefab;//Prefab que se va a usar
    }
}
