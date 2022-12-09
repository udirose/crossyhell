using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    //outlets
    public static GameController Instance;
    public Transform bg1;
    public Transform bg2;
    public Transform road1;
    public Transform road2;
    public Transform playerPos;
    public Transform bottomBound;
    public Transform topBound;
    public GameObject bottomBoundObject;
    public GameObject topBoundObject;
    public GameObject topBossBarrier;
    public GameObject botBossBarrier;

    public Player Player;
    //prefabs
    public GameObject[] carsToRight;

    public GameObject[] carsToLeft;
    /*public GameObject car1Prefab;
    public GameObject car2Prefab;*/
    public GameObject bossPrefab;
    //setters
    private Vector3 _r1Target = new Vector3();
    private Vector3 _r2Target = new Vector3();
    private Vector2 boundTargetPos = new Vector3();
    private Vector2 boundTopTargetPos = new Vector3();
    private Vector3 _lastPlayerPos;
    private Vector3 _currentPlayerPos;
    //Global Values
    public int globalScore;
    public bool hasDeathDefiedUnlocked;
    
    //difficulty config
    public float minCarDelay = .005f;
    public float maxCarDelay = 6f;
    public float minShotFreq;
    public float maxShotFreq;
    public float minBulletForce;
    public float maxBulletForce;
    public float minCarSpeed;
    public float maxCarSpeed;
    public float timeToMaxDifficulty = 600f;
    //difficulty state tracking
    private float _timeElapsed;
    private float _carDelay;
    private float _shotFreq;
    private float _bulletForce;
    private float _carSpeed;
    //boss config and statetracking
    public bool bossPresent;
    public bool bossDied;
    private float disapearTime;
    public bool bossJustAppeared = false;

    public float bossesBeat = 0;
    //UI
    public TextMeshProUGUI bossText;
    public TextMeshProUGUI WinText;

    private void Awake()
    {
        if(Instance != null && Instance != this) {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
    }
    // Start is called before the first frame update
    private void Start()
    {
        globalScore = PlayerPrefs.GetInt("GlobalScore");
        hasDeathDefiedUnlocked = PlayerPrefs.GetString("DeathDefied") == "true";
        var targetTransform = playerPos.transform;
        var targetTransformPos = targetTransform.position;
        _lastPlayerPos = new Vector3(targetTransformPos.x,targetTransformPos.y,targetTransformPos.z);
        bossPresent = false;
        bossDied = false;
        bossText.enabled = false;
        WinText.enabled = false;
        StartCoroutine(nameof(CarSpawnTimer));
        StartCoroutine(nameof(BossTimer));
        
        //set boss barriers to invisible
        topBossBarrier.SetActive(false);
        botBossBarrier.SetActive(false);
    }
    


    private void Update()
    {
        SetRoads();
        _timeElapsed += Time.deltaTime;

        //adjust difficulty of cars
        if (!bossPresent) { UpdateDifficultyValues(); }

        if (bossPresent && playerPos.transform.position.y > botBossBarrier.transform.position.y)
        {
            bottomBoundObject.SetActive(true);
            botBossBarrier.SetActive(true);
        }
        else if(bossPresent)
        {
            MoveBottomBound();
            botBossBarrier.SetActive(false);
        }

        if (bossDied)
        {
            topBossBarrier.SetActive(false);
            botBossBarrier.SetActive(false);
            EnableWinText();
            Player.HealAll();
            StartCoroutine(nameof(CarSpawnTimer));
            StartCoroutine(nameof(BossTimer));
            bossDied = false;
            bossPresent = false;
        }

        //Bounds Logic
        ManageBounds();
        
        //text management
        if (bossText.enabled && (Time.time > disapearTime))
        {
            bossText.enabled = false;
        }
        if (WinText.enabled && (Time.time > disapearTime))
        {
            WinText.enabled = false;
        }
        
    }

    #region Coroutines
    public IEnumerator CarSpawnTimer()
    {
        yield return new WaitForSeconds(_carDelay);
        SpawnCars();
        StartCoroutine(nameof(CarSpawnTimer));
    }

    private IEnumerator BossTimer()
    {
        yield return new WaitForSeconds(50);
        SpawnBoss();
        bossPresent = true;
        bossJustAppeared = true;
        StopCoroutine(nameof(CarSpawnTimer));
        EnableBossText();
    }
    #endregion

    #region SpawnAndDifficulty
    private void SetRoads()
    {
        //sets road1 position to background1
        var r1Pos = road1.position;
        r1Pos = SetPos(_r1Target, r1Pos.x, bg1.position.y+15, r1Pos.z);
        road1.position = r1Pos;
        //sets road2 position to background1
        var r2Pos = road2.position;
        r2Pos = SetPos(_r2Target, r2Pos.x, bg2.position.y+15, r2Pos.z);
        road2.position = r2Pos;
    }
    private void SpawnCars()
    {
        //road 1 spawn
        var roadPos1 = road1.transform.position;
        var roadPos2 = road2.transform.position;
        
        var car1 = carsToRight[Random.Range(0, carsToRight.Length)];
        var c1Pos = car1.transform.position;
        var car2 = carsToLeft[Random.Range(0,carsToLeft.Length)];
        var c2Pos = car2.transform.position;
        ApplyValuesToCars(car1,car2);

        Instantiate(car1,new Vector3(c1Pos.x,roadPos1.y+1.5f,c1Pos.z),Quaternion.identity);
        Instantiate(car2,new Vector3(c2Pos.x,roadPos1.y-1.5f,c2Pos.z),Quaternion.identity);
        
        //road 2 spawn
        var car3 = carsToRight[Random.Range(0, carsToRight.Length)];
        var c3Pos = car3.transform.position;
        var car4 = carsToLeft[Random.Range(0,carsToLeft.Length)];
        var c4Pos = car4.transform.position;
        ApplyValuesToCars(car3,car4);
        
        Instantiate(car3,new Vector3(c3Pos.x,roadPos2.y+1.5f,c3Pos.z),Quaternion.identity);
        Instantiate(car4,new Vector3(c4Pos.x,roadPos2.y-1.5f,c4Pos.z),Quaternion.identity);
    }

    private void SpawnBoss()
    {
        var roadPos1 = road1.transform.position;
        var roadPos2 = road2.transform.position;
        var playersPosition = this.playerPos.position;
        var bossPos = bossPrefab.transform.position;

        if (roadPos1.y-7 > playersPosition.y)
        {
            Instantiate(bossPrefab, new Vector3(bossPos.x, roadPos1.y + 2, bossPos.z), Quaternion.identity);
        } else if (roadPos2.y-7 > playersPosition.y)
        {
            Instantiate(bossPrefab, new Vector3(bossPos.x, roadPos2.y + 2, bossPos.z), Quaternion.identity);
        }
        
        //set boss barriers to visible
        topBossBarrier.SetActive(true);
    }

    private void UpdateDifficultyValues()
    {
        //make cars spawn faster
        var shortenCarTime = maxCarDelay - ((maxCarDelay - minCarDelay) / timeToMaxDifficulty * _timeElapsed);
        _carDelay = Mathf.Clamp(shortenCarTime, minCarDelay, maxCarDelay);
        //make bullets shoot faster
        var shortenShotFreq = maxShotFreq - ((maxShotFreq - minShotFreq) / timeToMaxDifficulty * _timeElapsed);
        _shotFreq = Mathf.Clamp(shortenShotFreq, minShotFreq, maxShotFreq);
        
        //make bullets faster
        var speedBullets = minBulletForce + ((Mathf.Abs(minBulletForce - maxBulletForce)) / timeToMaxDifficulty * _timeElapsed);
        _bulletForce = Mathf.Clamp(speedBullets, minBulletForce, maxBulletForce);
        //make cars faster
        var speedCar = minCarSpeed + ((Mathf.Abs(minCarSpeed - maxCarSpeed)) / timeToMaxDifficulty * _timeElapsed);
        _carSpeed = Mathf.Clamp(speedCar, minCarSpeed, maxCarSpeed);
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ApplyValuesToCars(GameObject c1,GameObject c2)
    {
        var c1Script = c1.GetComponent<CarScript>();
        var c2Script = c2.GetComponent<CarScript>();
        //c1Script.SetFireAlt();
        c1Script.SetShotFreq(_shotFreq);
        c2Script.SetShotFreq(_shotFreq);
        c1Script.SetBulletForce(_bulletForce);
        c2Script.SetBulletForce(_bulletForce);
        c1Script.SetSpeed(_carSpeed);
        c2Script.SetSpeed(_carSpeed);
    }
    #endregion
    private static Vector3 SetPos(Vector3 pos, float x, float y, float z)
    {
        pos.x = x;
        pos.y = y;
        pos.z = z;
        return pos;
    }
    
    private void ManageBounds()
    {
        _currentPlayerPos = playerPos.position;
        var botBoundPos = bottomBound.position;
        var topBoundPos = topBound.position;
        
        if (bossPresent)
        {
            var boss = FindObjectOfType<BossVan>();
            var bossPosition = boss.transform.position;
            if (playerPos.transform.position.y > bossPosition.y-23)
            {
                bottomBound.position = SetPos(boundTargetPos, botBoundPos.x, bossPosition.y-23, botBoundPos.z);
            }
            topBound.position = SetPos(boundTopTargetPos, topBoundPos.x,bossPosition.y-7.5f, topBoundPos.z);
        }
        else
        {
            topBound.position = SetPos(boundTopTargetPos, topBoundPos.x,bg2.position.y+40, topBoundPos.z);
        }
        _lastPlayerPos = _currentPlayerPos;
    }

    public void MoveBottomBound()
    {
        var botBoundPos = bottomBound.position;
        bottomBound.position = SetPos(boundTargetPos, botBoundPos.x, playerPos.position.y-3, botBoundPos.z);
    }
    
    private void EnableBossText()
    {
        bossText.enabled = true;
        disapearTime = Time.time + 2f;
    }

    private void EnableWinText()
    {
        WinText.enabled = true;
        disapearTime = Time.time + 3f;
    }
    
    void OnApplicationQuit()
    {
        PlayerPrefs.Save();
    }

    public void AddToGlobalScore(int amt)
    {
        globalScore += amt;
        PlayerPrefs.SetInt("GlobalScore",globalScore);
        PlayerPrefs.Save();
    }
}