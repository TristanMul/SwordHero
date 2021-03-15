using UnityEngine;
using UnityEditor;
using System.Collections;

namespace RavingBots.CartoonExplosion
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(FragmentAnimator), true)]
	public class FragmentAnimatorEditor : BaseEditor
	{
		protected override bool Validate()
		{
			var animator = (FragmentAnimator)target;

			var changed = ValidateGreaterThan(ref animator.ParticleCount, 0, 10);
			changed = ValidateGreaterThan(ref animator.ParticleSize, 0f, 1f) || changed;
			changed = ValidateGreaterThan(ref animator.EmitterRadius, 0f, 1f) || changed;

			return changed;
		}
	}
}
