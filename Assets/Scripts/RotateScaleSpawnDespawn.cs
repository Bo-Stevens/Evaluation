using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawn-Despawn/RotateAndScale")]
public class RotateScaleSpawnDespawn : SpawnDespawnBehavior
{
    [SerializeField] [Range(0, 10)] float time;
    [SerializeField] [Min(0)] int numberOfRotations;
    [SerializeField] Ease scaleEase;
    [SerializeField] Ease rotationEase;
    Vector3 fullSize;
    public override void RunSpawnBehavior(Transform transform)
    {
        fullSize = transform.localScale;
        transform.DORotate(transform.rotation.eulerAngles + new Vector3(0, 360 * numberOfRotations, 0), time, RotateMode.FastBeyond360).SetEase(rotationEase);
        transform.localScale = Vector3.zero;
        transform.DOScaleX(fullSize.x, time).SetEase(scaleEase);
        transform.DOScaleY(fullSize.y, time).SetEase(scaleEase);
        transform.DOScaleZ(fullSize.z, time).SetEase(scaleEase);
    }

    public override void RunDespawnBehavior(Transform transform, TweenCallback OnComplete)
    {
        transform.DORotate(transform.rotation.eulerAngles - new Vector3(0, 360 * numberOfRotations, 0), time, RotateMode.FastBeyond360).SetEase(rotationEase).OnComplete(OnComplete);
        transform.DOScale(0, time).SetEase(scaleEase);
    }

}
