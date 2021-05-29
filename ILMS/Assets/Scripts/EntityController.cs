using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    // Health e speed ser�o vari�veis comuns para ambos o jogador e os inimigos
    [SerializeField] protected float health = 1f;
    [SerializeField] protected float speed = 2f;


    protected Vector2 inputVector;

    // Precisamos do rigidbody para computar os movimentos
    protected Rigidbody2D rb;



    // Verifica se o player est� atacando ou n�o
    protected bool isAttacking = false;



    // Objeto de ataque (players e inimigos)
    [SerializeField] GameObject attackObject;


    // Awake � executado antes do Start
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
       
    }

    // Update � chamado uma vez por frame
    protected void Update()
    {
      
    }

    protected void MovePosition(Vector2 direction)
    {
        Vector2 actualPosition = (Vector2)rb.position;
        rb.MovePosition(actualPosition + direction.normalized * Time.fixedDeltaTime * speed);
    }



 


}
