using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anchor : MonoBehaviour
{
    public Transform anchorTransform;

    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - anchorTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = anchorTransform.position + offset;
    }
}
