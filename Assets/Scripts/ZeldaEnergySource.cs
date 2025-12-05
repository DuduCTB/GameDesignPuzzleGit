using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeldaEnergySource : MonoBehaviour
{
    public bool sourceChargedUp;
    public bool isAnInfiniteSource;
    public bool conectedToAPipeChargedConector;
    public bool dischargeIsDisabled;
    [SerializeField] private int touchingConnectorsAmount;
    [SerializeField] private EnergyConector connectorToDischarge;

    private void Start()
    {
        if (isAnInfiniteSource)
        {
            sourceChargedUp = true;
        }
    }


    private void Update()
    {
        CheckIfShouldKeepBeingCharged();
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
                conector.energyFromSource = true;
                conector.touchingEnergySource = false;
                conector.ConectorUpdateAnotherPipeRedEndCharge();

            }

            if (!conector.energyFromPipe)
            {
                connectorToDischarge = conector;
            }
            
            
            touchingConnectorsAmount++;
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
                conector.energyFromSource = false;
                conector.touchingEnergySource = false;
                conector.ConectorUpdateAnotherPipeRedEndCharge();
                
            }
            touchingConnectorsAmount--;

            if (conector == connectorToDischarge)
            {
                connectorToDischarge = null;
            }
        }

    }

    private void CheckIfShouldKeepBeingCharged()
    {
        if (sourceChargedUp && !conectedToAPipeChargedConector && !isAnInfiniteSource)
        {
            if (connectorToDischarge != null)
            {
                connectorToDischarge.energyFromSource = false;
                connectorToDischarge.conectorCharged = false;
            }

            sourceChargedUp = false;
        }

        if (sourceChargedUp && connectorToDischarge != null && connectorToDischarge.touchingEnergySource && !connectorToDischarge.conectorCharged)
        {
            connectorToDischarge.energyFromSource = true;
            connectorToDischarge.conectorCharged = true;
            connectorToDischarge.ConectorUpdateAnotherPipeRedEndCharge();
        }

        if (touchingConnectorsAmount == 0 && !isAnInfiniteSource)
        {
            sourceChargedUp = false;
            conectedToAPipeChargedConector = false;
            
        }

        if (!sourceChargedUp && touchingConnectorsAmount >= 1 && !dischargeIsDisabled && connectorToDischarge != null)
        {
            connectorToDischarge.energyFromSource = false;
            connectorToDischarge.conectorCharged = false;
            connectorToDischarge.ConectorUpdateAnotherPipeRedEndCharge();
            dischargeIsDisabled = true;
        }
    }
}
