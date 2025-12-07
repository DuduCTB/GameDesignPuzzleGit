using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LampBehaviour : MonoBehaviour
{
    public List<Collider> adjacentPipes = new List<Collider>();
    [SerializeField] private float detectionRadius;
    [SerializeField] private LayerMask pipesLayer;
    [SerializeField] private Material lampOnMaterial, lampOffMaterial;
    private Renderer myRenderer;

    [SerializeField] private bool isLampTurnedOn;

    public bool IsLampTurnedOn
    {
        get { return isLampTurnedOn; }
        set
        {
            isLampTurnedOn = value;
            UpdateAdjacentPipeRecibingEnds();
            ChangeMaterialBasedOnEnergy(isLampTurnedOn);
        }
    }

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialPipeSurroundingRecognition();
    }

    // Update is called once per frame
    void Update()
    {
        //InitialPipeSurroundingRecognition();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }



    public void ToggleLamp(bool value)
    {
        IsLampTurnedOn = value;  
    }

    private void InitialPipeSurroundingRecognition()
    {
        Collider[] adjacentPipesArray = Physics.OverlapSphere(transform.position, detectionRadius, pipesLayer, QueryTriggerInteraction.Collide);
        adjacentPipes = new List<Collider>(adjacentPipesArray);
    }

    public void UpdateAdjacentPipeRecibingEnds()
    {
        foreach (Collider i in adjacentPipes)
        {
            if (i.CompareTag("recibingEnd"))
            {
                i.GetComponent<RecibingEndPipe>().RecibingPartGetsCharged(isLampTurnedOn);
            }
        }
    }

    private void ChangeMaterialBasedOnEnergy(bool isConnectedToEnergy)
    {
        if (isConnectedToEnergy)
        {
            myRenderer.material = lampOnMaterial;
        }
        else
        {
            myRenderer.material = lampOffMaterial;
        }
    }




}
