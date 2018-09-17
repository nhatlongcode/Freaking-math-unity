using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Text mathText;
    public Text resultText;
    public Text scoreText;
    public Text finalScore;
    public GameObject gameOverPanel;
    public Text BestScoreText;
    public GameObject timeProgress;
    public GameObject trueButton;
    public GameObject falseButton;
    public GameObject Flying;
    public int speed;

    private int flag;
    private int rightNumber;
    private int leftNumber;
    private int mathOperator;
    private int trueResult;
    private int FalseResult;
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
        flag = 0;
        trueButton.SetActive(true);
        falseButton.SetActive(true);
        timeProgress.SetActive(true);
        //limitTime = 1.0f;
        currentTime = 0;    
        currentScore = 0;
        gameOverPanel.SetActive(false);
        createMath();
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
            timeProgress.transform.localScale = new Vector3(scaleProgressTime, 1, 1);
        }
    }

    public void createMath()
    {
        currentTime = 0;
        leftNumber = Random.Range(0, 10);
        rightNumber = Random.Range(0, 10);
        mathOperator = Random.Range(0, 1);
            switch (mathOperator)
        {
            case 0: // dau cong 
                trueResult = leftNumber + rightNumber;
                FalseResult = trueResult + Random.Range(-1, 1);
                mathText.GetComponent<Text>().text = leftNumber.ToString() + " + " + rightNumber.ToString();
                resultText.GetComponent<Text>().text = "=" + FalseResult.ToString();
            
                break;

            case 1: // dau tru
                if (leftNumber >= rightNumber)
                {
                    trueResult = leftNumber - rightNumber;
                    FalseResult = trueResult + Random.Range(-2, 2);
                    mathText.GetComponent<Text>().text = leftNumber.ToString() + " - " + rightNumber.ToString();
                    resultText.GetComponent<Text>().text = "=" + FalseResult.ToString();

                }
                else
                {
                    trueResult = rightNumber - leftNumber;
                    FalseResult = trueResult + Random.Range(-2, 2);
                    mathText.GetComponent<Text>().text = rightNumber.ToString() + " - " + leftNumber.ToString();
                    resultText.GetComponent<Text>().text = "=" + FalseResult.ToString();

                }
                //FalseResult = trueResult + Random.Range(-2, 2);
                break;  
                
        }
        scoreText.GetComponent<Text>().text = currentScore.ToString();
    } //random ra phep toan

    public void onTrueButtonClick()
    {
        if (trueResult == FalseResult) // thang
        {
            audio.PlayOneShot(trueClip);
            currentScore += 1;
            //Move();
            createMath();
        }
        else // thua
        {
            audio.PlayOneShot(falseClip);
            flag++;
            Loss();
        }
    }

    public void onFalseButtonClick() //thang
    {
        if(trueResult != FalseResult)
        {
            audio.PlayOneShot(trueClip);
            currentScore += 1;
            //Move();
            createMath();

        }
        else //thua
        {
            audio.PlayOneShot(falseClip);
            flag++;
            Loss();
        }
    }

    public void restartButtonOnClick()
    {
        //flag = 0;
        Start();
    } //choi lai

    public void mainMenuButtonOnClick()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        //Application.LoadLevel(1);
    } // chuyen ve menu

    private void checkBest()  
    {
        highScore = PlayerPrefs.GetInt("highscore");
        if (currentScore > highScore)
        {
            //highScore = currentScore;
            PlayerPrefs.SetInt("highscore", currentScore);
            PlayerPrefs.Save();
        }
    }  // kiem tra best da luu trong bo nho

    private void Loss()
    {
        //audio.PlayOneShot(falseClip);
        checkBest();
        highScore = PlayerPrefs.GetInt("highscore");
        BestScoreText.GetComponent<Text>().text = highScore.ToString();
        finalScore.GetComponent<Text>().text = currentScore.ToString();
        trueButton.SetActive(false);
        falseButton.SetActive(false);

        gameOverPanel.SetActive(true);
        timeProgress.SetActive(false);
    } // goi khi thua cmn roi

    void Move()
    {
        Vector3 temp = Flying.transform.position;
        temp.x -= speed * Time.deltaTime;
        Flying.transform.position = temp;
    } // animation khi chuyen bai toan (dang fix)
}

