using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollider
{
    bool CheckCollision(ICollider other);
}
