using UnityEngine;

namespace RavingBots.CartoonExplosion
{
	public abstract class FragmentAnimator : BaseAnimator
	{
		public enum RotationMode { None, TowardsSpeed, Random }

		[SerializeField] protected ParticleSystem ParticleSystem;

		public int ParticleCount = 10;
		public float ParticleSize = 1f;
		public float EmitterRadius = 1f;
		[Range(0f, 1f)] public float StartDelay = 0f;
		[Range(0f, 1f)] public float MaxLifetime = 1f;
		[Range(0f, 1f)] public float RandomizeLifetime = 0f;
		public float StartSpeed = 1f;
		[Range(0f, 1f)] public float RandomizeSpeed = 0f;
		public RotationMode StartRotation;

		protected ParticleSystemRenderer Renderer { get; private set; }
		protected ParticleSystem.Particle[] Particles;
		protected ParticleSystem.MainModule MainModule;

		public override void Init()
		{
			base.Init();

			Renderer = ParticleSystem.GetComponent<ParticleSystemRenderer>();
			Particles = new ParticleSystem.Particle[ParticleCount];
			MainModule = ParticleSystem.main;
		}

		public override void OnPlay()
		{
			ParticleSystem.transform.localScale = FX.Size * EmitterRadius * Vector3.one;
			MainModule.startSize = FX.Size * ParticleSize;
			MainModule.startLifetime = FX.Duration * (1f - StartDelay) * MaxLifetime;
			MainModule.startSpeed = FX.Size * StartSpeed / FX.Duration;
			ParticleSystem.Emit(ParticleCount);

			if (Particles.Length != ParticleCount)
				Particles = new ParticleSystem.Particle[ParticleCount];

			ParticleSystem.GetParticles(Particles);

			for (var i = 0; i < Particles.Length; i++)
			{
				if (RandomizeLifetime > 0f)
					Particles[i].startLifetime *= (1f - Random.value * RandomizeLifetime);

				if (RandomizeSpeed > 0f)
					Particles[i].velocity *= (1f - Random.value * RandomizeSpeed);

				if (StartRotation == RotationMode.TowardsSpeed)
					Particles[i].rotation = GetAngle(Particles[i].velocity);
				else if (StartRotation == RotationMode.Random)
					Particles[i].rotation = Random.value * 360f;
			}

			ParticleSystem.SetParticles(Particles, Particles.Length);

			ParticleSystem.Pause(false);
		}

		float GetAngle(Vector2 v)
		{
			return Vector2.Angle(Vector2.up, v) * Mathf.Sign(Vector2.Dot(v, Vector2.right));
		}

		public override void SetProgress(float p)
		{
			if (p < 1f && ParticleSystem.isPaused && p >= StartDelay)
			{
				var forwardTime = (p - StartDelay) * FX.Duration;
				ParticleSystem.Simulate(forwardTime, false, false);
				ParticleSystem.Play();
			}
		}

		public override void OnStop()
		{
			ParticleSystem.Clear();
			ParticleSystem.Stop();
		}
	}
}
