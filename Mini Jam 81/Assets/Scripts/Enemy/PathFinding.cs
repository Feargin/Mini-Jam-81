using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PathFinding : MonoBehaviour
{

    [HideInInspector]public Vector2 CurrentCoord;
    [Range(1, 6)][SerializeField] private int _range;
    private List<Transform> _listCoordAvailability;
    public List<Vector2> PathToTarget;


    private void StartSearch()
    {
        
    }

    
    
}

