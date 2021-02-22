using UnityEngine;

#pragma warning disable 649

namespace RavingBots.CartoonExplosion
{
	public abstract class RadialAnimator : ExplosionAnimator
	{
		public AnimationCurve HoleScale;

		[SerializeField] SpriteRenderer Renderer;

		public override void SetProgress(float p)
		{
			base.SetProgress(p);

			SetHole(HoleScale.Evaluate(p));
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

		protected virtual void SetHole(float hole)
		{
			Renderer.material.SetFloat("_Mask", hole);
		}
	}
}