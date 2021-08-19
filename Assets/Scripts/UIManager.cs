using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject cross;
    public Text ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Score", 0);
        Cursor.visible = false;   
    }
    // Update is called once per frame
    void Update()
    {
        ScoreText.text = "Score: " + PlayerPrefs.GetInt("Score", 0);
        cross.transform.position = Input.mousePosition;
    }
}
