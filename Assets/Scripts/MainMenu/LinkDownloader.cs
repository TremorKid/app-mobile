using UnityEngine;

namespace MainMenu
{
    public class LinkDownloader : MonoBehaviour
    {
        private const string Link = "https://drive.google.com/uc?export=download&id=1PEB2yxpODUfpNypoN6kXrxRNfmQFgNHc";

        public void OpenDownloadLink()
        {
            Application.OpenURL(Link);
        }
    } 
}