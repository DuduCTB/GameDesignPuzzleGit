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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("recibingEnd"))
        {
            RecibingEndPipe recibingEnd = collision.transform.GetComponent<RecibingEndPipe>();

            if (sourceChargedUp)
            {
                recibingEnd.isCharged = true;
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

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.CompareTag("recibingEnd"))
        {
            RecibingEndPipe recibingEnd = collision.transform.GetComponent<RecibingEndPipe>();

            if (sourceChargedUp && recibingEnd.connectedToAnotherPipe )
            {
                recibingEnd.isCharged = false;
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
