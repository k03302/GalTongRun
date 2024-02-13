using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour
{
    public Transform cam;
    public Transform Neck;
    public Transform OtherHead;
    public Animator animator;
    public CharacterController character;
    private float NeckScale;

    public float InterruptingNeckScale;
    public float NonInterruptingNeckScale;
    private bool InterruptingState = false;

    public float BlockingNeckScale;
    public float NonBlockingNeckScale;
    private bool BlockingState = false;

    public AudioSource audioSource;
    public AudioClip audioDie;
    public AudioClip audioEat;
    public AudioClip audioHit;
    public AudioClip audioEww;
    public AudioClip audioFall;

    public ParticleSystem HambergerParticleSystem;
    public ParticleSystem BroccoliParticleSystem;
    public ParticleSystem MealWormParticleSystem;


    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDistance = 0.4f;

    public float camHeight;
    


    private HeadAni headAni;




    private float speed = 10.0f;
    private float controllSpeed = 18f;
    private float fallingControllSpeed = 30f;
    private float jumpHeight = 4.0f;

    private int IsHitWallCount = 0;
    private int RollingModeCount = 0;
    
    
    private float gravity = -9.8f;
    private bool isGrounded = false;
    private Vector3 velocity;
    private Vector3 sideMove;

    private bool isFirstPenalty = true;
    private bool isFinish = false;
    public bool isRespawn = false;

    private PoleInfo CurrentPoleInfo;
    private HoleInfo CurrentHoleInfo;
    private int PoleIndex;
    private bool RollingMode = false;
    private bool RollingModeTurnOff = false;
    private bool JumpSignal = false;
    


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        HambergerParticleSystem.gameObject.SetActive(false);
        BroccoliParticleSystem.gameObject.SetActive(false);
        MealWormParticleSystem.gameObject.SetActive(false);
    }

    void Start()
    {
        OtherHead.gameObject.SetActive(false);
        character = GetComponent<CharacterController>();
        headAni = OtherHead.GetComponent<HeadAni>();

        UpdateNeckScale();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateNeckScale();


        if(transform.position.y < GameSys.DeathHeight)
        {
            isRespawn = true;
        }



        if (isGrounded && !RollingMode)
        {
            velocity.y = -2.0f;
        }








        // RollingMode일 때
        if (RollingMode)
        {
            speed = 7.0f;
            sideMove = Vector3.zero;
            if (CurrentPoleInfo.isScaleOver(OtherHead.localScale[0]))
            {
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            }
            else
            {
                isGrounded = false;
            }


            if(isGrounded)
            {
                velocity.y = 0f;
            }
            else
            {
                velocity.y += gravity * Time.deltaTime;
            }


            if (CurrentPoleInfo.isOnPoleX(transform.position.x, PoleIndex))
            {
                velocity.x = 0f;
            }

            if(JumpSignal)
            {
                JumpSignal = false;
                velocity.y = Mathf.Sqrt(2 * jumpHeight * -gravity);
            }
        }
        else
        {

            // 애니메이션마다 지정
            var aniInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (aniInfo.IsName("FallOver"))
            {
                speed = -5.0f;
                sideMove = Vector3.zero;
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                velocity.y += gravity * Time.deltaTime;
            }
            else if (aniInfo.IsName("Running"))
            {
                speed = 7.0f;
                sideMove = transform.right * Input.GetAxis("Horizontal") * controllSpeed;
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                velocity.y += gravity * Time.deltaTime;
            }
            else if (aniInfo.IsName("Stand up"))
            {
                speed = 0.0f;
                sideMove = Vector3.zero;
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                velocity.y += gravity * Time.deltaTime;
            }
            else if (aniInfo.IsName("Falling"))
            {
                speed = 0.0f;
                sideMove = transform.right * Input.GetAxis("Horizontal") * fallingControllSpeed;
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                velocity.y += gravity * Time.deltaTime;
            }
            else
            {
                speed = 0.0f;
                sideMove = Vector3.zero;
                isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
                velocity.y += gravity * Time.deltaTime;
            }

            velocity.y += gravity * Time.deltaTime;
        }



        character.Move((transform.forward * speed + sideMove + velocity) * Time.deltaTime);





        // 카운트를 세서 끄기
        if (animator.GetBool("IsHitWall"))
        {
            if (IsHitWallCount > 2)
            {
                animator.SetBool("IsHitWall", false);
                IsHitWallCount = 0;
            }
            IsHitWallCount++;
        }
        if(RollingModeTurnOff)
        {
            if(RollingModeCount > 2)
            {
                RollingModeTurnOff = false;
                RollingMode = false;
                RollingModeCount = 0;
            }
            RollingModeCount++;
        }




        // 일어설 때에는 FixAnimator를 호출하지 않는다

        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Stand Up"))
        {
            FixAnimator();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //아이템을 먹었을 때
        if (other.gameObject.CompareTag("Hamberger") ||
            other.gameObject.CompareTag("MealWorm"))
        {
            if (other.gameObject.CompareTag("Hamberger"))
            {
                PlaySound("EAT");

                PlayParticleSys(HambergerParticleSystem);
                other.gameObject.SetActive(false);
                if(animator.GetBool("IsFalling"))
                {
                    ResizeWithoutAnimation(true);
                }
                else
                {
                    StartResizeAnimation(true);
                }
                
            }
            else
            {
                PlaySound("EAT");
                if (isFirstPenalty)
                {
                    isFirstPenalty = false;
                    PlaySound("EWW");
                }

                PlayParticleSys(MealWormParticleSystem);
                other.gameObject.SetActive(false);
                if (animator.GetBool("IsFalling"))
                {
                    ResizeWithoutAnimation(false);
                }
                else
                {
                    StartResizeAnimation(false);
                }
            }

        }

        else if (other.gameObject.CompareTag("FallArea"))
        {
            EndResizeAnimation();
            animator.SetBool("IsFalling", true);

            PlaySound("FALL");
        }
        //아이템 상호작용 외
        else if (other.gameObject.CompareTag("Wall"))
        {
            //먼저 머리 회전 애니메이션을 중지
            EndResizeAnimation();
            //머리회전 애니메이션 없이 리사이즈
            ResizeWithoutAnimation(false);

            PlaySound("HIT");

            animator.SetBool("IsHitWall", true);
        }
        else if (other.gameObject.CompareTag("Pole"))
        {
            //먼저 머리 회전 애니메이션을 중지
            EndResizeAnimation();

            CurrentPoleInfo = other.GetComponent<PoleInfo>();
            PoleIndex = CurrentPoleInfo.GetMinXDistanceIndex(transform.position.x);
            velocity.x = 5.0f * CurrentPoleInfo.GetXAdjustSign(transform.position.x, PoleIndex);

            SetJumpSignal(4.0f);
            StartRollingAnimation();
        }
        /*
        else if(other.gameObject.CompareTag("Hole"))
        {
            //먼저 머리 회전 애니메이션을 중지
            EndResizeAnimation();


            CurrentHoleInfo = other.GetComponent<HoleInfo>();
            velocity += CurrentHoleInfo.GetAdustVector(transform);

            SetJumpSignal(CurrentHoleInfo.GetJumpHeight(transform));
            StartRollingAnimation();
        }
        */
        else if (other.gameObject.CompareTag("Enemy"))
        {
            //먼저 머리 회전 애니메이션을 중지
            EndResizeAnimation();

            if (!other.GetComponent<EnemyControl>().isPlayerHeadBigger())
            {
                //머리회전 애니메이션 없이 리사이즈
                ResizeWithoutAnimation(false);
                animator.SetBool("IsHitWall", true);
            }
            else
            {
                other.GetComponent<EnemyControl>().SetFlying();
            }

            PlaySound("HIT");
        }
        else if(other.gameObject.CompareTag("MovingEnemy"))
        {
            EndResizeAnimation();

            if(!other.GetComponent<MovingEnemyControl>().isPlayerHeadBigger())
            {
                ResizeWithoutAnimation(false);
                animator.SetBool("IsHitWall", true);
            }
            else
            {
                other.GetComponent<MovingEnemyControl>().SetFlying();
            }

            PlaySound("HIT");
        }


        else if (other.gameObject.CompareTag("Finish"))
        {
            isFinish = true;
        }
        else if (other.gameObject.CompareTag("Respawn"))
        {
            PlaySound("DIE");
        }
        else if(other.gameObject.CompareTag("Wind"))
        {
            velocity += other.GetComponent<WindInfo>().GetWindVector();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Pole"))
        {

            if (CurrentPoleInfo.isScaleOver(OtherHead.localScale[0]))
            {
                JumpSignal = true;
            }

            EndRollingAnimation();
        }
        /*
        else if (other.gameObject.CompareTag("Hole"))
        {
            EndRollingAnimation();
        }
        */
        else if(other.gameObject.CompareTag("Respawn"))
        {
            isRespawn = true; 
        }
        else if (other.gameObject.CompareTag("Wind"))
        {
            velocity -= other.GetComponent<WindInfo>().GetWindVector();
        }
    }



    private void SetJumpSignal(float height)
    {
        JumpSignal = true;
        jumpHeight = height;
    }

    private void StartRollingAnimation()
    {
        RollingMode = true;
        OtherHead.gameObject.SetActive(true);
        animator.gameObject.SetActive(false);
        headAni.SetRolling(true);
    }
    private void EndRollingAnimation()
    {
        OtherHead.gameObject.SetActive(false);
        animator.gameObject.SetActive(true);
        headAni.SetRolling(false);
        RollingModeTurnOff = true;
    }



    private void StartResizeAnimation(bool isIncrease)
    {
        Neck.localScale = Vector3.zero;

        headAni.Restart((isIncrease)? 1 : -1);
        OtherHead.gameObject.SetActive(true);
    }
    private void EndResizeAnimation()
    {
        headAni.FinishAnimation();
    }
    private void ResizeWithoutAnimation(bool isIncrease)
    {
        headAni.ResizeWithoutAnimation(isIncrease);
        
    }


    private void FixAnimator()
    {
        animator.transform.position = transform.position;
        animator.transform.rotation = transform.rotation;
    }

    private void PlayParticleSys(ParticleSystem particleSystem)
    {
        particleSystem.gameObject.SetActive(true);
        particleSystem.Clear();
        particleSystem.Play();
    }





    public bool IsFinished()
    {
        return isFinish;
    }



    public float GetHeadSize()
    {
        if(Neck.transform.localScale.x > 0f)
        {
            return Neck.transform.localScale.x;
        }
        else
        {
            return OtherHead.transform.localScale.x / 2f;
        }
    }



    public void PlaySound(string action)
    {
        AudioClip audioClip = null;
        float volume = 0.0f;
        switch (action)
        { 
            case "DIE":
                audioClip = audioDie;
                volume = 1.0f;
                break;
            case "EAT":
                audioClip = audioEat;
                volume = 0.3f;
                break;
            case "HIT":
                audioClip = audioHit;
                volume = 0.3f;
                break;
            case "EWW":
                audioClip = audioEww;
                volume = 0.4f;
                break;
            case "FALL":
                audioClip = audioFall;
                volume = 0.5f;
                break;

        }
        if (audioClip != null)
        {
            audioSource.PlayOneShot(audioClip, volume);
            
        }
    }

    public bool IsFallingState()
    {
        return animator.GetBool("IsFalling");
    }
    public float GetNeckScale()
    {
        return Neck.localScale.x;
    }

    public bool IsNeckScaleInterrupting()
    {
        return InterruptingState;
    }
    public bool IsNeckScaleBlocking()
    {
        return BlockingState;
    }

    private void UpdateNeckScale()
    {
        if(GetNeckScale() < 0.1f)
        {
            return;
        }
        else
        {
            if (InterruptingState)
            {
                if (GetNeckScale() < NonInterruptingNeckScale)
                {
                    InterruptingState = false;
                }
            }
            else
            {
                if (GetNeckScale() > InterruptingNeckScale)
                {
                    InterruptingState = true;
                }
            }

            if(BlockingState)
            {
                if(GetNeckScale() < NonBlockingNeckScale)
                {
                    BlockingState = false;
                }
            }
            else
            {
                if(GetNeckScale() > BlockingNeckScale)
                {
                    BlockingState = true;
                }
            }
            NeckScale = Neck.localScale.x;
        }
        
    }
    /*
    public bool isFrontNotShowing()
    {
        if(Neck)
    }
    */
}
