using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalker : MonoBehaviour
{

    private Rigidbody2D rb;

    private Vector2 inputVector;

    private float speed = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        inputVector = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {   
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        MovePosition(inputVector);
    }
   
    private void MovePosition(Vector2 direction)
    {
        Vector2 actualPosition = (Vector2)rb.position;
        rb.MovePosition(actualPosition + direction * Time.fixedDeltaTime * speed);
    }
}
