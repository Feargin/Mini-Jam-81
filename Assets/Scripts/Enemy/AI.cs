using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private void TargetFainder()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, transform.localScale * 2, Quaternion.identity);
        
    }

    /*private Transform Targets()
    {
        float distanse;
        Vector3 position = transform.position;
        foreach (var VARIABLE in COLLECTION)
        {
            
        }

        return distanse;
    }*/
    void Update()
    {
        
    }
}
