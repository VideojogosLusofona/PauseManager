using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class AIJump : MonoBehaviour
{
    public LayerMask characterLayer;
    public float     radius = 6;
    public float     cooldown = 0.5f;
    public bool      directional = false;

    class BlacklistElem
    {
        public EnemyController  enemy;
        public float            cooldown;
    };

    Collider2D[]            results;
    List<BlacklistElem>     blacklist;

    // Start is called before the first frame update
    void Start()
    {
        results = new Collider2D[16];

        blacklist = new List<BlacklistElem>();
    }

    // Update is called once per frame
    void Update()
    {
        blacklist.ForEach((e) => e.cooldown -= Time.deltaTime);
        blacklist.RemoveAll((e) => e.cooldown <= 0);

        int n = Physics2D.OverlapCircleNonAlloc(transform.position, radius, results, characterLayer);
        if (n > 0)
        {
            foreach (var c in results)
            {
                if (c == null) continue;

                EnemyController enemy = c.GetComponent<EnemyController>();
                if (enemy)
                {
                    if (blacklist.Find((e) => e.enemy == enemy) != null) continue;

                    GroundDetection gd = c.GetComponent<GroundDetection>();
                    if (gd)
                    {
                        if (!gd.isGrounded) continue;
                    }

                    Rigidbody2D rb = c.GetComponent<Rigidbody2D>();
                    if (rb)
                    {
                        var   velocity = rb.velocity;
                        float absVelY = Mathf.Abs(velocity.y);
                        if (absVelY > 0.01f) continue;
                        if (directional)
                        {
                            Vector2 direction = transform.right;
                            if (Vector2.Dot(direction, velocity) < 0) continue;
                        }
                    }

                    BlacklistElem belem = new BlacklistElem
                    {   
                        enemy = enemy,
                        cooldown = cooldown
                    };

                    blacklist.Add(belem);

                    JumpMover jump = c.GetComponent<JumpMover>();
                    jump.Jump();
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);

        if (directional)
        {
            Gizmos.color = new Color(1, 0.5f, 0.0f, 1.0f);
            Vector3 p = transform.position + transform.right * radius * 1.5f;
            Gizmos.DrawLine(transform.position, p);
            Gizmos.DrawLine(p, p - transform.right * radius * 0.25f + transform.up * radius * 0.125f);
            Gizmos.DrawLine(p, p - transform.right * radius * 0.25f - transform.up * radius * 0.125f);
        }
    }
}
