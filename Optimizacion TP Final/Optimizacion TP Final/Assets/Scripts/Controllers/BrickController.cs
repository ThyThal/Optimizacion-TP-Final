using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviourGameplay
{
    private CustomColliderBox _collider;

    [SerializeField] private Vector2Int _index;
    [SerializeField] private bool _breakable = false;
    [SerializeField] private Mesh mesh;

    [Header("Powered Components")]
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private bool _powered = false;
    [SerializeField] private GameObject _powerObject;
    [SerializeField] private BricksManager bricksManager;
    public event Action<BrickController> BrickDestroyedEvent;
    private List<BrickController> neighborBricks = new List<BrickController>();

    private BallController _ballCollisionCheck;

    public Vector2Int Index {
        get {return _index;}
        set { _index = value;}}

    public bool IsPowered => _powered;

    public void SetBreakable() { _breakable = true; }

    public override void Awake()
    {
        base.Awake();
        _collider = new CustomColliderBox(transform);
        bricksManager = GetComponentInParent<BricksManager>();
    }

    public void SetPowered()
    {
        _powered = true;
        transform.Rotate(0, 180, 0);
    }

    public override void ManagedUpdate()
    {
        if (GameManager.Instance.LevelManager.FinishedGame) return;

        // Check collisions if is breakable.
        if (_breakable)
        {
            CheckBallsCollisions();
        }
    }

    // Check for collision with balls.
    private void CheckBallsCollisions()
    {
        // Check for collision with all balls.
        for (var index = 0; index < GameManager.Instance.LevelManager.Balls.Count; index++)
        {
            _ballCollisionCheck = GameManager.Instance.LevelManager.GetBallController(index);

            // Check collision with ball.
            if (!_collider.CheckCollision(_ballCollisionCheck.GetCollider)) continue;

            _ballCollisionCheck.Reflect();
            DestroyBrick();
        }
    }

    // Method to add a neighbor brick.
    public void AddNeighborBrick(BrickController neighbor)
    {
        if (!neighborBricks.Contains(neighbor))
        {
            neighborBricks.Add(neighbor);
        }
    }

    // Method to remove a neighbor brick.
    public void RemoveNeighborBrick(BrickController neighbor)
    {
        neighborBricks.Remove(neighbor);
    }

    // Method to destroy the brick
    public void DestroyBrick()
    {
        // Perform any actions needed when the brick is destroyed
        _breakable = false;
        GameManager.Instance.LevelManager.DestroyedBrick();

        // Notify the neighboring bricks
        foreach (var neighbor in neighborBricks)
        {
            neighbor.OnNeighborBrickDestroyed(this);
        }

        // Invoke the BrickDestroyedEvent
        BrickDestroyedEvent?.Invoke(this);

        if (IsPowered) { SpawnPower(); }
        gameObject.SetActive(false);
    }

    // Method called when a neighbor brick is destroyed
    private void OnNeighborBrickDestroyed(BrickController neighbor)
    {
        _breakable = true;
    }

    private void SpawnPower()
    {
        var power = bricksManager.objectPool.GetObject();
        power.transform.position = transform.position;
        power.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (mesh == null)
            return;

        Gizmos.color = Color.cyan;

        if (_breakable == false)
            Gizmos.color = Color.red;

        
        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawMesh(mesh, Vector3.zero, Quaternion.identity, Vector3.one * 1);
    }
}
