using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CircleParameter
{
    DEFAULT = 0,
    EQUALDISTRIBUTION,
    ANGLERANGE,
    MOVINGANGLERANGE,
    PROGRESSIVECIRCLE
}

public class CirclePattern : MonoBehaviour
{
    [SerializeField]
    [Range(0,999)]
    int numProjectiles = 10;

    [SerializeField]
    GameObject projectile;

    [SerializeField]
    float projectileSpeed;

    //Used for non equal distribution around a circle.
    [SerializeField]
    [Range(0, 360)]
    float stepAngle;

    [SerializeField]
    bool timedSpawn;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    float circleRadius;

    [SerializeField]
    CircleParameter launcherParameter;

    [SerializeField]
    [Range(0, 360)]
    float startAngle;

    [SerializeField]
    [Range(0, 360)]
    float endAngle;

    [SerializeField]
    [Range(0, 360)]
    float angle;

    Vector3 center;

    float timer = 0.0f;

    float progressiveIterator = 0.0f;
    float progressiveIterator2 = 0.0f;

    void Start()
    {
        //InstantiateCircle(isEquallyDistributed);
        progressiveIterator = 0.0f;
        progressiveIterator2 = angle;
    }

    void Update()
    {
        
        center = transform.position;
        //Debug condition. Should only have the if part of the else into prod.
        if (!timedSpawn)
        {
            //DeleteProjectiles(); This is commented just in order to have beautiful helixes
            InstantiateCircle(launcherParameter);
        }
        else if (timer > spawnInterval)
        {
            InstantiateCircle(launcherParameter);
            timer = 0.0f;
        }
        timer += Time.deltaTime;
    }

    void DeleteProjectiles()
    {
        List<Projectile> projectileList = FindObjectsOfType<Projectile>().ToList();
        foreach (Projectile proj in projectileList)
        {
            Destroy(proj.gameObject);
        }
    }

    void InstantiateCircleEqualDistribution()
    {
        for (float i = 0.0f; i < 360.0f; i += 360.0f / (float)numProjectiles)
        {
            float angle1 = Mathf.Cos(i * Mathf.Deg2Rad);
            float angle2 = Mathf.Sin(i * Mathf.Deg2Rad);
            Vector3 pos = new Vector3(circleRadius * angle1, circleRadius * angle2, 0.0f) + center;
            GameObject go = Instantiate(projectile, pos, Quaternion.identity);
            Vector3 tempForce = (go.transform.position - transform.position);
            tempForce.Normalize();
            go.GetComponent<Rigidbody>().AddForce(tempForce * projectileSpeed);
            //go.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }

    void InstantiateCircleNonEqualDistrib()
    {
        int projNum = 0;
        for (float i = 0; i < 360 && projNum < numProjectiles; i += stepAngle)
        {
            float angle1 = Mathf.Cos(i * Mathf.Deg2Rad);
            float angle2 = Mathf.Sin(i * Mathf.Deg2Rad);
            Vector3 pos = new Vector3(circleRadius * angle1, circleRadius * angle2, 0.0f) + center;
            GameObject go = Instantiate(projectile, pos, Quaternion.identity);
            Vector3 tempForce = (go.transform.position - transform.position);
            tempForce.Normalize();
            go.GetComponent<Rigidbody>().AddForce(tempForce * projectileSpeed);
            //go.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
            projNum++;
        }
    }

    void InstantiateCircleAngleRange(float startAngle, float endAngle)
    {
        for (float i = startAngle; i < endAngle; i += stepAngle)
        {
            float angle1 = Mathf.Cos(i * Mathf.Deg2Rad);
            float angle2 = Mathf.Sin(i * Mathf.Deg2Rad);
            Vector3 pos = new Vector3(circleRadius * angle1, circleRadius * angle2, 0.0f) + center;
            GameObject go = Instantiate(projectile, pos, Quaternion.identity);
            Vector3 tempForce = (go.transform.position - transform.position);
            tempForce.Normalize();
            go.GetComponent<Rigidbody>().AddForce(tempForce * projectileSpeed);
            //go.GetComponent<MeshRenderer>().material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        }
    }

    void InstantiateCircleMovingAngleRange()
    {
        InstantiateCircleAngleRange(progressiveIterator, progressiveIterator2);
        progressiveIterator += stepAngle;
        progressiveIterator2 += stepAngle;
    }

    void InstantiateProgressiveCircle()
    {
        float angle1 = Mathf.Cos(progressiveIterator * Mathf.Deg2Rad);
        float angle2 = Mathf.Sin(progressiveIterator * Mathf.Deg2Rad);
        Vector3 pos = new Vector3(circleRadius * angle1, circleRadius * angle2, 0.0f) + center;
        GameObject go = Instantiate(projectile, pos, Quaternion.identity);
        Vector3 tempForce = (go.transform.position - transform.position);
        tempForce.Normalize();
        go.GetComponent<Rigidbody>().AddForce(tempForce * projectileSpeed);
        progressiveIterator += 360.0f / (float)numProjectiles;
    }

    void InstantiateCircle(CircleParameter param)
    {
        switch (param)
        {
            case CircleParameter.DEFAULT:
                InstantiateCircleNonEqualDistrib();
                break;
            case CircleParameter.EQUALDISTRIBUTION:
                InstantiateCircleEqualDistribution();
                break;
            case CircleParameter.ANGLERANGE:
                InstantiateCircleAngleRange(startAngle, endAngle);
                break;
            case CircleParameter.MOVINGANGLERANGE:
                InstantiateCircleMovingAngleRange();
                break;
            case CircleParameter.PROGRESSIVECIRCLE:
                InstantiateProgressiveCircle();
                break;
            default:
                break;
        }      
    }
}
