// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Runtime.CompilerServices;
using UnityEngine;

namespace Depra.Gizmo
{
	public static class Handles
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration) =>
			Debug.DrawLine(start, end, color, duration);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DrawRay(Vector3 start, Vector3 direction, Color color, float distance, float duration) =>
			Debug.DrawRay(start, direction * distance, color, duration);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void DrawBox(Vector3 position, Quaternion rotation, Vector3 scale, Color color, float duration)
		{
			var matrix = new Matrix4x4();
			matrix.SetTRS(position, rotation, scale);

			var point1 = matrix.MultiplyPoint(new Vector3(-0.5f, -0.5f, 0.5f));
			var point2 = matrix.MultiplyPoint(new Vector3(0.5f, -0.5f, 0.5f));
			var point3 = matrix.MultiplyPoint(new Vector3(0.5f, -0.5f, -0.5f));
			var point4 = matrix.MultiplyPoint(new Vector3(-0.5f, -0.5f, -0.5f));

			var point5 = matrix.MultiplyPoint(new Vector3(-0.5f, 0.5f, 0.5f));
			var point6 = matrix.MultiplyPoint(new Vector3(0.5f, 0.5f, 0.5f));
			var point7 = matrix.MultiplyPoint(new Vector3(0.5f, 0.5f, -0.5f));
			var point8 = matrix.MultiplyPoint(new Vector3(-0.5f, 0.5f, -0.5f));

			Debug.DrawLine(point1, point2, color, duration);
			Debug.DrawLine(point2, point3, color, duration);
			Debug.DrawLine(point3, point4, color, duration);
			Debug.DrawLine(point4, point1, color, duration);

			Debug.DrawLine(point5, point6, color, duration);
			Debug.DrawLine(point6, point7, color, duration);
			Debug.DrawLine(point7, point8, color, duration);
			Debug.DrawLine(point8, point5, color, duration);

			Debug.DrawLine(point1, point5, color, duration);
			Debug.DrawLine(point2, point6, color, duration);
			Debug.DrawLine(point3, point7, color, duration);
			Debug.DrawLine(point4, point8, color, duration);
		}

		public static void DrawWireSphere(Vector3 position, Color color, float radius = 1.0f, float duration = 0,
			bool depthTest = true)
		{
			const float ANGLE = 10.0f;

			var startX = new Vector3(position.x, position.y + radius * Mathf.Sin(0), position.z + radius * Mathf.Cos(0));
			var startY = new Vector3(position.x + radius * Mathf.Cos(0), position.y, position.z + radius * Mathf.Sin(0));
			var startZ = new Vector3(position.x + radius * Mathf.Cos(0), position.y + radius * Mathf.Sin(0), position.z);

			for (var index = 1; index < 37; index++)
			{
				var endX = new Vector3(position.x,
					position.y + radius * Mathf.Sin(ANGLE * index * Mathf.Deg2Rad),
					position.z + radius * Mathf.Cos(ANGLE * index * Mathf.Deg2Rad));
				var endY = new Vector3(position.x + radius * Mathf.Cos(ANGLE * index * Mathf.Deg2Rad), 
					position.y,
					position.z + radius * Mathf.Sin(ANGLE * index * Mathf.Deg2Rad));
				var endZ = new Vector3(position.x + radius * Mathf.Cos(ANGLE * index * Mathf.Deg2Rad),
					position.y + radius * Mathf.Sin(ANGLE * index * Mathf.Deg2Rad), 
					position.z);

				Debug.DrawLine(startX, endX, color, duration, depthTest);
				Debug.DrawLine(startY, endY, color, duration, depthTest);
				Debug.DrawLine(startZ, endZ, color, duration, depthTest);

				startX = endX;
				startY = endY;
				startZ = endZ;
			}
		}
	}
}