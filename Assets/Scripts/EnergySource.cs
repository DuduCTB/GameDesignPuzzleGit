using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnergySource : MonoBehaviour
{
    public bool sourceChargedUp;
    [SerializeField] private bool isAnInfiniteSource;

    private void Start()
    {
        if (isAnInfiniteSource)
        {
            sourceChargedUp = true;
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("recibingEnd"))
        {
            Debug.Log("Source contactando con recibing End");
            RecibingEndPipe recibingEnd = collision.transform.GetComponent<RecibingEndPipe>();

            if (sourceChargedUp)
            {
                recibingEnd.RecibingPartGetsCharged(true);
            }
        }

        if (collision.transform.CompareTag("givingEnd"))
        {
            GivingEnergyEnd givingEnd = collision.transform.GetComponent<GivingEnergyEnd>();

            if (givingEnd.isSendingEnergy)
            {
                sourceChargedUp = true;
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.transform.CompareTag("recibingEnd"))
        {
            RecibingEndPipe recibingEnd = collision.transform.GetComponent<RecibingEndPipe>();

            if (sourceChargedUp && recibingEnd.connectedToAnotherPipe)
            {
                //recibingEnd.isCharged = false;
            }
        }


        if (collision.transform.CompareTag("givingEnd"))
        {
            GivingEnergyEnd givingEnd = collision.transform.GetComponent<GivingEnergyEnd>();

            if (givingEnd.isSendingEnergy && !isAnInfiniteSource)
            {
                sourceChargedUp = false;
            }
        }
    }
}
