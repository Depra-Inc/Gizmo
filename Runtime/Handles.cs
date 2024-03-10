// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using UnityEngine;

namespace Depra.Gizmo
{
	public static class Handles
	{
		public static void DrawRay(Vector3 startPoint, Vector3 endPoint, Color color, float distance, float duration)
		{
			var direction = endPoint - startPoint;
			Debug.DrawRay(startPoint, direction * distance, color, duration);
		}

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

			var x = new Vector3(position.x, position.y + radius * Mathf.Sin(0), position.z + radius * Mathf.Cos(0));
			var y = new Vector3(position.x + radius * Mathf.Cos(0), position.y, position.z + radius * Mathf.Sin(0));
			var z = new Vector3(position.x + radius * Mathf.Cos(0), position.y + radius * Mathf.Sin(0), position.z);

			Vector3 newX;
			Vector3 newY;
			Vector3 newZ;

			for (var index = 1; index < 37; index++)
			{
				newX = new Vector3(position.x, position.y + radius * Mathf.Sin(ANGLE * index * Mathf.Deg2Rad),
					position.z + radius * Mathf.Cos(ANGLE * index * Mathf.Deg2Rad));
				newY = new Vector3(position.x + radius * Mathf.Cos(ANGLE * index * Mathf.Deg2Rad), position.y,
					position.z + radius * Mathf.Sin(ANGLE * index * Mathf.Deg2Rad));
				newZ = new Vector3(position.x + radius * Mathf.Cos(ANGLE * index * Mathf.Deg2Rad),
					position.y + radius * Mathf.Sin(ANGLE * index * Mathf.Deg2Rad), position.z);

				Debug.DrawLine(x, newX, color, duration, depthTest);
				Debug.DrawLine(y, newY, color, duration, depthTest);
				Debug.DrawLine(z, newZ, color, duration, depthTest);

				x = newX;
				y = newY;
				z = newZ;
			}
		}
	}
}