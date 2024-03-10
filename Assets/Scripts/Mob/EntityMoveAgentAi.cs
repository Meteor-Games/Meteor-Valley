using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveAgentAi : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MAX_RANGE = 2f;
    public float backwardDistance = 0.5f; // Distância para recuar quando o mob encosta no jogador
    public float distanceToMaintain = 1.5f; // Distância a ser mantida do jogador
    public bool isWithinMaintainDistance = false; // Variável para indicar se está dentro da distância de manutenção

    private Entity Entity;
    private GameObject targetPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Entity = GetComponent<Entity>();
        StartCoroutine(FollowClosestPlayer());
    }

    private IEnumerator FollowClosestPlayer()
    {
        while (true)
        {
            // Encontra o jogador mais próximo
            GameObject newTargetPlayer = FindClosestPlayer();

            // Verifica se há um novo jogador mais próximo e o atual é diferente do novo
            if (newTargetPlayer != null && newTargetPlayer != targetPlayer)
            {
                targetPlayer = newTargetPlayer;
                isWithinMaintainDistance = false; // Reinicia a variável ao trocar de jogador
            }
            else
            {
                isWithinMaintainDistance = false; // Define como false se nenhum jogador próximo for encontrado
            }

            // Aguarda antes de encontrar o próximo jogador mais próximo
            yield return new WaitForSeconds(1f);
        }
    }

    private GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if (distance < closestDistance)
            {
                closestPlayer = player;
                closestDistance = distance;
            }
        }

        return closestPlayer;
    }

    private void FixedUpdate()
    {
        if (targetPlayer != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.transform.position);
            if (distanceToPlayer <= MAX_RANGE)
            {
                // Verifica se está dentro da distância de manutenção
                if (distanceToPlayer <= distanceToMaintain)
                {
                    isWithinMaintainDistance = true;
                    return; // Para o método FixedUpdate aqui se estiver dentro da distância de manutenção
                }
                else
                {
                    isWithinMaintainDistance = false;
                }

                // Calcula a direção do jogador mais próximo
                Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;

                // Move o slime na direção do jogador mais próximo
                rb.MovePosition(rb.position + Entity.EntityData.moveSpeed * Time.fixedDeltaTime * direction);

                // Verifica se o mob está muito próximo do jogador
                if (distanceToPlayer <= backwardDistance)
                {
                    // Calcula a direção oposta para recuar
                    Vector2 backwardDirection = -direction;

                    // Move o slime um pouco para trás
                    rb.MovePosition(rb.position + backwardDistance * backwardDirection);
                }
            }
        }
    }
}
