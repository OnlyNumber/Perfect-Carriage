using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTransform;

    public float Speed;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }


    void Update()
    {
        rectTransform.Rotate(new Vector3(0, 0, Speed * Time.deltaTime));
    }
}
