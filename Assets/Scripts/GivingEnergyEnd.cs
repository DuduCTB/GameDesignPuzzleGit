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

    private void Awake()
    {
        parentPipe = GetComponentInParent<TriggerPipeManager>();
    }

    private void Update()
    {
        UpdateAnotherPipeRedEndCharge();
    }

    private void OnTriggerEnter(Collider detection)
    {
        if (detection.CompareTag("recibingEnd"))
        {

            Debug.Log("Parte giving (azul) propia conectada con recibing (azul) ajena");
            connectedToAnotherPipe = true;
            recibingEnd = detection.GetComponent<RecibingEndPipe>();

            Debug.Log("extremo detectedado azul se llama = " + recibingEnd.name);
        }

        if (detection.CompareTag("energyConnector"))
        {

            Debug.Log("Parte giving (azul) propia conectada con recibing (azul) ajena");
            connectedToAnotherPipe = true;
            myEnergyConector = detection.GetComponent<EnergyConector>();

            Debug.Log("extremo detectedado azul se llama = " + recibingEnd.name);
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
                Debug.Log("Parte roja ha cargado parte azul");

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
    }
}
