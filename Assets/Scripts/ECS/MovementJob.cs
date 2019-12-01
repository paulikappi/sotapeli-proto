using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Burst;
using Unity.Mathematics;

[BurstCompile]
public struct MovementJob : IJobParallelForTransform
{
    public float moveSpeed;
    public float deltaTime;

    public void Execute(int index, TransformAccess transform)
    {
        Vector3 pos = transform.position;
        pos += moveSpeed * deltaTime * (transform.rotation * new Vector3(0f, 0f, 1f));

        transform.position = pos;
    }
}
