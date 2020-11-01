using Pathfinding;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor, IMoveable
{
    public int numberOfMovesPerTurn = 1;
    public int numberOfManipulationsPerTurn = 1;

    private int _numberOfMovesThisTurn = 0;
    private int _numberOfManipulationsThisTurn = 0;

    public bool CanEndTurn { get { return !Mover.IsMoving && LevelManager.Instance.CurrentCube.CanBeManipulated; } }

    public override void Init(PointNode node)
    {
        base.Init(node);

        Mover.EndMovement += Mover_EndMovement;
    }

    private void Mover_EndMovement()
    {
        ConsumeMove();
    }

    private void ConsumeMove()
    {
        _numberOfMovesThisTurn++;
    }

    private void RefundMove()
    {
        _numberOfMovesThisTurn = Mathf.Max(0, _numberOfMovesThisTurn - 1);
    }

    public void ConsumeManipulation()
    {
        _numberOfManipulationsThisTurn++;
    }

    public void RefundManipulation()
    {
        _numberOfManipulationsThisTurn = Mathf.Max(0, _numberOfManipulationsThisTurn - 1);
    }

    public bool CanMove(PointNode pointNode)
    {
        return IsTakingTurn && Mover.NodeIsOrthogonal(pointNode) && _numberOfMovesThisTurn < numberOfMovesPerTurn;
    }

    public override void EndTurn()
    {
        base.EndTurn();

        _numberOfMovesThisTurn = 0;
        _numberOfManipulationsThisTurn = 0;
    }

    public void TryEndTurn()
    {
        if (CanEndTurn)
            EndTurn();
    }

    public bool CanExecuteManipulation(Manipulation manipulation)
    {
        return IsTakingTurn && _numberOfManipulationsThisTurn < numberOfManipulationsPerTurn;
    }
}