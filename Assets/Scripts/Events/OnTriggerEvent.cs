using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnTriggerEvent : MonoBehaviour
{
    public UnityEvent onEnter;
    public string hitTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == hitTag || hitTag == "")
        {
            onEnter.Invoke();
        }
    }
}
