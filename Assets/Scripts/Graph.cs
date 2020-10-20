using UnityEngine;
using Pathfinding;
using System;
using System.Collections.Generic;

public class Graph : MonoBehaviour
{
    public float nodeDistance = 0.3f;

    public PointGraph PointGraph { get; private set; }
    public Cube Cube { get; private set; }
    public Dictionary<Facelet, PointNode> Nodes { get; set; } = new Dictionary<Facelet, PointNode>();

    public void Init(Cube cube)
    {
        Cube = cube;

        PointGraph = AstarPath.active.data.AddGraph(typeof(PointGraph)) as PointGraph;

        GenerateNodes();

        GenerateConnections();
    }

    private void GenerateConnections()
    {
        ConnectSideNodes();

        ConnectSidesToSides();

        SetAllConnectionCosts(1);
    }

    private void SetAllConnectionCosts(uint cost)
    {
        AstarPath.active.AddWorkItem(new AstarWorkItem(ctx =>
        {
            PointGraph.GetNodes(node =>
            {
                PointNode pointNode = node as PointNode;

                if (pointNode != null)
                {
                    for (int i = 0; i < pointNode.connections.Length; i++)
                    {
                        pointNode.connections[i].cost = cost;
                    }
                }

                //node.SetConnectivityDirty();
            });

            //PointGraph.RegisterConnectionLength(1);
        }));
    }

    private void ConnectSidesToSides()
    {
        foreach (KeyValuePair<Side, List<Cubelet>> entry in Cube.Sides)
        {
            var side = entry.Key;
            var cubelets = entry.Value;

            foreach (var cubelet in cubelets)
            {
                List<PointNode> nodesOfThisCubelet = GetNodesOfCubelet(cubelet);

                if (nodesOfThisCubelet.Count <= 1)
                    continue;

                ConnectNodes(nodesOfThisCubelet);
            }
        }
    }

    private static void ConnectNodes(List<PointNode> nodesOfThisCubelet)
    {
        foreach (var node in nodesOfThisCubelet)
        {
            foreach (var other in nodesOfThisCubelet)
            {
                if (node == other)
                    continue;

                node.AddConnection(other, 1);
            }
        }
    }

    private List<PointNode> GetNodesOfCubelet(Cubelet cubelet)
    {
        List<PointNode> nodesOfThisCubelet = new List<PointNode>();
        foreach (var facelet in cubelet.facelets)
        {
            PointNode node = null;
            if (Nodes.TryGetValue(facelet, out node))
            {
                nodesOfThisCubelet.Add(node);
            }
        }

        return nodesOfThisCubelet;
    }

    private void ConnectSideNodes()
    {
        PointGraph.maxDistance = 1.1f;
        PointGraph.ConnectNodes();
    }

    //private Side GetSideForNode(GraphNode targetNode)
    //{
    //    foreach (var kvp in Nodes)
    //    {
    //        var side = kvp.Key;
    //        var nodes = kvp.Value;
    //        foreach (var node in nodes)
    //        {
    //            if (targetNode == node)
    //                return side;
    //        }
    //    }

    //    return Side.None;
    //}

    private void GenerateNodes()
    {
        AstarPath.active.Scan(PointGraph);

        Nodes.Clear();

        AstarPath.active.AddWorkItem(new AstarWorkItem(ctx =>
        {
            foreach (KeyValuePair<Side, List<Cubelet>> entry in Cube.Sides)
            {
                var side = entry.Key;
                var cubelets = entry.Value;

                var worldDirection = Utils.GetWorldDirectionForSide(side);
                foreach (var cubelet in cubelets)
                {
                    var outwardFacelet = cubelet.GetFaceletAtWorldDirection(worldDirection);

                    if (outwardFacelet != null)
                    {
                        var nodePosition = outwardFacelet.transform.position + worldDirection.normalized * nodeDistance;
                        var newNode = PointGraph.AddNode((Int3)nodePosition);

                        Nodes.Add(outwardFacelet, newNode);
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