using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int hp = 100;
    public int backstabMultiplier = 2;
    public Canvas enemyCanvas;
    public TMP_Text enemyText;
    public Animator animator;

    bool canTakeDamage = true;

    public void Start()
    {
        enemyText.SetText("Health: " + hp.ToString());
    }

    public void TakeDamage(int damageValue, bool critical)
    {
        if (!canTakeDamage)
        {
            return;
        }

        if (critical)
        {
            hp -= damageValue * backstabMultiplier;
        }
        else
        {
            hp -= damageValue;
        }



        if (hp <= 0)
        {
            Die();
            hp = 0;
        }

        enemyText.SetText("Health: " + hp.ToString());
        animator.SetTrigger("Hit");
    }

    void Die()
    {
        animator.SetTrigger("Die");
        canTakeDamage = false;
    }
}
