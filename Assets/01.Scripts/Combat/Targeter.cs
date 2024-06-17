using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();
    public Target CurrentTarget{ get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) return;
        
        targets.Add(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) return;

        targets.Remove(target);
    }

    public bool SelectTarget()
    {
        if(targets.Count ==0) return false;

        CurrentTarget = targets[0];

        return true;
    }

    public void Cancle()
    {
        CurrentTarget = null;
    }
}
