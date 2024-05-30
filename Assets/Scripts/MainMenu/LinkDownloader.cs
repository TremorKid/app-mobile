using System.Collections;
using UnityEngine;
using System;

public class LinkDownloader : MonoBehaviour
{
    private string link = "https://drive.google.com/uc?export=download&id=1PEB2yxpODUfpNypoN6kXrxRNfmQFgNHc";

    public void OpenDownloadLink()
    {
        Application.OpenURL(link);
    }
}
