using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivingEnergyEnd : MonoBehaviour
{
    private TriggerPipeManager parentPipe;
    public bool isSendingEnergy;
    public bool connectedToAnotherPipe;

    private void Awake()
    {
        parentPipe = GetComponentInParent<TriggerPipeManager>();
    }

    private void OnTriggerEnter(Collider detection)
    {
        if (detection.CompareTag("recibingEnd"))
        {
            connectedToAnotherPipe = true;
            RecibingEndPipe recibingEnd = detection.GetComponent<RecibingEndPipe>();

            if (recibingEnd.isCharged)
            {
                isSendingEnergy = true;
                parentPipe.sendingGettingEnergy = true;
                
            }
            else
            {
                isSendingEnergy = false;
                parentPipe.sendingGettingEnergy = false;
            }
        }

    }
}
