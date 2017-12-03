using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class LoadScreenButton : MonoBehaviour
    {
        public string SceneName;

        public void ButtonClicked()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}