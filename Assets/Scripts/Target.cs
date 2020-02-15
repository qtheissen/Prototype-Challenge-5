using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;
    public int pointValue;
    public ParticleSystem explosionParticle;
    private Rigidbody targetRb;
    // Start is called before the first frame update
    void Start()
    {
        //get the rigidbody component
        targetRb = GetComponent<Rigidbody>();
        //add a random ammount of force to the rigidbody component to launch it up into the air
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        //add random torque to make the object spin
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        //give the object a random spawn location
        transform.position = RandomSpawnPos();
        //find the game manager in the scene
        gameManager = FindObjectOfType<GameManager>();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
        
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
        
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        //only call this code if the mouse is pressed and the gamemanager says the game is active
        if (gameManager.isGameActive)
        {
           Destroy(gameObject);
           //summon particle effects
           Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
           //update the score in the gamemanager
           gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
}
