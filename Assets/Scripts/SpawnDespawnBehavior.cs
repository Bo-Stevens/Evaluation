using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class SpawnDespawnBehavior : ScriptableObject
{
    public abstract void RunSpawnBehavior(Transform transform);
    public abstract void RunDespawnBehavior(Transform transform, TweenCallback onComplete);

    public delegate void OnDespawnComplete();
}
