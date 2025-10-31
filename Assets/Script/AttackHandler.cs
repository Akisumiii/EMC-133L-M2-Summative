using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AttackHandler : MonoBehaviour
{
    public Animator characterAnimator;
    public Animator cameraAnimator;
    public Transform enemyTransform;

    [Header("Raycast Settings")]
    public Camera playerCamera;   
    public float rayDistance = 10f;     
    public LayerMask hitLayers;

    bool canAttack = true;

    public TMP_Text feedbackText;

    private void Start()
    {
        feedbackText.SetText("");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

    }

    public void Attack()
    {

        if (!canAttack)
        {
            return;
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, hitLayers))
        {

            if(hit.transform.gameObject != null) {
                Vector3 dirFromEnemyToPlayer = (transform.position - hit.collider.transform.parent.transform.position).normalized;
                float dot = Vector3.Dot(hit.collider.transform.parent.forward, dirFromEnemyToPlayer);

                if (dot < -0.7f)
                {
                    characterAnimator.SetTrigger("Stab");
                    cameraAnimator.SetTrigger("Shake");
                    hit.collider.transform.parent.GetComponent<EnemyHealth>().TakeDamage(20, true);
                    feedbackText.SetText("Backstabbed Enemy");
                }
                else
                {
                    characterAnimator.SetTrigger("Attack");
                    cameraAnimator.SetTrigger("Shake");
                    hit.collider.transform.parent.GetComponent<EnemyHealth>().TakeDamage(10, false);
                    feedbackText.SetText("Hit Enemy with Basic Attack");
                }
            }

        }

        StartCoroutine(WaitForAttacktoFinish());

    }

    IEnumerator WaitForAttacktoFinish()
    {

       canAttack = false;
        yield return new WaitForSeconds(.5f);

        canAttack = true;
        feedbackText.SetText("");
    }

}
