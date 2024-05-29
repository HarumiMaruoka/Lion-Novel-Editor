using Glib.NovelGameEditor;
using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private NovelGameController _novelGameController;

    private void Start()
    {
        _novelGameController.Play();
    }
}