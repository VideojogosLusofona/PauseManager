using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;

public class OnCollision : MonoBehaviour
{
    new public Collider2D   collider;
    public bool             dontDealDamageIfInvulnerable;
    public string[]         tagsToCollide;
    public ContactFilter2D  contactFilter;
    public bool             directional;
    [ShowIf("directional")]
    public Vector2          direction;
    public float            cooldown;
    public UnityEvent[]     additionalEvents;

    class BlacklistElem
    {
        public GameObject   topLevelObject;
        public float        cooldown;
    };

    Collider2D[]        results;
    List<BlacklistElem> blacklist;
    Vector3             prevPos;
    Health              selfHealth;

    void Start()        
    {
        if (collider == null) collider = GetComponent<Collider2D>();

        selfHealth = GetComponent<Health>();

        results = new Collider2D[16];
        blacklist = new List<BlacklistElem>();

        prevPos = transform.position;
    }

    void Update()
    {
        blacklist.ForEach((e) => e.cooldown -= Time.deltaTime);
        blacklist.RemoveAll((e) => e.cooldown <= 0);

        bool collisionEnabled = true;
        if ((dontDealDamageIfInvulnerable) && (selfHealth))
        {
            collisionEnabled &= !selfHealth.isInvulnerable;
        }

        if (collisionEnabled)
        {
            int n = Physics2D.OverlapCollider(collider, contactFilter, results);
            if (n > 0)
            {
                foreach (var c in results)
                {
                    if (c == null) continue;

                    if (tagsToCollide.Length > 0)
                    {
                        if (System.Array.IndexOf(tagsToCollide, c.tag) == -1) continue;
                    }

                    if (directional)
                    {
                        Vector2 moveDir = (transform.position - prevPos).normalized;

                        if (Vector2.Dot(moveDir, direction) <= 0) continue;
                    }

                    GameObject topLevelObject = c.gameObject.GetRootObject();

                    if (topLevelObject)
                    {
                        if (blacklist.Find((e) => e.topLevelObject == topLevelObject) != null) continue;

                        ThrowCollisionEvent(c);
                        
                        BlacklistElem belem = new BlacklistElem
                        {
                            topLevelObject = topLevelObject,
                            cooldown = cooldown
                        };

                        blacklist.Add(belem);
                    }

                    foreach (var evt in additionalEvents)
                    {
                        evt.Invoke();
                    }
                }
            }
        }

        prevPos = transform.position;
    }

    protected virtual void ThrowCollisionEvent(Collider2D objCollider)
    {
    }
}
