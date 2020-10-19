using UnityEngine;
using Pathfinding;
using System;
using System.Collections.Generic;

public class Graph : MonoBehaviour
{
    public float nodeDistance = 0.3f;

    public PointGraph PointGraph { get; private set; }
    public Cube Cube { get; private set; }
    public Dictionary<Side, List<PointNode>> Sides { get; set; } = new Dictionary<Side, List<PointNode>>();

    public void Init(Cube cube)
    {
        Cube = cube;

        PointGraph = AstarPath.active.data.AddGraph(typeof(PointGraph)) as PointGraph;

        GenerateNodes();

        GenerateConnections();
    }

    private void GenerateConnections()
    {
        PointGraph.maxDistance = 1.1f;
        PointGraph.ConnectNodes();
        //PointGraph.Scan();
        AstarPath.active.AddWorkItem(new AstarWorkItem(ctx =>
        {

            foreach (var kvp in Sides)
            {
                var sideNodes = kvp.Value;

                for (int i = 0; i < sideNodes.Count; i++)
                {
                    
                }
            }
            //PointGraph.GetNodes(node =>
            //{
            //});
        }));
    }

    private Side GetSideForNode(GraphNode targetNode)
    {
        foreach (var kvp in Sides)
        {
            var side = kvp.Key;
            var nodes = kvp.Value;
            foreach (var node in nodes)
            {
                if (targetNode == node)
                    return side;
            }
        }

        return Side.None;
    }

    private void GenerateNodes()
    {
        AstarPath.active.Scan(PointGraph);

        Sides.Clear();

        AstarPath.active.AddWorkItem(new AstarWorkItem(ctx =>
        {
            foreach (var kvp in Cube.Sides)
            {
                var side = kvp.Key;

                Sides.Add(side, new List<PointNode>());

                var worldDirection = Utils.GetWorldDirectionForSide(side);
                foreach (var cubelet in kvp.Value)
                {
                    var outwardFacelet = cubelet.GetFaceletAtWorldDirection(worldDirection);

                    if (outwardFacelet != null)
                    {
                        var nodePosition = outwardFacelet.transform.position + worldDirection.normalized * nodeDistance;
                        var newNode = PointGraph.AddNode((Int3)nodePosition);

                        Sides[side].Add(newNode);
                    }
                }
            }
        }));

        AstarPath.active.FlushWorkItems();
    }

    private void OnDrawGizmos()
    {
        if (PointGraph == null)
            return;

        PointGraph.GetNodes(node =>
        {
            var position = (Vector3)node.position;
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(position, 0.1f);
        });
    }
}