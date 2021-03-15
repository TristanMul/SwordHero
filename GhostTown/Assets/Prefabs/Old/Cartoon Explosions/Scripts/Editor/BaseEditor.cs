using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace RavingBots.CartoonExplosion
{
	public abstract class BaseEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			if (Validate())
				EditorUtility.SetDirty(this);
		}

		protected abstract bool Validate();

		protected static bool ValidateGreaterThan<T>(ref T value, T threshold, T defaultValue) where T : IComparable<T>
		{
            if (value.CompareTo(threshold) <= 0)
			{
				value = defaultValue;
				return true;
			}
			return false;
		}
	}
}
