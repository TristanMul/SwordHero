using UnityEngine;

#pragma warning disable 649

namespace RavingBots.CartoonExplosion
{
	public class RingAnimator : RadialAnimator
	{
		[SerializeField] SpriteRenderer SecondRenderer;
		[SerializeField] Transform Wrapper;

		protected override void SetSize(Vector3 size)
		{
			Wrapper.localScale = size;
		}

		protected override void SetColor(Color color)
		{
			base.SetColor(color);

			SecondRenderer.material.color = color;
		}

		protected override void SetHole(float hole)
		{
			base.SetHole(hole);

			SecondRenderer.material.SetFloat("_Mask", hole);
		}

		protected override void SetVariant(Sprite variant)
		{
			base.SetVariant(variant);

			SecondRenderer.sprite = variant;
        }

		protected override void SetRotation(float rotation)
		{
			Wrapper.localEulerAngles = new Vector3(0, 0, rotation);
		}
	}
}
