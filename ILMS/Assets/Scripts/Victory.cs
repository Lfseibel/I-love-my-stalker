using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : EntityController
{
    // pursueRange  : Persegue player a partir dessa distância
    // attackRange  : Ataca player a partir dessa distância
    // retreatRange : Recua do player a partir dessa distância
    [SerializeField] private float pursueRange, retreatRange, attackDistance;

    [SerializeField] private LayerMask playerMask;

    // Objeto do player usado como referência
    // em diversas verificações
    private GameObject player;

    // Distância do inimigo ao player
    protected float distPlayer;



    // Direção do player, em coordenadas x
    protected float playerDirection;

    // Verifica o estado de movimento
    // -1 - Idle | 2 - Aproximação | 1 - Ataque | 0 - Recuo
    protected float enemyMovementStatus = -1;

    // Threshold de movimento, essa lista é usada
    // para evitar excessivos aninhamentos de if-elses
    protected float[] enemyMovementThreshold = new float[3];

    // Verifica se pode atacar
    protected bool canAttack = true;

    // Tempo de espera para criar outro projétil
    [SerializeField] private float attackCooldown = 2f;

    // Variável de contagem do tempo de espero
    private float attackCooldownTimer;

    // Awake é executado antes do Start
    // base.Awake() copia os conteúdos
    // de CharacterController : Awake ()
    private new void Awake()
    {
        base.Awake();

        // Procurar o objeto do player na cena
        // ATENÇÃO: As funções GameObject.Find e afins
        // tem performance ruim, tenha certeza
        // de não usá-las constantemente
        player = GameObject.Find("Stalker");

        // Cria uma lista de 'thresholds' the ações
        // Imagem em anexo para visualizar


        // Inicializa o contador do ataque
        attackCooldownTimer = attackCooldown;
    }

    // Update é chamado uma vez por frame
    // base.Update() copia os conteúdos
    // de CharacterController : Update ()
    protected new void Update()
    {
        base.Update();

        // Calcula a distância ao player
        distPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distPlayer <= pursueRange)
        {
            MoveTowardsPlayer();
            if (distPlayer <= attackDistance)
            {

                anim.SetBool("IsAtacking", true);
            }
        }


    }

    // Função genérica de movimentação
    // isNotRetreat verifica se é aproximação
    // ou recuo.
    protected void MoveTowardsPlayer()
    {
        Vector2 direction = transform.position - player.transform.position;
        MovePosition(-direction);
    }

    public void PerformAttack()
    {

        Collider2D[] rc;

        rc = Physics2D.OverlapCircleAll(transform.position, attackDistance, playerMask);

        foreach (Collider2D hit in rc)
        {
            if (hit != null)
            {
                if (hit.gameObject.CompareTag("Player"))
                {
                    hit.gameObject.GetComponent<Stalker>().Ganha();
                    break;
                }
            }
        }
        anim.SetBool("IsAtacking", false);
    }

}
