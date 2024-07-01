using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets : MonoBehaviour
{
    private Rigidbody targetRb;

    private float upForce = 12.2f;

    private float lowForce = 8;

    private float spawnRangex = 4;

    private float spawnRangey = -1;

    private float torqueValue = 10;

    public int pointValue;

    private GameMenager gameMenager;

    public ParticleSystem explosionParticle;

    public AudioClip  explosionSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        MoveTarget();
        gameMenager = GameObject.Find("Game Menager").GetComponent<GameMenager>();
        audioSource.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveTarget()
    {
        targetRb = GetComponent<Rigidbody>();
    
        targetRb.AddForce( RandomForce() , ForceMode.Impulse);
        targetRb.AddTorque( RandomTorque(), ForceMode.Impulse);
        
        targetRb.transform.position = SpawnPosition();
    }

    private void OnMouseDown()
    {
        if(gameMenager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameMenager.UpdateScore(pointValue);
            OnDestory();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!(gameObject.CompareTag("Bad")) && gameMenager.isGameActive)
        {
            gameMenager.lifes--;
            gameMenager.UpdateLifes(gameMenager.lifes);
            if (gameMenager.lifes == 0)
            {
                gameMenager.GameOver();
            }
        }
    }

    public void  DestroyTarget()
    {
        if(gameMenager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameMenager.UpdateScore(pointValue);
            OnDestory();
        }
    }

    private void OnDestory()
    {
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
    }

    Vector3 RandomForce()
    {
        return Vector3.up * (Random.Range(lowForce,upForce));
    }

    Vector3 RandomTorque()
    {
        return new Vector3(Random.Range(-torqueValue,torqueValue), Random.Range(-torqueValue,torqueValue), Random.Range(-torqueValue,torqueValue));
    }

    Vector3 SpawnPosition()
    {
        return new Vector3(Random.Range(-spawnRangex,spawnRangex), spawnRangey);
    }
}
