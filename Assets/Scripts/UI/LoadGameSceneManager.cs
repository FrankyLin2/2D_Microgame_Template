using UnityEngine;

namespace Platformer.UI
{
    public class LoadGameSceneManager: MonoBehaviour
    {
        public void LoadGameScene(int level)
        {
            // Load the game scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(level);
        }
    }
}