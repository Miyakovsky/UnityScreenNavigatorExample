using UnityEngine;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

public class YesNoModal : Modal
{
    [SerializeField]
    private Button _yesButton = null;

    [SerializeField]
    private Button _noButton = null;


    public Button.ButtonClickedEvent OnClickYesButton => _yesButton.onClick;
    public Button.ButtonClickedEvent OnClickNoButton => _noButton.onClick;

}

