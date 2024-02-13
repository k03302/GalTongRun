using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    public Transform Enemy;
    public Transform EnemyHead;
    public Transform FixObject;

    public Transform Player;
    public Transform PlayerNeck;

    public Transform Sensor;

    public bool AutoColliderResize = true;


    private SensorControl SensorCon;
    private float ColliderLength;

    private float flySpeed = 50f;
    private float flyTime = 0f;
    private int isAttackCount = 0;
    private Animator animator;
    private float HeadStdScale;

    private Vector3 velocity = Vector3.zero;
    

    // Start is called before the first frame update
    void Start()
    {
        HeadStdScale = GetStdScale(Enemy, EnemyHead);
        animator = GetComponentInChildren<Animator>();
        //Sensor.transform.Translate(Vector3.forward * HeadStdScale * 0.5f);
        SensorCon = Sensor.GetComponent<SensorControl>();

        if(AutoColliderResize)
        {
            BoxCollider collider1 = GetComponent<BoxCollider>();
            BoxCollider collider2 = Sensor.GetComponent<BoxCollider>();

            ColliderLength = 2f + HeadStdScale / 3f;
            collider1.center = new Vector3(0f, 1f, (ColliderLength - 2f) / 2f);
            collider1.size = new Vector3(2f, 2f, ColliderLength);
            collider2.center = new Vector3(0f, 1f, (ColliderLength - 1f) / 2f);
            collider2.size = new Vector3(2f, 2f, ColliderLength + 1f);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (animator.GetBool("isAttack"))
        {
            if(isAttackCount > 1)
            {
                animator.SetBool("isAttack", false);
                isAttackCount = 0;
            }
            else
            {
                isAttackCount++;
            }
        }


        if(SensorCon.PlayerEntered())
        {
            animator.SetBool("isAttack", true);
            isAttackCount = 0;
        }


        if (velocity.magnitude > 0.1f)
        {
            transform.Translate(velocity * Time.deltaTime);
            if (Time.time - flyTime > 1f)
            {
                gameObject.SetActive(false);
            }
        }


        FixAnimator(FixObject);
    }



    public void SetFlying()
    {
        velocity = new Vector3(Random.Range(0.3f, 1.0f) * randomSign(), Random.Range(0.3f, 1.0f), 0.0f);
        velocity = velocity.normalized * flySpeed;
        flyTime = Time.time;
    }

    public bool isPlayerHeadBigger()
    {
        //Debug.Log(new Vector2(GetStdScale(Player, PlayerNeck), HeadStdScale));
        return GetStdScale(Player, PlayerNeck) >= HeadStdScale;
    }

    private float GetStdScale(Transform Parent, Transform Target)
    {
        float scale = Target.transform.lossyScale.x;

        Transform Avatar = Parent.GetComponentInChildren<Animator>().transform;
        scale /= Avatar.transform.localScale.x;

        Transform Armature = Avatar.Find("Armature");
        scale /= Armature.transform.localScale.x;

        return scale;
    }

    private void FixAnimator(Transform Fix)
    {
        animator.transform.position = Fix.transform.position;
        animator.transform.rotation = Fix.transform.rotation;
    }

    private int randomSign()
    {
        return Random.Range(0, 2)==0? 1 : -1;
    }
}
