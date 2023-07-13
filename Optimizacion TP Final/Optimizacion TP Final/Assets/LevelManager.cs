using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<BallController> _balls;

    public List<BallController> Balls => _balls;
    public ICollider GetCollider(int index) => _balls[index].GetCollider;
}
