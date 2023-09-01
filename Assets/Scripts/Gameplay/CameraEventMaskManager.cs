using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEventMaskManager : MonoBehaviour
{
    [SerializeField] private List<int> layersToIgnore; 

    void Awake()
    {
        Camera camera = GetComponent<Camera>();
        int eventMask = 1;
        foreach (int layer in layersToIgnore)
        {
            eventMask = eventMask << layer;
        }
        camera.eventMask = ~eventMask;
    }
}
