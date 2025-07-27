using System.Collections.Generic;
using System;
using UnityEngine;

public interface IPool
{
    public Transform parentTransform { get; set; }
    public Queue<GameObject> pool { get; set; }
    public GameObject Get(Action<GameObject> action = null);

    public void Return(GameObject obj, Action<GameObject> action = null);
}
