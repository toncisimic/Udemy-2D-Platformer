using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [Header("Patrol")]
    public Transform[] points;
    public float moveSpeed = 2f;
    public int currentPoint;

    [Header("Chase")]
    public Transform player;
    [Tooltip("Udaljenost na kojoj neprijatelj počinje chase")]
    public float distanceToAttackPlayer;
    [Tooltip("Koliko će biti veća udaljenost na kojoj neprijatelj odustaje od chase")]
    public float disengageMultiplier = 1.5f;
    public float chaseSpeed = 4f;

    [Header("Sprite")]
    public SpriteRenderer sr;

    // unutarnje stanje
    private bool isChasing = false;
    public float chasingTime = 2f, chasingCount;

    void Start()
    {
        // Odvojimo točke od roditelja (ako su bile child objekti)
        foreach (var point in points)
            point.parent = null;

        // Pretpostavljamo da PlayerController.instance već postoji
        if (PlayerController.instance != null)
            player = PlayerController.instance.transform;
        else
            Debug.LogWarning("Nemogu pronaći PlayerController.instance!");
    }

    void Update()
    {
        if (player == null) return;

       // Debug.Log($"Count: {chasingCount}, {chasingCount > 0}");
        if(chasingCount > 0) chasingCount -= Time.deltaTime;

        // 1) Izračun offseta između pivot-a i vizualnog centra
        Vector3 myCenter = sr.bounds.center;
        Vector3 pivotOffset = myCenter - transform.position;

        // 2) Centar igrača
        var playerSR = player.GetComponent<SpriteRenderer>();
        Vector3 playerCenter = (playerSR != null)
            ? playerSR.bounds.center
            : player.position;

        float dist = Vector3.Distance(myCenter, playerCenter);

        // 3) Ažuriranje stanja chase/non-chase kao prije...
        if (!isChasing && dist <= distanceToAttackPlayer && chasingCount <= 0) isChasing = true;
        else if (isChasing && dist > distanceToAttackPlayer * disengageMultiplier) isChasing = false;

        if (dist < 1.1f)
        {
            isChasing = false;
            chasingCount = chasingTime;
        }

        if (isChasing)
        {
            // 4) Cilj za pivot: pomaknuti ga tako da vizualni centar ide točno na playerCenter
            Vector3 targetPivotPos = playerCenter - pivotOffset;
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPivotPos,
                chaseSpeed * Time.deltaTime
            );

            // flip kao prije
            sr.flipX = transform.position.x + pivotOffset.x < playerCenter.x;
        }
        else
        {
            Patrol();
        }
    }

    private void ChasePlayer(float currentDistance)
    {
        // Idemo direktno na igrača
        transform.position = Vector3.MoveTowards(
            transform.position,
            player.position,
            chaseSpeed * Time.deltaTime
        );

        // Flip sprite prema igraču
        sr.flipX = transform.position.x < player.position.x;
    }

    private void Patrol()
    {
        // Kretanje po točkama
        transform.position = Vector3.MoveTowards(
            transform.position,
            points[currentPoint].position,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, points[currentPoint].position) < .05f)
        {
            currentPoint++;

            if (currentPoint >= points.Length) currentPoint = 0;
        }

        if (transform.position.x < points[currentPoint].position.x)
        {
            sr.flipX = true;
        }
        else if (transform.position.x > points[currentPoint].position.x)
        {
            sr.flipX = false;
        }
    }

    // Opcionalno: za debug, nacrtaj krug napada u editoru
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, distanceToAttackPlayer);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distanceToAttackPlayer * disengageMultiplier);
    }
}
