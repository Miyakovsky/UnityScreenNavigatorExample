using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityScreenNavigator.Runtime.Core.Modal;

public class ModalTest : MonoBehaviour
{
    [SerializeField]
    private Button _openModalButton = null;
    [SerializeField]
    private Text _resultText = null;

    private ModalContainer _modalContainer = null;
    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(_openModalButton);
        Assert.IsNotNull(_resultText);

        _modalContainer = ModalContainer.Find("ModalContainer");
        Assert.IsNotNull(_modalContainer);
        _resultText.text = "Not SelectButton";

        _openModalButton.onClick.AddListener(
            () =>
            {
                OnClick_OpenModal();
            }
            );
    }

    //Button Event : Modal�̕\��
    public void OnClick_OpenModal()
    {
        StartCoroutine(OpenModal());
    }

    //Button Event : YES�{�^���������ꂽ
    public void OnClick_YesButton()
    {
        _modalContainer.Pop(true);
        _resultText.text = "Select Yes Button";
    }

    //Button Event : NO�{�^���������ꂽ
    public void OnClick_NoButton()
    {
        _modalContainer.Pop(true);
        _resultText.text = "Select No Button";

    }

    //Modal�̕\��
    private IEnumerator OpenModal()
    {
        var handle = _modalContainer.Push("YesNoModal", true, onLoad: x =>
        {
            var yesNoModal = (YesNoModal)x.modal;
            //�e�{�^���Ɏ��s���̏�����o�^
            yesNoModal.OnClickYesButton.AddListener(OnClick_YesButton);
            yesNoModal.OnClickNoButton.AddListener(OnClick_NoButton);
        });
        yield return handle;
    }
}

