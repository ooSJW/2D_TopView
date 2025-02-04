/*
	* Coder :
	* Last Update :
	* Information
*/
namespace project02
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public partial class PlayerAnimation : MonoBehaviour // Data Field
    {
        private Player player;

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        private Color originalColor;
    }
    public partial class PlayerAnimation : MonoBehaviour // Initialize
    {
        private void Allocate()
        {

        }
        public void Initialize(Player playerValue)
        {
            player = playerValue;

            Allocate();
            Setup();
        }
        private void Setup()
        {
            originalColor = spriteRenderer.color;
        }
    }
    public partial class PlayerAnimation : MonoBehaviour // Progress
    {
        public void LateProgress()
        {
            FlipX();
            SetAnimation();
        }
    }
    public partial class PlayerAnimation : MonoBehaviour // Property
    {
        private void FlipX()
        {
            float inputX = player.InputVector.x;
            if (inputX != 0)
                spriteRenderer.flipX = inputX < 0;
        }
        private void SetAnimation()
        {
            if (player.PlayerState != PlayerState.Death)
                animator.SetInteger("State", (int)player.PlayerState);
        }
        public void PlayerDeath()
        {
            animator.SetTrigger("PlayerDeath");
        }
        public void GameOver()
        {
            MainSystem.Instance.UIManager.UIContoller.GameOver();
        }
    }
    public partial class PlayerAnimation : MonoBehaviour // Coroutine
    {
        public IEnumerator Invincible()
        {
            float blinkDuration = 0.2f;
            int blinkCount = 5;
            int currentBlinkCount = 0;
            bool isVisible = true;
            gameObject.layer = LayerMask.NameToLayer("HitPlayer");
            while (currentBlinkCount < blinkCount)
            {
                Color blinkColor = spriteRenderer.color;
                float alpha = originalColor.a;
                blinkColor.a = isVisible ? alpha * 0.3f : alpha;
                spriteRenderer.color = blinkColor;

                isVisible = !isVisible;
                currentBlinkCount++;

                yield return new WaitForSeconds(blinkDuration);
            }
            spriteRenderer.color = originalColor;
            player.EndInvincible();
        }
    }
}
