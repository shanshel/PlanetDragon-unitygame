using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowMatrialMesh
{
    public Color originalColor;
    public Color originalMessionColor;
    public bool isGoingUp = false; 
    public MeshRenderer renderer;

    public HollowMatrialMesh(MeshRenderer renderer)
    {
        originalColor = renderer.material.color;
        originalMessionColor = renderer.material.GetColor("_EmissionColor");
        this.renderer = renderer;
    }
}
