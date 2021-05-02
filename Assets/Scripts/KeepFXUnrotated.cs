using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepFXUnrotated : MonoBehaviour
{
    private Transform _transform;
    private Quaternion _startRotation;
    void Awake() 
    {
        _transform = gameObject.GetComponent<Transform>();    
        _startRotation = _transform.rotation;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _transform.rotation = _startRotation;
    }
}
