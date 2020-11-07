using System.Collections;
using UnityEngine;

namespace BrokenFury.Test
{
    internal class SelectedBuilding : State
    {
        public SelectedBuilding(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log("SelectedBilding State Active.");

            BattleSystem.BuildingInterface.SetActive(true);
            BattleSystem.BuildingInterface.transform.localPosition = PanelPosition();
            
            yield return new WaitForSeconds(0f);
        }

        Vector2 PanelPosition()
        {
            float offsetPosY = BattleSystem.SelectedBuilding.transform.position.y - 1f;

            // Final position of marker above GO in world space
            Vector3 offsetPos = new Vector3(BattleSystem.SelectedBuilding.transform.position.x, offsetPosY, BattleSystem.SelectedBuilding.transform.position.z);

            // Calculate *screen* position (note, not a canvas/recttransform position)
            Vector2 canvasPos;
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

            // Convert screen position to Canvas / RectTransform space <- leave camera null if Screen Space Overlay
            RectTransformUtility.ScreenPointToLocalPointInRectangle(BattleSystem.BuildingInterface.GetComponentInParent<Canvas>().gameObject.GetComponent<RectTransform>(), screenPoint, null, out canvasPos);

            // Set
            return canvasPos;
        }

        public override IEnumerator Tile()
        {
            yield return new WaitForSeconds(0f);            
        }

        public override IEnumerator Building()
        {
            BattleSystem.SetState(new SelectedBuilding(BattleSystem));

            yield return new WaitForSeconds(0f);           
        }
    }
}