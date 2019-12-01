using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using UnityEngine.Jobs;

public struct NavMeshEntity : IComponentData
{

}

public class NavMeshSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        NavMeshJob navMeshJob = new NavMeshJob
        {
        };
        JobHandle jobHandle = new JobHandle();
    }

    [BurstCompile]
    public struct NavMeshJob : IJobParallelForTransform
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
}