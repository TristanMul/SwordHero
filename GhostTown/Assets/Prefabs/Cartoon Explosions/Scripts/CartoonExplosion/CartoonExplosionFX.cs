using UnityEngine;

namespace RavingBots.CartoonExplosion
{
	public class CartoonExplosionFX : MonoBehaviour
	{
		public bool PlayOnStart;
		public bool Loop;
		public float Duration = 1f;
		public float Size = 1f;

		public float Progress { get; private set; }
		public bool IsPlaying { get { return _startTime >= 0 && Progress < 1f; } }

		BaseAnimator[] _animators;
		float _startTime = -1f;

		void Awake()
		{
			_animators = GetComponents<BaseAnimator>();
			foreach (var ea in _animators)
			{
				ea.Init();
				ea.SetProgress(0f);
			}
		}

		void Start()
		{
			if (PlayOnStart)
				Play();
		}

		[ContextMenu("Play")]
		public void Play()
		{
			Play(Time.time);
		}

		public void Play(float startTime)
		{
			if (IsPlaying)
				foreach (var ea in _animators)
					ea.OnStop();

			_startTime = startTime;

			foreach (var ea in _animators)
				ea.OnPlay();

			Update();
		}

		[ContextMenu("Stop")]
		public void Stop()
		{
			Progress = 0f;

			foreach (var ea in _animators)
			{
				ea.SetProgress(Progress);
				ea.OnStop();
			}

			_startTime = -1f;
		}

		public void Update()
		{
			if (_startTime < 0f)
				return;

			var p = Mathf.Clamp01((Time.time - _startTime) / Duration);

			if (Mathf.Approximately(Progress, p))
			{
				if (Loop && p >= 1f)
					Play();

				return;
			}

			Progress = p;

			foreach (var ea in _animators)
				ea.SetProgress(Progress);
		}
	}
}
