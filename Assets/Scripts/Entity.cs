using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private PointGraph _pointGraph;
    public PointGraph PointGraph
    {
        get
        {
            if (_pointGraph == null)
                _pointGraph = GraphManager.Instance.PointGraph;

            return _pointGraph;
        }
    }

    protected virtual void Awake()
    {
        
    }
}
