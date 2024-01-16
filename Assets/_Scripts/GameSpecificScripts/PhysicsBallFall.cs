using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBallFall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.PhysicsBall))
        {
            other.GetComponent<Rigidbody>().useGravity = true;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
