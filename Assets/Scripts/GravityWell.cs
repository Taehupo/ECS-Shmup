using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    [SerializeField]
    bool isUnmovable = true;

    float G = 6.67f * Mathf.Pow(10, -11);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (null != other.GetComponent<Rigidbody>())
        {
            Rigidbody foreignRB = other.GetComponent<Rigidbody>();

            if (isUnmovable)
            {
                ApplyForceToForeing(foreignRB);
            }
        }
    }

    void ApplyForceToForeing(Rigidbody foreignRigidBody)
    {
        if (null != foreignRigidBody.gameObject.GetComponent<HelixProjectile>())
        {
            foreignRigidBody.gameObject.GetComponent<HelixProjectile>().behaveProperly = false;
        }
        Vector3 baseForForceVector = transform.position - foreignRigidBody.transform.position;
        float force = G * ((GetComponent<Rigidbody>().mass*1000) * (foreignRigidBody.mass*1000)) / baseForForceVector.sqrMagnitude;        
        baseForForceVector.Normalize();
        foreignRigidBody.AddForce(baseForForceVector * force);

        //Debug.Log("Applying " + force.ToString("N2") + " N to " + foreignRigidBody.name);
    }
}
