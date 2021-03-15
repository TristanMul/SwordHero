using UnityEngine;
using UnityEditor;
using System.Collections;

namespace RavingBots.CartoonExplosion
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(CartoonExplosionFX))]
	public class CartoonExplosionFXEditor : BaseEditor
	{
		protected override bool Validate()
		{
			var fx = (CartoonExplosionFX)target;

			var changed = ValidateGreaterThan(ref fx.Duration, 0f, 1f);
			changed = ValidateGreaterThan(ref fx.Size, 0f, 1f) || changed;

			return changed;
		}
	}
}
