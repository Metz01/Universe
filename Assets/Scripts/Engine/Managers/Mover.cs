using UnityEngine;

public class CelestialBodyMover : MonoBehaviour
{

    void FixedUpdate(){
        foreach (CelestialBody body in CelestialBodySpawner.allBodies){
            body.UpdateVelocity(CelestialBodySpawner.allBodies, Time.fixedDeltaTime);
            body.updatePosition(Time.fixedDeltaTime);
        }
    }

}