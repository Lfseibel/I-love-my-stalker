using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    // Health e speed serão variáveis comuns para ambos o jogador e os inimigos
    [SerializeField] protected float health = 1f;
    [SerializeField] protected float speed = 2f;

    protected Animator anim;

    protected Vector2 inputVector;

    // Precisamos do rigidbody para computar os movimentos
    protected Rigidbody2D rb;

    protected Vector2 currentSpeed = Vector2.zero;

    protected float dampeningTime = 0.1f;

    // Verifica se o player está atacando ou não
    protected bool isAttacking = false;

    // Objeto de ataque (players e inimigos)
    [SerializeField] GameObject attackObject;


    // Awake é executado antes do Start
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update é chamado uma vez por frame
    protected void Update()
    {
        
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.sqrMagnitude));
    }

    protected void MovePosition(Vector2 direction)
    {
        Vector2 actualPosition = (Vector2)rb.position;
        rb.velocity = direction * speed;
        //rb.MovePosition(actualPosition + direction.normalized * Time.fixedDeltaTime * speed);
    }

    protected void StopBody()
    {
        rb.velocity = Vector2.SmoothDamp(rb.velocity, Vector2.zero, ref currentSpeed, dampeningTime);
    }
}
