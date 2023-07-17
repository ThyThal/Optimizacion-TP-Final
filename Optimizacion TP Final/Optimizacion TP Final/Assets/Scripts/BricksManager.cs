using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BricksManager : MonoBehaviourGameplay
{
    [SerializeField] private List<BrickController> brickControllers;
    [SerializeField] private Dictionary<Vector2Int, BrickController> _bricksMatrix = new Dictionary<Vector2Int, BrickController>();
    [SerializeField] private Vector2Int _gridSize;
    [SerializeField] private int _powers = 3;
    public ObjectPool objectPool;

    private void Awake()
    {
        objectPool.InitializePool();
        _bricksMatrix = new Dictionary<Vector2Int, BrickController>();
        var x = 0;
        var y = 0;

        // Create Bricks Matrix.
        foreach (var brick in brickControllers)
        {
            if (x >= _gridSize.x)
            {
                x = 0;
                y++;
            }
            if (y > _gridSize.y)
            {
                y = 0;
            }

            // Sets the indexes and names.
            brick.Index = new Vector2Int(x, y);
            brick.name = $"Brick: {brick.Index}";

            // Adds index to a matrix and its brick.
            _bricksMatrix.Add(brick.Index, brick);

            // Sets border bricks as breakables.
            if (IsBorderBrick(brick))
            {
                brick.SetBreakable();
            }

            x++;
        }

        // Set Neighbour Bricks.
        foreach (var brick in brickControllers)
        {
            GetNeighbours(brick);
        }

        CreatePoweredBricks();
    }

    // Check if the brick is on the border of the grid.
    public bool IsBorderBrick(BrickController brick)
    {
        return brick.Index.x == 0 || brick.Index.x == _gridSize.x - 1 || brick.Index.y == 0 || brick.Index.y == _gridSize.y - 1;
    }

    // Get neighbours bricks.
    public void GetNeighbours(BrickController brick)
    {
        // Sets indexes to check.
        var upIndex = new Vector2Int(brick.Index.x - 1, brick.Index.y);
        var downIndex = new Vector2Int(brick.Index.x + 1, brick.Index.y);
        var leftIndex = new Vector2Int(brick.Index.x, brick.Index.y - 1);
        var rightIndex = new Vector2Int(brick.Index.x, brick.Index.y + 1);

        // Get the values of the neighboring nodes and try to add them.
        BrickController left;
        if (_bricksMatrix.TryGetValue(leftIndex, out left))
        {
            brick.AddNeighborBrick(left);
        }

        BrickController right;
        if (_bricksMatrix.TryGetValue(rightIndex, out right))
        {
            brick.AddNeighborBrick(right);
        }

        BrickController up;
        if (_bricksMatrix.TryGetValue(upIndex, out up))
        {
            brick.AddNeighborBrick(up);
        }

        BrickController down;
        if (_bricksMatrix.TryGetValue(downIndex, out down))
        {
            brick.AddNeighborBrick(down);
        }
    }

    private void CreatePoweredBricks()
    {
        List<BrickController> unpowered = new List<BrickController>(brickControllers);
        _powers = Mathf.Clamp(_powers, 0, brickControllers.Count);

        for (int i = 0; i < _powers; i++)
        {
            var index = Random.Range(0, unpowered.Count);
            unpowered[index].SetPowered();
            unpowered.RemoveAt(index);
        }
    }
}