using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CelestialBodySpawner : MonoBehaviour
{
    public string celestialObjPath = "Prefabs/CelestialObj";
    public int count;
    public static List<CelestialBody> allBodies = new List<CelestialBody>();

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = Random.insideUnitSphere * 5;
            Object newCelestialObj;
            float radius = Random.Range(0.1f, 1.0f);
            newCelestialObj = Instantiate(Resources.Load(celestialObjPath), position, Quaternion.identity);
            newCelestialObj.name = "CelestialObj" + i;
            newCelestialObj.GetComponent<CelestialBody>().radius = radius;
            newCelestialObj.GetComponent<CelestialBody>().mass = radius * 1000000000;
            //newCelestialObj.GetComponent<CelestialBody>().initialVelocity = Random.insideUnitSphere;
            newCelestialObj.GetComponent<CelestialBody>().initialVelocity = Vector3.zero;
            newCelestialObj.GameObject().transform.localScale = new Vector3(radius, radius, radius);

            allBodies.Add(newCelestialObj.GetComponent<CelestialBody>());
        }
    }
}