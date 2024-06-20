using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class RagDoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController controller;

    private Collider[] allcolliders;
    private Rigidbody[] allRigidbody;
    private void Start()
    {
        allcolliders = GetComponentsInChildren<Collider>(true);
        allRigidbody = GetComponentsInChildren<Rigidbody>(true);
        ChangeToRagdoll(false);
    }

    public void ChangeToRagdoll(bool isRagdoll)
    {
        foreach(Collider collider in allcolliders)
        {
            if (collider.gameObject.CompareTag("Ragdoll"))
            {
                collider.enabled = isRagdoll;   
            }
        }

        foreach(Rigidbody rigid in allRigidbody)
        {
            rigid.isKinematic = !isRagdoll;
            rigid.useGravity = isRagdoll; 
        }

        controller.enabled = !isRagdoll;
        animator.enabled = !isRagdoll;
    }
}
