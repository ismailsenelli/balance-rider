using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddForceControl : MonoBehaviour
{
    [SerializeField] float force;
    [SerializeField] Image safetyImage;
    [SerializeField] UIManager uiManager;
    Rigidbody rb;
    RagdollController[] ragdollController;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //Put objects tagged Passenger with RagdollController script in ragdollController array in scene
        GameObject[] ragdollGameObject = GameObject.FindGameObjectsWithTag("Passenger");

        ragdollController = new RagdollController[ragdollGameObject.Length];
        for (int i = 0; i < ragdollGameObject.Length; i++)
        {
            ragdollController[i] = ragdollGameObject[i].GetComponent<RagdollController>();
        }  
    }
    void FixedUpdate()
    {
      
        //Check if vehicle is rotated enough to toggle ragdolls
        if (transform.localRotation.eulerAngles.z >= 50 && transform.localRotation.eulerAngles.z <90) 
        {
            ToggleRagdolls();
           
        }

        if (transform.localRotation.eulerAngles.z <= 310 && transform.localRotation.eulerAngles.z > 270)
        {
            ToggleRagdolls();
        }
        
        SafetyMeter();
        LoseGame();

    }

    void OnTriggerStay(Collider other)
    {
        //Add torque depending on turn
        if (other.gameObject.tag == "AddForceLeft")
        {

            rb.AddRelativeTorque(Vector3.forward * force * Time.deltaTime);
            

        }

        if (other.gameObject.tag == "AddForceRight")
        {

            rb.AddRelativeTorque(Vector3.forward * -force * Time.deltaTime);
        }

        
    }

    void OnTriggerEnter(Collider other)
    {
        //Enable level end UI
        if (other.gameObject.tag == "Finish")
        {
            uiManager.LevelEnd();
        }
    }

    void ToggleRagdolls()
    {
        foreach (RagdollController ragdollControl in ragdollController)
        {
            ragdollControl.ToggleRagdoll(true);
        }
    }

    public void SafetyMeter()
    {
        //Calculate the vehicle rotation angle to fill safety bar
        if (transform.localRotation.eulerAngles.z <= 45 && transform.localRotation.eulerAngles.z < 90)
        {           
            safetyImage.fillAmount = transform.localRotation.eulerAngles.z / 45;
        }
        if (transform.localRotation.eulerAngles.z >= 315 && transform.localRotation.eulerAngles.z > 270)
        { 
            safetyImage.fillAmount = Mathf.Abs(transform.localRotation.eulerAngles.z - 360) / 45;
        }
    }

    public void LoseGame()
    {
        //Fail level check that will end the level if z is rotation bigger than treshold
        if (transform.localRotation.eulerAngles.z >= 70 && transform.localRotation.eulerAngles.z < 80)
        {
            Time.timeScale = 0;
            safetyImage.fillAmount = 1;
        }

        if (transform.localRotation.eulerAngles.z >= 290 && transform.localRotation.eulerAngles.z < 300)
        {
            Time.timeScale = 0;
            safetyImage.fillAmount = 1;
        }

    }

}
