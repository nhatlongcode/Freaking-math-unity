using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour {
    [Header("Math 1")]
    public GameObject math_1;
    public Text mathText_1;
    public Text resultText_1;
    //[Space(10)]
    
    [Header("Math 2")]
    public GameObject math_2;
    public Text mathText_2;
    public Text resultText_2;
    
    [Header("Gameover panel")]
    public GameObject gameOverPanel;
    public Text bestScoreText;
    public Text finalScoreText;

    [Header("UI")]
    public Text scoreText;
    public GameObject timeProgress;
    public GameObject trueButton;
    public GameObject falseButton;
    public GameObject trueParticle;
    public GameObject falseParticle;
    
    [Header("Speed")]
    public int speed;


    private GameObject Flying;
    private bool check;
    private bool click;
    private int flag;
    private int rightNumber;
    private int leftNumber;
    private int mathOperator;
    private int trueResult;
    private int falseResult;
    private int currentScore;
    private int highScore;

    [SerializeField]
    private float limitTime;
        
    [SerializeField]
    private AudioSource audio;

    [SerializeField]
    private AudioClip trueClip, falseClip;

    private float currentTime;



    public void Start()
    {
        //PlayerPrefs.SetInt("highscore", 0);
        //PlayerPrefs.Save();
        check = false;
        click = true;
        flag = 0;
        trueButton.SetActive(true);
        falseButton.SetActive(true);
        timeProgress.SetActive(true);
        //limitTime = 1.0f;
        currentTime = 0;    
        currentScore = 0;
        gameOverPanel.SetActive(false);
        if (currentScore == 0 || currentScore % 2 == 0)
        {
            CreateMath(mathText_1, resultText_1);
        }
        else CreateMath(mathText_2, resultText_2);
    }

    public void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime > limitTime)
        {
            if (flag == 0)
            {
                audio.PlayOneShot(falseClip);
                flag++;
            }
            
            Loss();
        }
        else
        {
            float scaleProgressTime = 1.0f - currentTime / limitTime;
            timeProgress.transform.localScale = new Vector3(scaleProgressTime, timeProgress.transform.localScale.y, 1);
        }
    }

    private void CreateMath(Text mathText, Text resultText)
    {
        currentTime = 0;
        leftNumber = Random.Range(0, 10);
        rightNumber = Random.Range(0, 10);
        mathOperator = Random.Range(0, 1);
            switch (mathOperator)
        {
            case 0: // plus 
                trueResult = leftNumber + rightNumber;
                falseResult = trueResult + Random.Range(-1, 1);
                mathText.GetComponent<Text>().text = leftNumber.ToString() + " + " + rightNumber.ToString();
                resultText.GetComponent<Text>().text = "=" + falseResult.ToString();
            
                break;

            case 1: // minus
                if (leftNumber >= rightNumber)
                {
                    trueResult = leftNumber - rightNumber;
                    falseResult = trueResult + Random.Range(-2, 2);
                    mathText.GetComponent<Text>().text = leftNumber.ToString() + " - " + rightNumber.ToString();
                    resultText.GetComponent<Text>().text = "=" + falseResult.ToString();

                }
                else
                {
                    trueResult = rightNumber - leftNumber;
                    falseResult = trueResult + Random.Range(-2, 2);
                    mathText.GetComponent<Text>().text = rightNumber.ToString() + " - " + leftNumber.ToString();
                    resultText.GetComponent<Text>().text = "=" + falseResult.ToString();

                }
                //falseResult = trueResult + Random.Range(-2, 2);
                break;  
                
        }
        scoreText.GetComponent<Text>().text = currentScore.ToString();
    } 

    public void OnTrueButtonClick()
    {
        trueParticle.GetComponent<ParticleSystem>().Play();
        if (trueResult == falseResult) // thang
        {
            check = true;
            Invoke("Delay", 0.2f);
            audio.PlayOneShot(trueClip);
            currentScore += 1;
            //Move();
            if (currentScore == 0 || currentScore % 2 == 0)
            {
                CreateMath(mathText_1, resultText_1);
            }
            else CreateMath(mathText_2, resultText_2);
        }
        else // thua
        {
            audio.PlayOneShot(falseClip);
            flag++;
            Loss();
        }
    }

    public void OnFalseButtonClick() //thang
    {
        falseParticle.GetComponent<ParticleSystem>().Play();
        if(trueResult != falseResult)
        {
            check = true;
            Invoke("Delay", 0.2f);
            audio.PlayOneShot(trueClip);
            currentScore += 1;
            //Move();
            if (currentScore == 0 || currentScore % 2 == 0)
            {
                CreateMath(mathText_1, resultText_1);
            }
            else CreateMath(mathText_2, resultText_2);

        }
        else //thua
        {
            audio.PlayOneShot(falseClip);
            flag++;
            Loss();
        }
    }

    public void OnRestartButtonOnClick()
    {
        //flag = 0;
        Start();
    } 

    public void OnMainMenuButtonOnClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        //Application.LoadLevel(1);
    } 

    private void CheckBest()  
    {
        highScore = PlayerPrefs.GetInt("highscore");
        if (currentScore > highScore)
        {
            //highScore = currentScore;
            PlayerPrefs.SetInt("highscore", currentScore);
            PlayerPrefs.Save();
        }
    }  

    private void Loss()
    {
        CheckBest();
        highScore = PlayerPrefs.GetInt("highscore");
        bestScoreText.GetComponent<Text>().text = highScore.ToString();
        finalScoreText.GetComponent<Text>().text = currentScore.ToString();
        trueButton.SetActive(false);
        falseButton.SetActive(false);

        gameOverPanel.SetActive(true);
        timeProgress.SetActive(false);
    } // goi khi thua cmn roi

    private void Move()
    {
        Vector3 temp = Flying.transform.position;
        temp.x -= speed * Time.deltaTime;
        Flying.transform.position = temp;
    } // animation khi chuyen bai toan (dang fix)

    private void Delay()
    {
        check = false;
        click = true;
    }

    void FixedUpdate() // 0.02s
    {

        //time.GetComponent<Text>().text = v.ToString();
        if ((check == true))
        {
            click = false;
            math_1.transform.localPosition += new Vector3(-50, 0.0f);
            math_2.transform.localPosition += new Vector3(-50, 0f);

        }

        if ((math_1.transform.localPosition.x == -500))
        {
            click = false;
            math_1.SetActive(false);
            math_1.transform.localPosition = new Vector3(500, 0f);
            if (math_1.transform.localPosition.x == 500) math_1.SetActive(true);
        }

        if (math_2.transform.localPosition.x == -500)
        {
            click = false;
            math_2.SetActive(false);
            math_2.transform.localPosition = new Vector3(500, 0f);
            if (math_2.transform.localPosition.x == 500) math_2.SetActive(true);
        }

    }
}

