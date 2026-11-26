using UnityEngine;

public class WavesDirector : WaveSystem
{
    [Header("Waves Director")]   
    
    public int wavesNumber;
    public bool isDetected;
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            isDetected = true;
        }
    }
    public void DetectPlayer(){
        if (isDetected){
            SpawnEnemy();
        }
    }
    private void Update(){
        DetectPlayer();
    }

}
