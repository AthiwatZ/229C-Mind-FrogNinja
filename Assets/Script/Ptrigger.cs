using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ptigger : MonoBehaviour
{
    public float force, mass, acc;

    void CalculateForce()
    {
        mass = GetComponent<Rigidbody>().mass;
        force = mass * acc;

        GetComponent<Rigidbody>().AddForce(0, force, force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TriggerMushroom1"))
        {
            acc = 750;
            CalculateForce();
        }
        else if (other.gameObject.CompareTag("TriggerMushroom2"))
        {
            acc = 450;
            CalculateForce();
        }
        else if (other.gameObject.CompareTag("TriggerMushroom3"))
        {
            acc = 1750;
            CalculateForce();
        }
    }
}
