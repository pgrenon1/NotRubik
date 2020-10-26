using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : OdinserializedSingletonBehaviour<EnemyManager>
{
    public Enemy enemyPrefab;

    public List<Enemy> Enemies { get; set; } = new List<Enemy>();

    private Cube _cube;
    public Cube Cube
    {
        get
        {
            if (_cube == null)
                _cube = LevelManager.Instance.CurrentCube;

            return _cube;
        }
    }

    public void SpawnEnemy(PointNode node, Quaternion rotation)
    {
        var newEnemy = Instantiate(enemyPrefab, Cube.transform);

        newEnemy.Init(node);
        newEnemy.transform.position = (Vector3)node.position;
        newEnemy.transform.rotation = rotation;

        Enemies.Add(newEnemy);
        TurnManager.Instance.Actors.Enqueue(newEnemy);
    }
}