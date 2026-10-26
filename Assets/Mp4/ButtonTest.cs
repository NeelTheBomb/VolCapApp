using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    public GameObject cube;

    public void Hidecube()
    {
        if (cube != null)
            cube.SetActive(false);
    }

}
