using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    // Health e speed serão variáveis comuns para ambos o jogador e os inimigos
    [SerializeField] protected float health = 1f;
    [SerializeField] protected float speed = 2f;


    protected Vector2 inputVector;

    // Precisamos do rigidbody para computar os movimentos
    protected Rigidbody2D rb;

    // Precisamos do animator para ativar as transições entre as animações
    private Animator anim;

    // Verifica se o sprite está olhando para a direita ou não
    private bool isFacingFront = true;

    // Verifica se o player pode mexer ou não
    private bool canMove = true;

    // Verifica se o player está atacando ou não
    protected bool isAttacking = false;

    // Velocidade atual do objeto usado para cálculos
    private float currentSpeed = 0f;

    // Tempo para suavizar o movimento
    [SerializeField] private float dampeningTime = 0.15f;

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
        // Atualiza as variáveis de controle do animator
        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("isAttacking", isAttacking);
    }

    protected void MovePosition(Vector2 direction)
    {
        Vector2 actualPosition = (Vector2)rb.position;
        rb.MovePosition(actualPosition + direction * Time.fixedDeltaTime * speed);
    }

    private void AdjustOrientation(Vector2 movement)
    {
        // Verifica a direção do movimento e 
        // a qual direção o sprite está olhando
        if (isFacingFront && movement.x < 0)
        {
            isFacingFront = false;
            Flip();

        }

        if (!isFacingFront && movement.x > 0)
        {
            isFacingFront = true;
            Flip();
        }
    }

    private void Flip()
    {
        // Muda o sprite de direção dependendo do movimento
        Vector3 objectScale = transform.localScale;
        objectScale.y *= -1;
        transform.localScale = objectScale;
    }

    protected void StopBody()
    {
        // Queremos manter a velocidade em y por conta da gravidade
        // SmoothDamp suaviza o movimento com base em dampeningTime
        Vector2 dampenedVelocity = new Vector2(Mathf.SmoothDamp(rb.velocity.x, 0f, ref currentSpeed, dampeningTime), rb.velocity.y);
        rb.velocity = dampenedVelocity;
    }

    protected void StartAttack()
    {
        isAttacking = true;
    }

    public void PerformAttack()
    {
        isAttacking = false;
        Debug.Log("Atacou!");
    }
}
