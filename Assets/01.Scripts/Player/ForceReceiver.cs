using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.4f;

    private Vector3 dampingVelocity;

    private Vector3 impact;
    private float gravityVelocity;

    public Vector3 Movement => impact + Vector3.up * gravityVelocity;

    private void Update()
    {
        if (gravityVelocity < 0f && characterController.isGrounded)
        {
            gravityVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            gravityVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        if (agent != null)
        {
            if (impact.sqrMagnitude < 0.2f * 0.2f) 
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;
        if (agent != null)
        {
            agent.enabled = false;
        }
    }
}
