using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro = null;
    [SerializeField] Button button = null;

    public void SetBaseAction(BaseAction baseAction)
    {
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        button.onClick.AddListener( () =>
        {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }
}
