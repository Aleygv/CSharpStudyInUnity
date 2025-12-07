using System;
using System.Linq;
using UnityEngine;

public class HighLightOnHover : MonoBehaviour
{
    [SerializeField] private MeshRenderer _renderer;

    private void OnMouseOver()
    {
        _renderer.material.color = Color.green;
    }

    private void OnMouseExit()
    {
        // Debug.Log("Мышь ВНЕ базы");
        _renderer.material.color = Color.cyan;
    }
}
