using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] float _launchForce = 600;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    
    void Awake() {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    void OnMouseDrag() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);   
    }

    void OnMouseUp(){
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();
        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rigidbody2D.position= _startPosition;
        _rigidbody2D.isKinematic = true;
        _rigidbody2D.velocity = Vector2.zero;
    }
}
