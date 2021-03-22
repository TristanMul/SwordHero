using UnityEngine;

namespace RavingBots.CartoonExplosion
{
	public class BubbleAnimator : FragmentAnimator
	{
		public Gradient PrimaryColor;
		public Gradient SecondaryColor;

		public override void SetProgress(float p)
		{
			base.SetProgress(p);

			SetPrimaryColor(PrimaryColor.Evaluate(p));
			SetSecondaryColor(SecondaryColor.Evaluate(p));
		}

		void SetPrimaryColor(Color color)
		{
			Renderer.material.color = color;
		}

		void SetSecondaryColor(Color color)
		{
			Renderer.material.SetColor("_SecondColor", color);
		}
	}
}
