﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Player : Entity
{
    public void PassTurn()
    {
        EndTurn();
    }
}