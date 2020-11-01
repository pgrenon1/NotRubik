using UnityEngine;
using Pathfinding;
using System;
using System.Collections.Generic;
using UnityEditor;

public class GraphManager : OdinserializedSingletonBehaviour<GraphManager>
{
    public Mover testPrefab;
    public bool createTestEntity = false;
    public float nodeDistance = 0.3f;

    public PointGraph PointGraph { get; private set; }
    public Cube Cube { get; private set; }
    public Dictionary<Side, List<PointNode>> Nodes { get; private set; } = new Dictionary<Side, List<PointNode>>();
    public bool NodeToFaceletCacheIsDirty { get; set; } = true;

    // look here @alvaro
    private Dictionary<PointNode, Facelet> _nodeToFaceletCache = new Dictionary<PointNode, Facelet>();

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

    public Facelet GetFaceletForNode(PointNode node)
    {
        if (NodeToFaceletCacheIsDirty)
            UpdateFaceletToNodeCache();

        return _nodeToFaceletCache[node];
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

    public PointNode GetNodeForFacelet(Facelet facelet)
    {
        if (NodeToFaceletCacheIsDirty)
            UpdateFaceletToNodeCache();

        foreach (var nodeToFacelet in _nodeToFaceletCache)
        {
            if (nodeToFacelet.Value == facelet)
            {
                return nodeToFacelet.Key;
            }
        }

        return null;
    }

    public Side GetSideForNode(PointNode pointNode)
    {
        foreach (KeyValuePair<Side, List<PointNode>> entry in Nodes)
        {
            Side side = entry.Key;
            List<PointNode> nodes = entry.Value;

            foreach (var node in nodes)
            {
                if (node == pointNode)
                    return side;
            }
        }

        return Side.None;
    }

    private void UpdateFaceletToNodeCache()
    {
        _nodeToFaceletCache.Clear();

        foreach (KeyValuePair<Side, List<PointNode>> entry in Nodes)
        {
            var side = entry.Key;
            var nodes = entry.Value;

            foreach (var node in nodes)
            {
                var facelet = node.GetFacelet();

                if (_nodeToFaceletCache.ContainsKey(node))
                    _nodeToFaceletCache[node] = facelet;
                else
                    _nodeToFaceletCache.Add(node,facelet);
            }
        }

        NodeToFaceletCacheIsDirty = false;
    }

    private List<PointNode> GetNodesOfCubelet(Cubelet cubelet)
    {
        if (NodeToFaceletCacheIsDirty)
            UpdateFaceletToNodeCache();

        List<PointNode> nodesOfThisCubelet = new List<PointNode>();

        foreach (KeyValuePair<Side, List<PointNode>> entry in Nodes)
        {
            var side = entry.Key;
            var nodes = entry.Value;

            foreach (var node in nodes)
            {
                var facelet = _nodeToFaceletCache[node];

                if (cubelet.facelets.Contains(facelet))
                {
                    nodesOfThisCubelet.Add(node);
                }
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

                Nodes[side] = new List<PointNode>();

                var worldDirection = Utils.GetWorldDirectionForSide(side);
                foreach (var cubelet in cubelets)
                {
                    var outwardFacelet = cubelet.GetFaceletAtWorldDirection(worldDirection);

                    if (outwardFacelet != null)
                    {

                        //for (int i = 0 ; i < Cube.subdivisions; i++)
                        //{
       
                            //TODO Correctly subdivide a Facelet
                            var nodePosition = outwardFacelet.transform.position + worldDirection.normalized * nodeDistance;
                            var newNode = PointGraph.AddNode((Int3)nodePosition);

                            Nodes[side].Add(newNode);
                       // }

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
            var pointNode = node as PointNode;
            var position = (Vector3)pointNode.position;
            if (_nodeToFaceletCache[pointNode] != null)
                Gizmos.color = new Color(51, 51, 51, 51);
            else
                Gizmos.color = Color.red;

            Gizmos.DrawSphere(position, 0.1f);

            GUIStyle style = EditorStyles.boldLabel;
            style.normal.textColor = Color.cyan;
            var side = GetSideForNode(pointNode);
            Handles.Label(position + Vector3.up * 0.2f, Nodes[side].IndexOf(pointNode).ToString(), style);
        });
    }
}
