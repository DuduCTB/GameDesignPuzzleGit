using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivingEnergyEnd : MonoBehaviour
{
    [SerializeField] private TriggerPipeManager parentPipe;
    public bool isSendingEnergy;
    public bool connectedToAnotherPipe;
    [SerializeField] private  RecibingEndPipe recibingEnd;
    [SerializeField] private  EnergyConector myEnergyConector;
    [SerializeField] private  LampBehaviour lampNearby;
    [SerializeField] private  float detectionRadius = 0.1f;
    [SerializeField] private LayerMask pipesLayer;
    [SerializeField] private bool detectingPipes = true;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody>();
        parentPipe = GetComponentInParent<TriggerPipeManager>();
    }

    private void Start()
    {
        StartCoroutine(UpdateAdjacentStuff());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void DetectStuffNearGivingEnd()
    {
        Collider[] detectedStuff = Physics.OverlapSphere (transform.position, detectionRadius, pipesLayer);
        recibingEnd = null;
        myEnergyConector = null;
        lampNearby = null;

        foreach (Collider detectedObject in detectedStuff)
        {

            if (detectedObject.CompareTag("energyConnector"))
            {

                //Debug.Log("Parte giving (azul) propia conectada con recibing (azul) ajena");
                connectedToAnotherPipe = true;
                myEnergyConector = detectedObject.GetComponent<EnergyConector>();

                //Debug.Log("extremo detectedado azul se llama = " + recibingEnd.name);
            }

            if (detectedObject.CompareTag("recibingEnd"))
            {

                //Debug.Log("Parte giving (azul) propia conectada con recibing (azul) ajena");
                connectedToAnotherPipe = true;
                recibingEnd = detectedObject.GetComponent<RecibingEndPipe>();

                //Debug.Log("extremo detectedado azul se llama = " + recibingEnd.name);
            }

            if (detectedObject.transform.CompareTag("energyLamp"))
            {
                lampNearby = detectedObject.GetComponent<LampBehaviour>();
            }
        }
    }

    private IEnumerator UpdateAdjacentStuff()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        while (detectingPipes)
        {
            DetectStuffNearGivingEnd();
            UpdateAnotherPipeRedEndCharge();
            rb.WakeUp();
            yield return wait;
        }
    }

    private void UpdateAnotherPipeRedEndCharge()
    {

        if (recibingEnd != null)
        {
            if (isSendingEnergy)
            {
                recibingEnd.RecibingPartGetsCharged(true);
                parentPipe.sendingGettingEnergy = true;
                //Debug.Log("Parte roja ha cargado parte azul");

            }
            else
            {
                recibingEnd.RecibingPartGetsCharged(false);
                parentPipe.sendingGettingEnergy = false;
            }
        }

        if (myEnergyConector != null)
        {
            if (isSendingEnergy)
            {
                myEnergyConector.ConectorUpdateAnotherPipeRedEndCharge();

            }
            else
            {
                myEnergyConector.ConectorUpdateAnotherPipeRedEndCharge();
            }
        }

        if (lampNearby != null)
        {
            if (isSendingEnergy)
            {
                lampNearby.ToggleLamp(true);

            }
            else
            {
                lampNearby.ToggleLamp(false);
            }
        }
    }
}
