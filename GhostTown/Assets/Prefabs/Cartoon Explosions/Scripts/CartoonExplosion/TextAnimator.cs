using UnityEngine;

#pragma warning disable 649

namespace RavingBots.CartoonExplosion
{
	public class TextAnimator : ExplosionAnimator
	{
		public Gradient OutlineColor;

		[SerializeField] SpriteRenderer Renderer;

		public override void SetProgress(float p)
		{
			base.SetProgress(p);

			SetOutline(OutlineColor.Evaluate(p));
		}

		protected override void SetSize(Vector3 size)
		{
			Renderer.transform.localScale = size;
		}

		protected override void SetColor(Color color)
		{
			Renderer.material.color = color;
		}

		protected override void SetRotation(float rotation)
		{
			Renderer.transform.localEulerAngles = new Vector3(0, 0, rotation);
		}

		protected override void SetVariant(Sprite variant)
		{
			Renderer.sprite = variant;
		}

		protected void SetOutline(Color color)
		{
			Renderer.material.SetColor("_SecondColor", color);
		}
	}
}