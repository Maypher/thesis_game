using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Collider2D _collider;
    private SpriteRenderer _sprite;

    private void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        if (_collider == null) _collider = GetComponent<Collider2D>();
        if (_sprite == null) _sprite = GetComponent<SpriteRenderer>();

        // Since it gets spawned in the hands of the character there's no need to apply physics to it
        _rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
