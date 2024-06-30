using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticleSpawner : MonoBehaviour
{
    private GameObject particleObj;
    
    // Start is called before the first frame update
    void Start()
    {
        particleObj = PlayerController.Instance.collisonParticle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var con in collision.contacts)
        {
            GameObject particle = Instantiate(particleObj, con.point, Quaternion.identity);
            particle.GetComponent<AudioSource>().clip = particle.GetComponent<AudioClipsLib>().clips[Random.Range(0,3)];

        }
        
    }
}
