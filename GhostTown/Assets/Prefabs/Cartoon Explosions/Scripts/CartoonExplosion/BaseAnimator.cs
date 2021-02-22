using UnityEngine;

namespace RavingBots.CartoonExplosion
{
	[RequireComponent(typeof(CartoonExplosionFX))]
	public abstract class BaseAnimator : MonoBehaviour
	{
		protected CartoonExplosionFX FX { get; private set; }

		public virtual void Init()
		{
			FX = GetComponent<CartoonExplosionFX>();
		}

		public abstract void OnPlay();

		public abstract void SetProgress(float p);

		public abstract void OnStop();
	}
}
