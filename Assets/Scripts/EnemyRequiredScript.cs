using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EnemyManager;

public class EnemyRequiredScript : MonoBehaviour
{
    public static System.Action OnRequestAmount;
    public static int enemyCounter = 0;
    public TMP_Text enemyNumber;

    public Animator anim;

    void Start()
    {
        enemyCounter = 0;
        OnRequestAmount?.Invoke();
        enemyNumber.text = enemyCounter.ToString();
    }

    void OnEnable()
    {       
        EnemyMediumBT.OnAwake += AddToCounter;
        EnemyHealthManager.OnEnemyDeath += TakeFromCounter;
    }

    void OnDisable()
    {
        EnemyMediumBT.OnAwake -= AddToCounter;
        EnemyHealthManager.OnEnemyDeath -= TakeFromCounter;
    }


    void AddToCounter()
    {
        enemyCounter ++;
        enemyNumber.text = enemyCounter.ToString();
    }

    void TakeFromCounter()
    {
        enemyCounter --;
        enemyNumber.text = enemyCounter.ToString();
        IfNoMoreEnemies();
    }

    void IfNoMoreEnemies()
    {
        if(enemyCounter <= 0)
        {
            anim.Play("Open");
        }
    }


}
