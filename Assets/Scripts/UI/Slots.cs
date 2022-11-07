using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slots : MonoBehaviour
{
    private int numberOfSlots;

    public int NumberOfSlots
    {
        get { return numberOfSlots; }
    }

    void Start()
    {
        numberOfSlots = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
}
