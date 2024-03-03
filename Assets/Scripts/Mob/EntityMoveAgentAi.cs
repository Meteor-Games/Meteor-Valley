using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveAgentAi : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MAX_RANGE = 2f;
    public float backwardDistance = 0.5f; // Distância para recuar quando o mob encosta no jogador

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
                // Calcula a direção do jogador mais próximo
                Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;

                // Move o slime na direção do jogador mais próximo
                rb.MovePosition(rb.position + Entity.entityData.Value.moveSpeed * Time.fixedDeltaTime * direction);

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
