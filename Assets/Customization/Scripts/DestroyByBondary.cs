using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBondary : MonoBehaviour
{
    void OnTriggerExit(Collider collider)
    {

        if (collider.tag == "Enemy")
        {
            Destroy(collider.gameObject.transform.parent.gameObject.transform.parent.gameObject.transform.parent.gameObject); // Destroy Enemy
        }
        else
        {
            Destroy(collider.gameObject);
        }
    }
}
