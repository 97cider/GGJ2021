using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugNavController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D _rb;

    public float ballForce = 1.0f;
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) 
        {
            var mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            var mouseDir = mousePos - gameObject.transform.position;
            mouseDir.z = 0.0f;
            mouseDir = mouseDir.normalized;

            _rb.AddForce(mouseDir * ballForce);
        }
    }
}
