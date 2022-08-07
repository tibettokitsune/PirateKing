using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private ModelImporter[] _meshes;

    [SerializeField] private Material[] _materials;

    private void Start()
    {
        foreach (var mesh in _meshes)
        {
            
        }
    }
}
