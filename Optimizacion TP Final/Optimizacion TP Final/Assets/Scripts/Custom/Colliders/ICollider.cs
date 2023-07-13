using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollider
{
    void CheckCollision(ICollider other);
}
