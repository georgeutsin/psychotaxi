using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAnimation : MonoBehaviour
{

    public float rotationSpeed = 200f;
    public RectTransform progressRect;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        progressRect.Rotate(0f, 0f, -rotationSpeed * Time.unscaledDeltaTime);
    }
}