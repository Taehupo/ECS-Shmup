using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float timer = 0.0f;

    [SerializeField]
    float lifetime = 2.5f;

    [SerializeField]
    bool isImmortal;

    // Update is called once per frame
    void Update()
    {
        if (timer >= lifetime && !isImmortal)
        {
            Destroy(gameObject);
        }

        if (!isImmortal)
        {
            timer += Time.deltaTime;
        }        
    }

    public void SetImmortal(bool _immortal)
    {
        isImmortal = _immortal;
    }
}
