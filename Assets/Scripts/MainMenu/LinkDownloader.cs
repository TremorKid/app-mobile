using UnityEngine;

namespace MainMenu
{
    public class LinkDownloader : MonoBehaviour
    {
        private const string Link = "https://drive.google.com/file/d/1I8uKuggBOWleD4Ibd6DlfxeVcNsnUNlX/view?usp=sharing";

        public void OpenDownloadLink()
        {
            Application.OpenURL(Link);
        }
    } 
}