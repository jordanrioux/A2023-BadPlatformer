using Platformer.Managers;
using TMPro;
using UnityEngine;

namespace Platformer.UI
{
    public class UIDiamondsText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _diamondsText;

        private void Start()
        {
            GameManager.Instance.OnDiamondsChanged += HandleOnDiamondsChanged;
            HandleOnDiamondsChanged(GameManager.Instance.Diamonds);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnDiamondsChanged -= HandleOnDiamondsChanged;
        }

        private void HandleOnDiamondsChanged(int diamondsAmount)
        {
            _diamondsText.text = diamondsAmount.ToString();
        }
    }
}
