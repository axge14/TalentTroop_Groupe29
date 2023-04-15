using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGraphics : MonoBehaviour
{

    public ParticleSystem particles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            particles.Play();
        }
    }
}
