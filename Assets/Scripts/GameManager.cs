using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] float startingHealth;
    [SerializeField] Slider healthBar;
    [SerializeField] GameObject instruction;
    [SerializeField] GameObject GOP;
    private void Start()
    {
        GameEvents.instance.onBabyHurt += DecHealth;
        GameEvents.instance.onGameOver += GameOver;
        healthBar.maxValue = startingHealth;
        healthBar.value = startingHealth;
        StartCoroutine(DisableInstruction());
    }
    IEnumerator DisableInstruction()
    {
        yield return new WaitForSeconds(7);
        instruction.SetActive(false);
    }
    void DecHealth(float health)
    {
        if(health<= 0)
        {
            health = 0;
            GameOver();
        }
        healthBar.value = health;
    }
    void GameOver()
    {
        GOP.SetActive(true);
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
