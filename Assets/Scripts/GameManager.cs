using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : OdinserializedSingletonBehaviour<GameManager>
{
    public bool spawnPlayer;
    public Player playerPrefab;

    public Player Player { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        LevelManager.Instance.LevelGenerated += LevelGenerated;
    }

    private void LevelGenerated()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var cube = LevelManager.Instance.CurrentCube;

        Player = Instantiate(playerPrefab, cube.transform);

        // find the middle cubelet of the top side
        var nodes = GraphManager.Instance.Nodes[Side.Up];
        var middleNode=  nodes[Mathf.FloorToInt(nodes.Count / 2)];
        Player.transform.position = (Vector3)middleNode.position;

        Player.Init(middleNode);
    }
}
