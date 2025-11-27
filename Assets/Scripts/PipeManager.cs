using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class PipeManager : MonoBehaviour
{

    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask pipeLayer;

    [SerializeField] private PipeType pipeType;
    [SerializeField] private bool connected;
    [SerializeField] private Material pipeOnMaterial, pipeOffMaterial;

    private Renderer myRenderer;
    public bool turnedOn;


    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (turnedOn)
        {
            myRenderer.material = pipeOnMaterial;
        }
        else
        {
            myRenderer.material = pipeOffMaterial;
        }
    }

    private void CheckIfPipeIsConected()
    {
        Collider[] nearPipes = Physics.OverlapSphere(transform.position, detectionRadius, pipeLayer);

        switch (pipeType)
        {
            case PipeType.Straight:

                
                foreach (Collider pipes in nearPipes)
                {
                    float localPipeCoordenateToCheck = 0;
                    float adjacentPipeCoordenateToCheck = 0;

                    if (transform.rotation.y == 0 || transform.rotation.y == 180)
                    {
                        adjacentPipeCoordenateToCheck = pipes.transform.localPosition.z;
                        localPipeCoordenateToCheck = transform.localPosition.z;
                    }
                    else
                    {
                        adjacentPipeCoordenateToCheck = pipes.transform.localPosition.x;
                        localPipeCoordenateToCheck = transform.localPosition.x;
                    }

                    if (adjacentPipeCoordenateToCheck == localPipeCoordenateToCheck + 10 || adjacentPipeCoordenateToCheck == localPipeCoordenateToCheck - 10)
                    {
                        connected = true;

                        if (pipes.GetComponent<PipeManager>().turnedOn && pipes.GetComponent<PipeManager>().connected)
                        {
                            turnedOn = true;
                        }
                    }
                }

                break;

            case PipeType.Corner:

                int zOffsetCheck = 0;
                int xOffsetCheck = 0;

                switch (transform.rotation.y)
                {
                    case 0:
                        zOffsetCheck = 10;
                        xOffsetCheck = -10;
                        break;
                    case 90:
                        zOffsetCheck = 10;
                        xOffsetCheck = 10;
                        break;
                    case 180:
                        zOffsetCheck = -10;
                        xOffsetCheck = 10;
                        break;
                    case 270:
                        zOffsetCheck = -10;
                        xOffsetCheck = -10;
                        break;
                }

                //Collider[] nearPipes = Physics.OverlapSphere(transform.position, detectionRadius, pipeLayer);

                foreach (Collider pipes in nearPipes)
                {
                    if (pipes.transform.localPosition.z == transform.localPosition.z + zOffsetCheck || pipes.transform.localPosition.x == transform.localPosition.x + xOffsetCheck)
                    {
                        connected = true;

                        if (pipes.GetComponent<PipeManager>().turnedOn && pipes.GetComponent<PipeManager>().connected)
                        {
                            turnedOn = true;
                        }
                    }
                }

                break;
        }
    }
}
