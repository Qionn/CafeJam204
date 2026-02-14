using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManagerBehavior : MonoBehaviour
{
    
    public void IncreaseLevelIndex(int levelIncrementCount)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + levelIncrementCount);
    }

    public void SetLevelIndex(int levelIndex)
    {
        SceneManager.LoadScene(levelIndex);
    }
}
