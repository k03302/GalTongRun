using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIControl : MonoBehaviour
{
    public TextMeshPro StageNumber;
    // Start is called before the first frame update
    void Start()
    {
        StageNumber.SetText("" + (GameSys.SceneNumber + 1));
    }


}
