using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDebugMenu : MonoBehaviour
{
    private bool _showDebugMenu;
    private bool _lol;

    private Cube _cube;
    public Cube Cube
    {
        get
        {
            if (_cube == null)
                _cube = GetComponent<Cube>();

            return _cube;
        }
    }

    private void OnGUI()
    {
        if (_showDebugMenu)
        {
            if (GUILayout.Button("Undo"))
            {
                Cube.Undo();
            }

            if (GUILayout.Button("Shuffle"))
            {
                Cube.Shuffle(10);
            }

            if (!_lol)
            {
                if (GUILayout.Button("Solve"))
                {
                    Cube.Solve();
                    _lol = true;
                }
            }
            else
            {
                if (GUILayout.Button("lol"))
                {
                    Debug.Log("nope");
                }
            }

            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("X+"))
                    {
                        var rotation = new CubeRotation(Vector3.right * 90f);
                        rotation.TryExecute(Cube);
                    }
                    if (GUILayout.Button("X-"))
                    {
                        var rotation = new CubeRotation(Vector3.left * 90f);
                        rotation.TryExecute(Cube);
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Y+"))
                    {
                        var rotation = new CubeRotation(Vector3.up * 90f);
                        rotation.TryExecute(Cube);
                    }
                    if (GUILayout.Button("Y-"))
                    {
                        var rotation = new CubeRotation(Vector3.down * 90f);
                        rotation.TryExecute(Cube);
                    }
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Z+"))
                    {
                        var rotation = new CubeRotation(Vector3.forward * 90f);
                        rotation.TryExecute(Cube);
                    }
                    if (GUILayout.Button("Z-"))
                    {
                        var rotation = new CubeRotation(Vector3.back * 90f);
                        rotation.TryExecute(Cube);
                    }
                }
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Rotate To Origin"))
                {
                    Cube.RotateTo(Quaternion.identity);
                }
            }
            GUILayout.EndVertical();

            GUILayout.BeginVertical();
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Select Front"))
                {
                    Cube.SelectSide(Side.Front);
                }
                if (GUILayout.Button("Select Back"))
                {
                    Cube.SelectSide(Side.Back);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Select Up"))
                {
                    Cube.SelectSide(Side.Up);
                }
                if (GUILayout.Button("Select Down"))
                {
                    Cube.SelectSide(Side.Down);
                }
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                if (GUILayout.Button("Select Right"))
                {
                    Cube.SelectSide(Side.Right);
                }
                if (GUILayout.Button("Select Left"))
                {
                    Cube.SelectSide(Side.Left);
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndVertical();

            GUILayout.BeginArea(new Rect(Screen.width - 200, 0, 200, 20));
            if (GUILayout.Button("Spawn Enemy"))
            {
                var randomSide = Utils.RandomSide(true);
                var nodes = GraphManager.Instance.Nodes[randomSide];
                var node = nodes.RandomElement();
                var entityUp = Utils.GetWorldDirectionForSide(randomSide);
                var forward = node.GetFacelet().transform.right;
                var rotation = Quaternion.LookRotation(forward, entityUp);
                EnemyManager.Instance.SpawnEnemy(node, rotation);
            }
            GUILayout.EndArea();

            GUILayout.BeginArea(new Rect(0, Screen.height - 20, 200, 20));
            if (GUILayout.Button("Pass Turn"))
            {
                GameManager.Instance.Player.EndTurn();
            }
            GUILayout.EndArea();
        }
    }

    public void Toggle()
    {
        _showDebugMenu = !_showDebugMenu;
    }
}
