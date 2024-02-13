using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyControl : MonoBehaviour
{
    public Transform Enemy;
    public Transform EnemyHead;

    public Transform Player;
    public Transform PlayerNeck;

    public Vector3 MoveDirection;
    public Vector3 Axis;
    public float speed;
    public float angleSpeed;

    private float flySpeed = 50f;
    private float flyTime = 0f;
    private float HeadStdScale;


    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Awake()
    {
        HeadStdScale = GetStdScale(Enemy, EnemyHead);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < GameSys.DeathHeight)
        {
            gameObject.SetActive(false);
        }


        if (velocity.magnitude > 0.1f)
        {
            transform.Translate(velocity * Time.deltaTime, Space.World);
            if (Time.time - flyTime > 1f)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            transform.Translate(MoveDirection * speed * Time.deltaTime, Space.World);
            transform.Rotate(2f * Mathf.PI * Axis * angleSpeed * Time.deltaTime);
        }


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

    private int randomSign()
    {
        return Random.Range(0, 2) == 0 ? 1 : -1;
    }

}
