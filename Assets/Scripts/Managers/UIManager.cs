using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Globalization;
using System.Text.RegularExpressions;
using DG.Tweening;
//using ElephantSDK;
//using MoreMountains.NiceVibrations;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("Panels")] [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject successPanel;
    [SerializeField] private GameObject failPanel;

    [Header("TextMeshPro")]
    [SerializeField]private TMP_Text levelText;
    [SerializeField]private TMP_Text coinText;

    private void OnEnable()
    {

        Config.OnGameCompleted += OnGameCompleted;
        Config.OnGameFailed += OnGameFailed;
        Config.OnCoinCollected += OnCoinCollected;
        coinText.text = LevelCreator.Instance.nextCoinOrder.ToString();
    }

    private void OnCoinCollected(Coin _collectedCoin, int _nextCoinOrder)
    {
        coinText.text = _nextCoinOrder.ToString();
    }

    private void OnGameFailed()
    {
        failPanel.SetActive(true);
        Debug.Log("GAME FAILED!!!");
    }

    private void OnGameCompleted()
    {
        successPanel.SetActive(true);
        Debug.Log("GAME COMPLETED!!!");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnDisable()
    {
        Config.OnGameCompleted -= OnGameCompleted;
        Config.OnGameFailed -= OnGameFailed;
    }
}