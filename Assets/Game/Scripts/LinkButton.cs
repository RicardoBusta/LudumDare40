using UnityEngine;

namespace Game.Scripts
{
    public class LinkButton : MonoBehaviour
    {
        public string Url;

        public void ButtonClicked()
        {
            Application.OpenURL(Url);
        }
    }
}
