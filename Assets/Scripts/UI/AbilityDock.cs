using AbilitySystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class AbilityDock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private AbilityComponent abilityComponent;
        [SerializeField] private AbilityUI abilityUIPrefab;

        //readonly List<AbilityUI> abilities = new();

        PointerEventData touchData;
        private AbilityUI highlightedAbility;

        private void Start()
        {
            abilityComponent.OnNewAbilityAdded += AbilityComponent_OnNewAbilityAdded;
        }

        private void OnDestroy()
        {
            abilityComponent.OnNewAbilityAdded -= AbilityComponent_OnNewAbilityAdded;
        }

        private void AbilityComponent_OnNewAbilityAdded(Ability ability)
        {
            AbilityUI newAbilityUI = Instantiate(abilityUIPrefab, transform);
            newAbilityUI.Init(ability);
            //abilities.Add(newAbilityUI);
        }

        private void Update()
        {
            if (touchData != null && GetUIUnderPointer(touchData, out AbilityUI current))
                highlightedAbility = current;
        }

        public void OnPointerDown(PointerEventData eventData) => touchData = eventData;

        public void OnPointerUp(PointerEventData eventData)
        {
            if (highlightedAbility != null)
                highlightedAbility.Activate();
            touchData = null;
        }

        bool GetUIUnderPointer(PointerEventData eventData, out AbilityUI abilityUI)
        {
            List<RaycastResult> findAbility = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, findAbility);

            abilityUI = null;
            foreach (RaycastResult raycastResult in findAbility)
            {
                abilityUI = raycastResult.gameObject.GetComponentInParent<AbilityUI>();
                if (abilityUI != null)
                    return true;
            }

            return false;
        }
    }
}