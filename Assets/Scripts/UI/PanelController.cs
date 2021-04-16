
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class PanelController : MonoBehaviour
{

    [System.Serializable]
    public class PlayerCollidedEvent : UnityEvent { }

    Scene ThisScene;
    bool inGame;
    float timer;
    float temptimer;
    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject helpMenu;
    public GameObject inGameUI;
    public GameObject gameOverUi;
    public GameObject player;
    public GameObject scoreT;
    public GameObject fuelGauge;
    public GameObject healthGauge;
    public GameObject shootGauge;
    string LoadedLevel;
    public Texture2D crosshair;
    public Texture2D DefaultCursor;

    public void Awake()
    {

        DontDestroyOnLoad(this.gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        ThisScene = SceneManager.GetActiveScene();
        Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!inGame)
        {
            return;
        }
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        } else
        {
            if (player.GetComponent<PlayerControl>().health <= 0)
            {
                gameOverUi.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
            } else {

                temptimer += (1f * Time.deltaTime);
                timer = Mathf.RoundToInt(temptimer);
                fuelGauge.GetComponent<Slider>().value = player.GetComponent<PlayerControl>().fuel;
                healthGauge.GetComponent<Slider>().value = player.GetComponent<PlayerControl>().health;
                shootGauge.GetComponent<Slider>().value = player.GetComponent<PlayerControl>().elapsedTime;

                scoreT.GetComponent<Text>().text = (timer + player.GetComponent<PlayerControl>().score).ToString();
            }



            //scoreT.GetComponent<Text>().text = player.GetComponent<SnakeHeadScript>().score.ToString();
        } 
    }


    public void onButtonClick(int choiceButt)
    {
        switch (choiceButt)
        {
            case 0:
                creditsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                helpMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0,0,0);
                break;
            case 1:
                ChangetoLevel("MainGame");
                Vector2 hotSpot = new Vector2(crosshair.width / 2f, crosshair.height / 2f);
                Cursor.SetCursor(crosshair, hotSpot, CursorMode.ForceSoftware);
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                break;
            case 2:
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                helpMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                break;
            case 3:
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                creditsMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                break;
            case 4:
                SceneManager.LoadScene("Start Screen Scene");
                mainMenu.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                gameOverUi.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
                inGameUI.SetActive(false);
                inGame = false;
                Cursor.SetCursor(DefaultCursor,Vector2.zero,CursorMode.ForceSoftware);
                break;
            case 5:
                Debug.Log("Quit game");
                Application.Quit();
                break;
            case 6:
                Application.OpenURL("https://www.ronaldvarela.com/");
                break;
            default:
                break;
        }}

    private void ChangetoLevel(string LevelChange)
    {
        inGameUI.SetActive(true);
        inGame = true;

        SceneManager.LoadScene(LevelChange);

        //this.GetComponent<Animator>().SetInteger("ChangeScene", 1);
        //LoadedLevel = LevelChange;
    }

    /*void ChangeScene()
    {
        SceneManager.LoadScene(LoadedLevel);
        levelselectUI.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 7000, 0);
        this.GetComponent<Animator>().SetInteger("ChangeScene", 2);
    }*/
}
