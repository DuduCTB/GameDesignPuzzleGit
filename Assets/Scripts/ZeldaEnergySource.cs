using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldaEnergySource : MonoBehaviour
{
    public bool sourceChargedUp;
    public bool isAnInfiniteSource;

    private void Start()
    {
        if (isAnInfiniteSource)
        {
            sourceChargedUp = true;
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag("energyConnector"))
        {
            Debug.Log("Source contactando con recibing End");
            EnergyConector conector = collision.transform.GetComponent<EnergyConector>();

            if (sourceChargedUp && !conector.energyFromPipe)
            {
                conector.conectorCharged = true;
                conector.ConectorUpdateAnotherPipeRedEndCharge();

            }
        }

    }

    private void OnTriggerExit(Collider collision)
    {

        if (collision.transform.CompareTag("energyConnector"))
        {
            Debug.Log("Source contactando con recibing End");
            EnergyConector conector = collision.transform.GetComponent<EnergyConector>();

            if (sourceChargedUp && !conector.energyFromPipe)
            {
                conector.conectorCharged = false;
                conector.ConectorUpdateAnotherPipeRedEndCharge();

            }
        }

    }
}
