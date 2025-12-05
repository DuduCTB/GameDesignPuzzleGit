using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyConector : MonoBehaviour
{
    public bool conectorCharged;
    public bool touchingEnergySource;
    public bool energyFromPipe;
    public bool conectorComesFromGiver;
    public bool energyFromSource;
    [SerializeField] private RecibingEndPipe recibingEnd;
    [SerializeField] private GivingEnergyEnd givingEnd;
    [SerializeField] private ZeldaEnergySource savedEnergySource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider detection)
    {
        if (detection.CompareTag("recibingEnd"))
        {
            Debug.Log("Conector detectando extremo azul");
            recibingEnd = detection.GetComponent<RecibingEndPipe>();
        }
        
        if (detection.CompareTag("givingEnd"))
        {
            Debug.Log("Conector detectando extremo rojo ");
            givingEnd = detection.GetComponent<GivingEnergyEnd>();
            conectorComesFromGiver = true;
        }

        if (detection.CompareTag("energySource"))
        {
            Debug.Log("Conector tiene un energySourceEncima");
            ZeldaEnergySource energySource = detection.GetComponent<ZeldaEnergySource>();
            touchingEnergySource = true;

            if (conectorComesFromGiver) savedEnergySource = energySource;

            if (energySource != null)
            {
                if (conectorCharged && energyFromPipe)
                {
                    savedEnergySource = energySource;
                    energySource.sourceChargedUp = true;
                    energySource.conectedToAPipeChargedConector = true;
                }
            }

           
        }

    }

    private void OnTriggerExit(Collider detection)
    {

        if (detection.CompareTag("energySource"))
        {
            Debug.Log("Parte giving (azul) propia conectada con recibing (azul) ajena");
            ZeldaEnergySource energySource = detection.GetComponent<ZeldaEnergySource>();
            touchingEnergySource = false;

            if (energySource != null)
            {
                if (conectorCharged && !energySource.isAnInfiniteSource)
                {
                    energySource.sourceChargedUp = false;
                }

                if (conectorCharged && !energyFromPipe && !touchingEnergySource)
                {
                    energyFromSource = false;
                    conectorCharged = false;
                    ConectorUpdateAnotherPipeRedEndCharge();
                }

                if (energyFromPipe)
                {
                    energySource.conectedToAPipeChargedConector = false;
                }

            }
            savedEnergySource = null;
        }
    }

    public void ConectorUpdateAnotherPipeRedEndCharge()
    {

        if (recibingEnd != null)
        {
            if (conectorCharged)
            {
                recibingEnd.RecibingPartGetsCharged(true);

                Debug.Log("Parte roja ha cargado parte azul");

            }
            else
            {
                recibingEnd.RecibingPartGetsCharged(false);
            }
        }

        if (givingEnd != null)
        {
            if (givingEnd.isSendingEnergy)
            {
                energyFromPipe = true;
                conectorCharged = true;
                if (savedEnergySource != null && touchingEnergySource)
                {
                    savedEnergySource.conectedToAPipeChargedConector = true;
                    savedEnergySource.sourceChargedUp = true;
                    savedEnergySource.dischargeIsDisabled = false;
                }
            }
            else
            {
                if (energyFromPipe && savedEnergySource != null)
                {
                    savedEnergySource.conectedToAPipeChargedConector = false;
                }

                energyFromPipe = false;
                conectorCharged = false;
            }
        }

        if (conectorCharged && !energyFromPipe && !touchingEnergySource && !energyFromSource)
        {
            conectorCharged = false;

            if (recibingEnd != null)
            {
                recibingEnd.RecibingPartGetsCharged(false);
            }
        }
        else
        {
            if (savedEnergySource != null && !conectorCharged)
            {
                savedEnergySource.conectedToAPipeChargedConector = false;
                savedEnergySource.sourceChargedUp = false;
            }
        }



    }
}
