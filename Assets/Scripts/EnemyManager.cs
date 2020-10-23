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

    public void TakeEnemyTurns() 
    {
        foreach (var enemy in Enemies)
        {
            enemy.TakeTurn();
        }
    }

    public void SpawnEnemy(Facelet facelet, Quaternion rotation)
    {
        var newEnemy = Instantiate(enemyPrefab, Cube.transform);

        newEnemy.Init(facelet);
        var node = GraphManager.Instance.Nodes[facelet];
        newEnemy.transform.position = (Vector3)node.position;
        newEnemy.transform.rotation = rotation;

        Enemies.Add(newEnemy);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(Screen.width - 200, 0, 200, 60));
        if (GUILayout.Button("Spawn Enemy"))
        {
            var facelet = Cube.AllCubelets[0].facelets[0];
            var entityUp = -facelet.transform.forward;
            var forward = facelet.transform.right;
            var rotation = Quaternion.LookRotation(forward, entityUp);
            SpawnEnemy(facelet, rotation);
        }
        GUILayout.EndArea();
    }
}
