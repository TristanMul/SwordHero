using UnityEngine;

namespace RavingBots.CartoonExplosion
{
	public abstract class ExplosionAnimator : BaseAnimator
	{
		public float LocalSize = 1f;

		[Range(0f, 180f)] public float RandomizeRotation;
		public AnimationCurve SizeScale;
		public AnimationCurve ScaleRatio;
		public Sprite[] Variants;
		public bool VariantsRandomOrder = true;
		public Gradient MainColor;

		int _currentVariant = -1;

		public override void OnPlay()
		{
			if (RandomizeRotation > 0f)
				SetRotation(RandomizeRotation * Random.Range(-1f, 1f));

			if (Variants != null && Variants.Length > 0)
			{
				if (VariantsRandomOrder)
					SetVariant(Variants[Random.Range(0, Variants.Length)]);
				else
				{
					_currentVariant = (_currentVariant + 1) % Variants.Length;
					SetVariant(Variants[_currentVariant]);
				}
			}
		}

		public override void SetProgress(float p)
		{
			var ratio = ScaleRatio.Evaluate(p);
			var scale = new Vector3(2f * ratio, 2f * (1f - ratio), 1f);

			SetSize(SizeScale.Evaluate(p) * LocalSize * FX.Size * scale);
			SetColor(MainColor.Evaluate(p));
		}

		protected abstract void SetSize(Vector3 size);

		protected abstract void SetColor(Color color);

		protected abstract void SetRotation(float rotation);

		protected abstract void SetVariant(Sprite variant);

		public override void OnStop()
		{ }
	}
}
