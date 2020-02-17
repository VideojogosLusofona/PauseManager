using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIJump : MonoBehaviour
{
    public LayerMask characterLayer;
    public float     cooldown = 0.5f;

    class BlacklistElem
    {
        public EnemyController  enemy;
        public float            cooldown;
    };

    new Collider2D          collider;
    Collider2D[]            results;
    ContactFilter2D         contactFilter;
    List<BlacklistElem>     blacklist;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();

        contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(characterLayer);

        results = new Collider2D[16];

        blacklist = new List<BlacklistElem>();
    }

    // Update is called once per frame
    void Update()
    {
        blacklist.ForEach((e) => e.cooldown -= Time.deltaTime);
        blacklist.RemoveAll((e) => e.cooldown <= 0);

        int n = Physics2D.OverlapCollider(collider, contactFilter, results);
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
                        float vel = Mathf.Abs(rb.velocity.y);
                        if (vel > 0.01f) continue;
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
}
