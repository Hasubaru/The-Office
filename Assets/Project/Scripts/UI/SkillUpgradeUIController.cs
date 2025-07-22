using UnityEngine;

public class SkillUpgradeUIController : MonoBehaviour
{
    public GameObject skillUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            skillUI.SetActive(!skillUI.activeSelf);
        }
    }
}
