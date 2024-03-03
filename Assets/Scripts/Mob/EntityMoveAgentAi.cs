using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMoveAgentAi : MonoBehaviour
{
    public Rigidbody2D rb;
    public float MAX_RANGE = 2f;
    public float backwardDistance = 0.5f; // Dist�ncia para recuar quando o mob encosta no jogador

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
            // Encontra o jogador mais pr�ximo
            GameObject newTargetPlayer = FindClosestPlayer();

            // Verifica se h� um novo jogador mais pr�ximo e o atual � diferente do novo
            if (newTargetPlayer != null && newTargetPlayer != targetPlayer)
            {
                targetPlayer = newTargetPlayer;
            }

            // Aguarda antes de encontrar o pr�ximo jogador mais pr�ximo
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
                // Calcula a dire��o do jogador mais pr�ximo
                Vector2 direction = (targetPlayer.transform.position - transform.position).normalized;

                // Move o slime na dire��o do jogador mais pr�ximo
                rb.MovePosition(rb.position + Entity.entityData.Value.moveSpeed * Time.fixedDeltaTime * direction);

                // Verifica se o mob est� muito pr�ximo do jogador
                if (distanceToPlayer <= backwardDistance)
                {
                    // Calcula a dire��o oposta para recuar
                    Vector2 backwardDirection = -direction;

                    // Move o slime um pouco para tr�s
                    rb.MovePosition(rb.position + backwardDistance * backwardDirection);
                }
            }
        }
    }
}
