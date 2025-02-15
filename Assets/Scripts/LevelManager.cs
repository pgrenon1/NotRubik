﻿using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class LevelManager : OdinserializedSingletonBehaviour<LevelManager>
{
    public Cube cubePrefab;

    public LevelData defaultLevelData;

    public Cube CurrentCube { get; private set; }
    public LevelData CurrentLevelSetting { get; private set; }

    public delegate void OnLevelGenerated();
    public event OnLevelGenerated LevelGenerated;

    [Button("Generate Level", ButtonSizes.Large)]
    public void GenerateDefault()
    {
        Generate(defaultLevelData);
    }

    private void Start()
    {
        GenerateDefault();
    }

    public void Generate(LevelData levelData)
    {
        DestroyLevel();

        CurrentLevelSetting = levelData;

        CurrentCube = CreateCube();

        CurrentCube.Init(CurrentLevelSetting);

        if (LevelGenerated != null)
            LevelGenerated();
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(cubePrefab);

        return cube;
    }

    private void DestroyLevel()
    {
        if (CurrentCube == null)
            CurrentCube = FindObjectOfType<Cube>();

        if (CurrentCube != null)
            DestroyImmediate(CurrentCube.gameObject);
    }
}
