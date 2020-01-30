using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixProjectile : MonoBehaviour
{
    [SerializeField]
    GameObject projectile1;
    [SerializeField]
    GameObject projectile2;
    [SerializeField]
    GameObject centralProjectile;

    static public float timer = 0.0f;

    [SerializeField]
    bool isSynchronized = false;

    public bool behaveProperly = true;

    bool parented = true;

    float individualTimer = 0.0f;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    float waveAmplitude = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        centralProjectile.transform.position = transform.position;
        if (behaveProperly)
        {
            if (isSynchronized)
            {
                SynchronizedHelix();
            }
            else
            {
                NormalHelix();
            }
        }
        if (parented && !behaveProperly)
        {
            Deparent();
        }
    }

    void SynchronizedHelix()
    {
        transform.up = GetComponent<Rigidbody>().velocity.normalized;
        projectile1.transform.localPosition = new Vector3(Mathf.Sin(timer * Mathf.Deg2Rad) * waveAmplitude, 0, 0);
        projectile2.transform.localPosition = new Vector3(-Mathf.Sin(timer * Mathf.Deg2Rad) * waveAmplitude, 0, 0);
    }

    void NormalHelix()
    {
        individualTimer += Time.deltaTime * 50.0f;
        transform.up = GetComponent<Rigidbody>().velocity.normalized;
        projectile1.transform.localPosition = new Vector3(Mathf.Sin(individualTimer * Mathf.Deg2Rad) * waveAmplitude, 0, 0);
        projectile2.transform.localPosition = new Vector3(-Mathf.Sin(individualTimer * Mathf.Deg2Rad) * waveAmplitude, 0, 0);
    }

    void Deparent()
    {
        projectile1.transform.parent = null;
        projectile1.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;

        projectile2.transform.parent = null;
        projectile2.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;

        projectile1.GetComponent<Projectile>().SetImmortal(false);
        projectile2.GetComponent<Projectile>().SetImmortal(false);
        parented = false;
    }
}
