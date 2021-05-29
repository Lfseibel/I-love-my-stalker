using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victory : EntityController
{
    // pursueRange  : Persegue player a partir dessa dist�ncia
    // attackRange  : Ataca player a partir dessa dist�ncia
    // retreatRange : Recua do player a partir dessa dist�ncia
    [SerializeField] private float pursueRange, retreatRange, attackDistance;

    [SerializeField] private LayerMask playerMask;

    // Objeto do player usado como refer�ncia
    // em diversas verifica��es
    private GameObject player;

    // Dist�ncia do inimigo ao player
    protected float distPlayer;



    // Dire��o do player, em coordenadas x
    protected float playerDirection;

    // Verifica o estado de movimento
    // -1 - Idle | 2 - Aproxima��o | 1 - Ataque | 0 - Recuo
    protected float enemyMovementStatus = -1;

    // Threshold de movimento, essa lista � usada
    // para evitar excessivos aninhamentos de if-elses
    protected float[] enemyMovementThreshold = new float[3];

    // Verifica se pode atacar
    protected bool canAttack = true;

    // Tempo de espera para criar outro proj�til
    [SerializeField] private float attackCooldown = 2f;

    // Vari�vel de contagem do tempo de espero
    private float attackCooldownTimer;

    // Awake � executado antes do Start
    // base.Awake() copia os conte�dos
    // de CharacterController : Awake ()
    private new void Awake()
    {
        base.Awake();

        // Procurar o objeto do player na cena
        // ATEN��O: As fun��es GameObject.Find e afins
        // tem performance ruim, tenha certeza
        // de n�o us�-las constantemente
        player = GameObject.Find("Stalker");

        // Cria uma lista de 'thresholds' the a��es
        // Imagem em anexo para visualizar


        // Inicializa o contador do ataque
        attackCooldownTimer = attackCooldown;
    }

    // Update � chamado uma vez por frame
    // base.Update() copia os conte�dos
    // de CharacterController : Update ()
    protected new void Update()
    {
        base.Update();

        // Calcula a dist�ncia ao player
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

    // Fun��o gen�rica de movimenta��o
    // isNotRetreat verifica se � aproxima��o
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
