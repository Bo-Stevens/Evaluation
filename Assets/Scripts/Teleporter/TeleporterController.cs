using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class TeleporterController : MonoBehaviour
{
    [HideInInspector] public Mesh TeleporterMesh;
    // Start is called before the first frame update
    void Awake()
    {
        TeleporterMesh = GetComponent<MeshFilter>().mesh;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.forward * 2);
    }
}
