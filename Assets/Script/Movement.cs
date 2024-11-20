using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    const string IDLE = "Idle";
    const string WALK = "Walk";
    const string ATTACK = "BasicAttack";
    const string SKILL = "S1";
    const string ULTI = "S2";

    public bool test;
    CustomActions input;
    NavMeshAgent agent;
    Animator animator;
    
    // Movement
    [SerializeField] ParticleSystem clickEffect;
    [SerializeField] LayerMask clickableLayers;
    [SerializeField] float lookRotationSpeed = 8f;

    // Attack
    [SerializeField] float attackSpeed = 1.0f;
    [SerializeField] float attackDelay = 0.3f;
    [SerializeField] float attackDistance = 5f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] ParticleSystem hitEffect;

    // Skill Effects
    [SerializeField] float skillOneAttackIncrease = 0.02f; 
    [SerializeField] float skillOneHPLoss = 0.018f;       
    [SerializeField] float skillOneDuration = 5f;   
    [SerializeField] float skillTwoAOERadius = 30f; 
    [SerializeField] float skillTwoDamagePercentage = 0.5f;

    bool playerBusy = false;
    Interactable Target;

    Stat playerStats; 

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        input = new CustomActions();
        AssignInputs();
        playerStats = GetComponent<Stat>();
    }

    void AssignInputs()
    {
        input.Main.Move.performed += ctx => ClicktoMove();
    }

    void ClicktoMove()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                Target = hit.transform.GetComponent<Interactable>();
                if (clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
            else
            {
                Target = null;
                agent.destination = hit.point;
                if (clickEffect != null)
                {
                    Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                }
            }
        }
    }

    void OnEnable() => input.Enable();

    void OnDisable() => input.Disable();

    void Update()
    {
        FollowTarget();
        FaceTarget();
        SetAnimations();
    }

    void FollowTarget()
    {
        if (Target == null) return;

        if (Vector3.Distance(Target.transform.position, transform.position) <= attackDistance)
        {
            ReachDistance();
        }
        else
        {
            agent.SetDestination(Target.transform.position);
        }
    }

    void ReachDistance()
    {
        agent.SetDestination(transform.position);

        if (playerBusy) return;
        playerBusy = true;

        switch (Target.interactionType)
        {
            case InteractableTableType.Enemy:
            if(test == true){
                animator.Play(ATTACK);
                Invoke(nameof(SendAttack), attackDelay);
                Invoke(nameof(ResetBusyState), attackSpeed);
            }else{
                animator.Play(SKILL);
                Invoke(nameof(SendAttack), attackDelay);
                Invoke(nameof(ResetBusyState), attackSpeed);
            }
                break;

            case InteractableTableType.Item:
                Target.InteractWithItem();
                Target = null;
                Invoke(nameof(ResetBusyState), 0.5f);
                break;
        }
    }

    void SendAttack()
    {
        if (Target == null) return;

        if (Target.myActor.currentHealth <= 0)
        {
            Target = null;
            return;
        }

        Instantiate(hitEffect, Target.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Target.GetComponent<Stat>().TakeDamage(attackDamage);
    }

    void ResetBusyState()
    {
        playerBusy = false;
        SetAnimations();
    }

    void FaceTarget()
    {
        if (agent.destination == transform.position) return;

        Vector3 facing = Target != null ? Target.transform.position : agent.destination;
        Vector3 direction = (facing - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);
    }

    void SetAnimations()
    {
        if (playerBusy) return;

        if (agent.velocity == Vector3.zero)
        {
            animator.Play(IDLE);
        }
        else
        {
            animator.Play(WALK);
        }
    }

    public void ActivateSkillOne()
    {
        test = false;
        StartCoroutine(SkillOneCoroutine());
    }

IEnumerator SkillOneCoroutine(){
    int originalAttackDamage = attackDamage;
    attackDamage = Mathf.RoundToInt(attackDamage * (1 + skillOneAttackIncrease));

    float duration = 0f;

    while (duration < skillOneDuration)
    {
        float healthLoss = playerStats.maxHealth * skillOneHPLoss * Time.deltaTime;
        playerStats.currentHealth = playerStats.currentHealth - healthLoss;

        duration += Time.deltaTime;
        Debug.Log($"Skill active, duration: {duration}/{skillOneDuration}");

        yield return null;
    }
    attackDamage = originalAttackDamage;
    test = true;
    Debug.Log("Skill duration ended. Reverting attack damage to normal.");
}


    public void ActivateSkillTwo()
    {
        animator.Play(ULTI);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, skillTwoAOERadius, clickableLayers);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag("Interactable"))
            {
                Stat enemyStats = hit.GetComponent<Stat>();
                if (enemyStats != null)
                {
                    enemyStats.TakeDamage(Mathf.RoundToInt(attackDamage * skillTwoDamagePercentage));
                }
            }
        }
    }
}
