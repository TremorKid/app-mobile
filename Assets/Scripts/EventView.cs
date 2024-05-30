using UnityEngine;

public class EventView : MonoBehaviour
{
    public void ModelFound(string modelName)
    {
        Debug.Log(modelName);
    }
}
