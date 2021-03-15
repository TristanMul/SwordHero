using UnityEngine;
using UnityEditor;
using System.Collections;

namespace RavingBots.CartoonExplosion
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(ExplosionAnimator), true)]
	public class ExplosionAnimatorEditor : BaseEditor
	{
		protected override bool Validate()
		{
			var animator = (ExplosionAnimator)target;

			return ValidateGreaterThan(ref animator.LocalSize, 0f, 1f);
		}
	}
}
