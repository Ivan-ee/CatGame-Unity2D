using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.UI.Canvas
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIScreen : MonoBehaviour 
    {
        #region Variables
        [Header("Main Properties")]
        public Selectable m_StartSelectable;

        [Header("Screen Events")]
        public UnityEvent onScreenStart = new UnityEvent();
        public UnityEvent onScreenClose = new UnityEvent();

        private Animator animator;
        #endregion


        #region Main Methods
        // Use this for initialization
        void Start () 
        {
            animator = GetComponent<Animator>();

            if(m_StartSelectable)
            {
                EventSystem.current.SetSelectedGameObject(m_StartSelectable.gameObject);
            }
        }
        #endregion


        #region Helper Methods
        public virtual void StartScreen()
        {
            if(onScreenStart != null)
            {
                onScreenStart.Invoke();
            }

            HandleAnimator("show");
        }


        public virtual void CloseScreen()
        {
            if(onScreenClose != null)
            {
                onScreenClose.Invoke();
            }

            HandleAnimator("hide");
        }


        void HandleAnimator(string aTrigger)
        {
            if(animator)
            {
                animator.SetTrigger(aTrigger);
            }
        }
        #endregion
    }
    
}