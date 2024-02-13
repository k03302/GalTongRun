using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    public Text DeathCount, ReplayCount, ClearTime;
    // Start is called before the first frame update
    void Start()
    {
        DeathCount.text = GameSys.DeathCount + "회";
        ReplayCount.text = GameSys.ReplayCount + "회";

        int minutes = (int)((Time.time - GameSys.StartTime) / 60f);
        int seconds = (int)(Time.time - GameSys.StartTime) % 60;
        ClearTime.text = minutes + "분 " + seconds + "초";
    }


}
