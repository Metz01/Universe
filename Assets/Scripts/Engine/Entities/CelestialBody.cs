using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Accessibility;

public class CelestialBody : MonoBehaviour
{
    public float mass;
    public float radius;
    public float density;
    public Vector3 initialVelocity;
    private Vector3 currentVelocity;

    void Awake(){
        currentVelocity = initialVelocity;
        density = mass / (4f / 3f * Mathf.PI * Mathf.Pow(radius, 3));
    }

    public void UpdateVelocity(List<CelestialBody> allBodies, float timeStep){
        foreach (CelestialBody body in allBodies){
            if (body != this){
                float sqrDistance = (body.transform.position - transform.position).sqrMagnitude;
                Vector3 forceDirectionNorm = (body.transform.position - transform.position).normalized;
                Vector3 force = body.mass * mass * Universe.gravitationalConstant * forceDirectionNorm / sqrDistance;
                Vector3 acceleration = force / mass;
                currentVelocity += acceleration * timeStep;
                if(sqrDistance < Mathf.Pow(radius + body.radius, 2)){
                    if(body.GetComponent<CelestialBody>().density > density){
                        OnCollision(body, this);
                        allBodies.Remove(this);
                        Destroy(gameObject);
                    }else{
                        OnCollision(this, body);
                        allBodies.Remove(body);
                        Destroy(body.gameObject);
                    }
                }
            }
        }
    }

    public void updatePosition(float timeStep){
        transform.position += currentVelocity * timeStep;
    }

    private void OnCollision(CelestialBody aggregatorBody, CelestialBody destroyedBody){
        print("Collision");
        float winnerMass = aggregatorBody.GetComponent<CelestialBody>().mass;
        float winnerRadius = aggregatorBody.GetComponent<CelestialBody>().radius;
        float loserMass = destroyedBody.GetComponent<CelestialBody>().mass;
        aggregatorBody.GetComponent<CelestialBody>().mass += mass;
        float newRadius = Mathf.Pow(Mathf.Pow(winnerRadius, 3) * (winnerMass + loserMass) / winnerMass, 1f / 3f);
        aggregatorBody.GetComponent<CelestialBody>().radius = newRadius;
        aggregatorBody.transform.localScale = new Vector3(newRadius, newRadius, newRadius);
    }
}