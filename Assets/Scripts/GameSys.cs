using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;






public class GameSys : MonoBehaviour
{
    public static int DeathCount = 0;
    public static int ReplayCount = 0;
    public static float StartTime;


    public static int SceneNumber = 1;
    
    public static int MaxSceneNumber = 6;
    public static float DeathHeight = -20f;

    public Transform Player;
    public Transform Fighter1;
    public Transform Fighter2;


    public Transform PlayerNeck;
    public Transform Fighter1Neck;
    public Transform Fighter2Head;

    public ParticleSystem HitParticle1;
    public ParticleSystem HitParticle2;

    public Transform CameraView1;
    public Transform CameraView2;

    public Canvas endScreen;
    public Canvas startScreen;

    public Camera camera;
    public Vector3 DefaultStateAdjustVector;
    public Vector3 DefaultStateRotateVector;
    public Vector3 FallingStateAdjustVector;
    public Vector3 FallingStateRotateVector;
    public Vector3 InterruptingStateAdjustVector;
    public Vector3 InterruptingStateRotateVector;
    public Vector3 BlockingStateAdjustVector;
    public Vector3 BlockingStateRotateVector;
    private MoveCamera moveCamera;


    private Vector3 distanceVector;
    private float initPlayerHeight;
    private static bool StartMode = true;
    private bool PlayerMode = true;
    private bool isFighter1Win = true;
    private bool isFirst = true;


    private StepList steplist;


    public static bool isStartMode()
    {
        return StartMode;
    }

    public static void SetDifficulty(bool isEasy) {
        if(isEasy)
        {
            SceneNumber = 8;
        }
        else
        {
            SceneNumber = 1;
        }
    }
    public static void InitalizeScores()
    {
        StartTime = Time.time;
        DeathCount = 0;
        ReplayCount = 0;
    }

    private void PlayParticleSys(ParticleSystem particleSystem)
    {
        particleSystem.gameObject.SetActive(true);
        particleSystem.Clear();
        particleSystem.Play();
    }


    private float GetStdScale(Transform Parent, Transform Target)
    {
        float scale = Target.transform.lossyScale.x;

        Transform Avatar = Parent.GetChild(0);
        scale /= Avatar.transform.localScale.x;

        Transform Armature = Avatar.GetChild(0);
        scale /= Armature.transform.localScale.x;

        return scale;
    }



    private void Awake()
    {


        string[] Step = new string[6];
        Step[0] = "CameraMove1";
        Step[1] = "Rest1";
        Step[2] = "Fighter1Attack";
        Step[3] = "CameraMove2";
        Step[4] = "Rest1";
        Step[5] = "Fighter2Attack";
        steplist = new StepList(Step);
    }


    private void SetFighterPosition()
    {
        Vector3 FighterPositionCenter = (Fighter1.position + Fighter2.position) / 2f;
        Vector3 FighterDistanceVector = Fighter1.position - Fighter2.position;

        float Fighter1HeadScale = GetStdScale(Fighter1, Fighter1Neck);
        float Fighter2HeadScale = GetStdScale(Fighter2, Fighter2Head);
        float StandardHeadScaleSum = 8f;

        float distanceFactor = (Fighter1HeadScale + Fighter2HeadScale) / StandardHeadScaleSum;
        Fighter1.position = FighterPositionCenter + FighterDistanceVector * distanceFactor / 2f;
        Fighter2.position = FighterPositionCenter - FighterDistanceVector * distanceFactor / 2f;
    }


    // Start is called before the first frame update
    void Start()
    {
        float[] distances = new float[32];
        distances[8] = 60f;
        distances[9] = 60f;
        camera.layerCullDistances = distances;


        float camHeight =  camera.transform.position.y - Player.position.y;
        distanceVector = new Vector3(0.0f, camHeight, -4.0f);
        initPlayerHeight = Player.position.y;



        Fighter1.gameObject.SetActive(false);
        startScreen.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        

        Cursor.visible = false;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            ReplayCount++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


        if (StartMode)
        {
            Time.timeScale = 0.0f;
            startScreen.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.S))
            {
                StartMode = false;
                startScreen.gameObject.SetActive(false);
                camera.GetComponent<CameraControl>().PlaySound();
                Time.timeScale = 1.0f;
            }
        }
        else if (PlayerMode)
        {
            PlayerControl PlayerCon = Player.GetComponent<PlayerControl>();
            if (PlayerCon.isRespawn)
            {
                DeathCount++;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (PlayerCon.IsFinished())
            {
                Fighter1.gameObject.SetActive(true);
                Fighter1Neck.localScale = PlayerNeck.localScale;
                Player.gameObject.SetActive(false);
                PlayerMode = false;
            }


            Vector3 AdjustVector = DefaultStateAdjustVector;
            Vector3 RotateVector = DefaultStateRotateVector;

            if(PlayerCon.IsFallingState())
            {

                


                AdjustVector = FallingStateAdjustVector;
                RotateVector = FallingStateRotateVector;
            }
            else
            {



                if (PlayerCon.IsNeckScaleInterrupting())
                {
                    if (PlayerCon.IsNeckScaleBlocking())
                    {
                        AdjustVector = BlockingStateAdjustVector;
                        RotateVector = BlockingStateRotateVector;
                    }
                    else
                    {
                        AdjustVector = InterruptingStateAdjustVector;
                        RotateVector = InterruptingStateRotateVector;
                    }
                }
            }




            if(Player.position.y - initPlayerHeight >= -0.1f)
            {
                camera.transform.position = Player.position + distanceVector + AdjustVector;
                camera.transform.localEulerAngles = RotateVector;
            }
            else
            {
                camera.transform.position = new Vector3(Player.position.x, initPlayerHeight, Player.position.z) + distanceVector + AdjustVector;
                camera.transform.localEulerAngles = RotateVector;
            }
        }
        else
        {
            string currentStep = steplist.GetCurrentStep();

            if (isFirst)
            {
                Fighter1.gameObject.SetActive(true);
                isFighter1Win = GetStdScale(Fighter1, Fighter1Neck) >= GetStdScale(Fighter2, Fighter2Head);
                //Debug.Log(new Vector2(GetStdScale(Fighter1, Fighter1Neck), GetStdScale(Fighter2, Fighter2Head)));
                SetFighterPosition();
                isFirst = false;
            }

           

            


            if (currentStep == "CameraMove1")
            {





                if (moveCamera == null || steplist.isFirst[0])
                {
                    moveCamera = new MoveCamera(camera.transform, CameraView1.position, CameraView1.eulerAngles);
                    steplist.isFirst[0] = false;
                }

                moveCamera.Translate();
                if (moveCamera.IsFinished() && steplist.HasNextStep())
                {
                    steplist.NextStep();
                }





            }
            else if (currentStep == "CameraMove2")
            {



                if (moveCamera == null || steplist.isFirst[0])
                {
                    moveCamera = new MoveCamera(camera.transform, CameraView2.position, CameraView2.eulerAngles);
                    steplist.isFirst[0] = false;
                }

                moveCamera.Translate();
                if (moveCamera.IsFinished() && steplist.HasNextStep())
                {
                    steplist.NextStep();
                }



            }
            else if (currentStep == "Fighter1Attack")
            {
                Animator attacker, defensor;
                attacker = Fighter1.GetComponentInChildren<Animator>();
                defensor = Fighter2.GetComponentInChildren<Animator>();

                bool isAttackerWin = isFighter1Win;

                if (steplist.isFirst[0])
                {
                    steplist.isFirst[0] = false;
                    attacker.SetBool("Attacking", true);
                }



                else if (steplist.isFirst[1] &&
                    attacker.GetBool("Attacking") &&
                    attacker.GetCurrentAnimatorStateInfo(0).IsName("HeadButt") &&
                    attacker.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
                {
                    steplist.isFirst[1] = false;
                    defensor.SetBool("Attacked", true);
                    attacker.SetBool("Attacking", false);
                    defensor.SetBool("IsLost", isAttackerWin);
                    attacker.SetBool("IsWin", isAttackerWin);

                    Fighter1.GetComponent<FighterControl>().PlaySound("HIT");

                    PlayParticleSys(HitParticle2);
                }


                else if (steplist.isFirst[2] &&
                    defensor.GetBool("Attacked") && !defensor.GetBool("IsLost") &&
                    defensor.GetCurrentAnimatorStateInfo(0).IsName("HeadHit") &&
                    defensor.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    steplist.isFirst[2] = false;
                    defensor.SetBool("Attacked", false);
                    steplist.NextStep();
                }

                else if (steplist.isFirst[2] &&
                    defensor.GetBool("Attacked") && defensor.GetBool("IsLost") &&
                    defensor.GetCurrentAnimatorStateInfo(0).IsName("Flying Back Death") &&
                    defensor.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    steplist.isFirst[2] = false;
                    Fighter1.GetComponent<FighterControl>().PlaySound("WIN");
                    steplist.Finish();
                }


            }

            else if (currentStep == "Fighter2Attack")
            {


                Animator attacker, defensor;
                defensor = Fighter1.GetComponentInChildren<Animator>();
                attacker = Fighter2.GetComponentInChildren<Animator>();

                bool isAttackerWin = !isFighter1Win;

                if (steplist.isFirst[0])
                {
                    steplist.isFirst[0] = false;
                    attacker.SetBool("Attacking", true);
                }



                else if (steplist.isFirst[1] &&
                    attacker.GetBool("Attacking") &&
                    attacker.GetCurrentAnimatorStateInfo(0).IsName("HeadButt") &&
                    attacker.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
                {
                    steplist.isFirst[1] = false;
                    defensor.SetBool("Attacked", true);
                    attacker.SetBool("Attacking", false);
                    defensor.SetBool("IsLost", isAttackerWin);
                    attacker.SetBool("IsWin", isAttackerWin);


                    Fighter1.GetComponent<FighterControl>().PlaySound("HIT");
                    if (!isFighter1Win)
                    {
                        Fighter2.GetComponent<FighterControl>().PlaySound("DIE");
                    }
                    PlayParticleSys(HitParticle1);

                }

                else if (steplist.isFirst[2] &&
                    defensor.GetBool("Attacked") && !defensor.GetBool("IsLost") &&
                    defensor.GetCurrentAnimatorStateInfo(0).IsName("HeadHit") &&
                    defensor.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    steplist.isFirst[2] = false;
                    defensor.SetBool("Attacked", false);
                    steplist.NextStep();
                }

                else if (steplist.isFirst[2] &&
                   defensor.GetBool("Attacked") && defensor.GetBool("IsLost") &&
                   defensor.GetCurrentAnimatorStateInfo(0).IsName("Flying Back Death") &&
                   defensor.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f)
                {
                    steplist.isFirst[2] = false;
                    Fighter2.GetComponent<FighterControl>().PlaySound("WIN");
                    steplist.Finish();
                }

            }

            else if (currentStep == "Rest1")
            {
                if (Time.time - steplist.firstTime > 0.8f && steplist.HasNextStep())
                {
                    steplist.NextStep();
                }
            }
            else if (currentStep == "Finish")
            {
                if (Time.time - steplist.firstTime > 2.5f)
                {
                    /*
                    camera.GetComponent<CameraControl>().PauseSound();
                    Time.timeScale = 0;
                    endScreen.gameObject.SetActive(true);
                    */

                    if (isFighter1Win)
                    {

                        SceneNumber++;
                        SceneManager.LoadScene(SceneNumber);


                    }
                    else
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }

                }
            }
            else
            {
                if (steplist.HasNextStep())
                {
                    steplist.NextStep();
                }
            }
        }
    }





}





class MoveCamera
{
    private Transform MyCamera;
    private Vector3 targetPosition;
    private Vector3 targetEular;
    private Vector3 direction;
    private Vector3 rotateDirection;
    private Vector3 middle;

    private float maxSpeed = 20.0f;
    private float rotationSpeed = 1.0f;
    public MoveCamera(Transform MyCamera, Vector3 targetPosition, Vector3 targetEular)
    {
        this.MyCamera = MyCamera;
        this.targetPosition = targetPosition;
        this.targetEular = targetEular;
        this.direction = (targetPosition - MyCamera.position).normalized;
        this.rotateDirection = (MyCamera.eulerAngles - targetEular).normalized;
        this.middle = (MyCamera.position + targetPosition) / 2.0f;
    }

    public bool IsNearTo(Vector3 position, float distance)
    {
        Vector3 diff = MyCamera.position - position;
        for(int i=0;i<3;i++)
        {
            if (diff[i] > distance)
            {
                return false;
            }
        }

        if(Vector3.Distance(MyCamera.position, position) <= distance)
        {
            return true;
        }

        return false;
    }

    public bool IsRotationFinished(Vector3 eularAngle, float error)
    {
        Vector3 diff = MyCamera.eulerAngles - eularAngle;
        for(int i=0;i<3;i++)
        {
            if (diff[i] > error)
            {
                return false;
            }
        }
        if(Vector3.Distance(MyCamera.eulerAngles, eularAngle) <= error)
        {
            return true;
        }
        return false;
    }

    public float GetSpeed()
    {
        return maxSpeed;
    }

    public float GetRotationSpeed()
    {
        return rotationSpeed;
    }

    public void Translate()
    {
        
        if (!IsNearTo(targetPosition, 0.3f))
        {
            MyCamera.Translate(direction * GetSpeed() * Time.deltaTime, Space.World);
        }
        MyCamera.eulerAngles = targetEular;


    }

    public bool IsFinished()
    {
        return IsNearTo(targetPosition, 0.3f);
    }
}


class StepList
{
    public bool[] isFirst;
    public bool finished = false;
    public float firstTime;

    private int currentStep;
    private string[] TODO;
    
    public StepList(string[] TODO)
    {
        this.TODO = new string[TODO.Length];
        for(int i=0;i<TODO.Length;i++)
        {
            this.TODO[i] = TODO[i];
        }
        currentStep = 0;

        isFirst = new bool[100];
        for(int i=0;i<isFirst.Length;i++)
        {
            isFirst[i] = true;
        }
    }

    public string GetCurrentStep()
    {
        if(finished)
        {
            return "Finish";
        }
        if (currentStep < TODO.Length)
        {
            return TODO[currentStep];
        }
        else
        {
            return null;
        }
    }

    public bool HasNextStep()
    {
        if(finished)
        {
            return false;
        }
        return currentStep + 1 < TODO.Length;
    }

    public void NextStep()
    {
        if(finished)
        {
            return;
        }
        if(currentStep + 1 < TODO.Length)
        {
            for (int i = 0; i < isFirst.Length; i++)
            {
                isFirst[i] = true;
            }
            firstTime = Time.time;
            currentStep++;
        }
    }

    public void Finish()
    {
        for (int i = 0; i < isFirst.Length; i++)
        {
            isFirst[i] = true;
        }
        firstTime = Time.time;
        finished = true;
    }


}

