using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Rigidbody[] ragdollBody;
    private Collider[] ragdollCollider;

    void Start()
    {
        ragdollBody = GetComponentsInChildren<Rigidbody>();
        ToggleRagdoll(false);
    }


    public void ToggleRagdoll(bool state)
    {
        //Disable or enable ragdolls with bool check
        animator.enabled = !state;
        foreach (Rigidbody rb in ragdollBody)
        {
            rb.isKinematic = !state;
        }
       
    }
}
