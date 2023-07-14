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
    [SerializeField] private Material _powerMaterial;
    [SerializeField] private GameObject _powerObject;

    public event Action<BrickController> BrickDestroyedEvent;
    private List<BrickController> neighborBricks = new List<BrickController>();




    public Vector2Int Index {
        get {return _index;}
        set { _index = value;}}

    public bool IsPowered => _powered;

    public void SetBreakable() { _breakable = true; }

    public override void Awake()
    {
        base.Awake();
        _collider = new CustomColliderBox(transform);
    }

    public void SetPowered()
    {
        _powered = true;
        _meshRenderer.material = _powerMaterial;
    }

    public override void ManagedUpdate()
    {
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
            BallController ballController = GameManager.Instance.LevelManager.GetBallController(index);

            // Check collision with ball.
            if (!_collider.CheckCollision(ballController.GetCollider)) continue;
            
            ballController.Reflect();
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
        var power = Instantiate(_powerObject);
        power.transform.position = transform.position;
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
