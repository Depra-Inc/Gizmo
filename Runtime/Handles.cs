// SPDX-License-Identifier: Apache-2.0
// © 2023-2024 Nikolay Melnikov <n.melnikov@depra.org>

using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Depra.Gizmo
{
	public static class Handles
	{
		[Conditional("DEBUG")]
		public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration) =>
			Debug.DrawLine(start, end, color, duration);

		[Conditional("DEBUG")]
		public static void DrawLines(Vector3[] points, Color color, float duration)
		{
			for (var index = 0; index < points.Length - 1; index++)
			{
				DrawLine(points[index], points[index + 1], color, duration);
			}
		}
		
		[Conditional("DEBUG")]
		public static void DrawRay(Vector3 start, Vector3 direction, Color color, float duration) =>
			Debug.DrawRay(start, direction, color, duration);

		[Conditional("DEBUG")]
		public static void DrawRay(Vector3 start, Vector3 direction, Color color, float distance, float duration) =>
			Debug.DrawRay(start, direction * distance, color, duration);

		[Conditional("DEBUG")]
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

			DrawLine(point1, point2, color, duration);
			DrawLine(point2, point3, color, duration);
			DrawLine(point3, point4, color, duration);
			DrawLine(point4, point1, color, duration);

			DrawLine(point5, point6, color, duration);
			DrawLine(point6, point7, color, duration);
			DrawLine(point7, point8, color, duration);
			DrawLine(point8, point5, color, duration);

			DrawLine(point1, point5, color, duration);
			DrawLine(point2, point6, color, duration);
			DrawLine(point3, point7, color, duration);
			DrawLine(point4, point8, color, duration);
		}

		[Conditional("DEBUG")]
		public static void DrawWireSphere(Vector3 position, Color color, float radius = 1.0f, float duration = 0)
		{
			const float ANGLE = 10.0f;

			var startX = new Vector3(position.x,
				position.y + radius * Mathf.Sin(0),
				position.z + radius * Mathf.Cos(0));
			var startY = new Vector3(position.x + radius * Mathf.Cos(0),
				position.y,
				position.z + radius * Mathf.Sin(0));
			var startZ = new Vector3(position.x + radius * Mathf.Cos(0),
				position.y + radius * Mathf.Sin(0),
				position.z);

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

				DrawLine(startX, endX, color, duration);
				DrawLine(startY, endY, color, duration);
				DrawLine(startZ, endZ, color, duration);

				startX = endX;
				startY = endY;
				startZ = endZ;
			}
		}

		[Conditional("DEBUG")]
		public static void DrawWireCircle(Vector3 origin, Vector3 direction, float radius, Color color,
			float duration = 0, int segments = 32)
		{
			var normal = direction * radius;
			var left = Vector3.Cross(normal, Vector3.up).normalized;
			var up = Vector3.Cross(left, normal).normalized;
			if (Mathf.Approximately(left.sqrMagnitude, 0f))
			{
				left = Vector3.left;
				up = Vector3.forward;
			}

			for (var index = 0; index < segments; index++)
			{
				var theta0 = 2f * Mathf.PI * index / segments;
				var theta1 = 2f * Mathf.PI * (index + 1) / segments;

				var x0 = radius * Mathf.Cos(theta0);
				var y0 = radius * Mathf.Sin(theta0);
				var x1 = radius * Mathf.Cos(theta1);
				var y1 = radius * Mathf.Sin(theta1);

				var startPoint = origin + left * x0 + up * y0;
				var endPoint = origin + left * x1 + up * y1;

				DrawLine(startPoint, endPoint, color, duration);
			}
		}

		[Conditional("DEBUG")]
		public static void DrawWireArc(Vector3 position, Vector3 direction, float angle, float radius,
			Color color, float duration = 0, float segments = 32)
		{
			var startPoint = position;
			var stepAngles = angle / segments;
			var currentAngle = AnglesFromDirection() - angle / 2;

			for (var index = 0; index <= segments; index++)
			{
				var radians = Mathf.Deg2Rad * currentAngle;
				var endPoint = position;
				endPoint += new Vector3(radius * Mathf.Cos(radians), 0, radius * Mathf.Sin(radians));
				DrawLine(startPoint, endPoint, color, duration);

				currentAngle += stepAngles;
				startPoint = endPoint;
			}

			DrawLine(startPoint, position, color, duration);

			float AnglesFromDirection()
			{
				var positionLimit = position + direction;
				return Mathf.Rad2Deg * Mathf.Atan2(positionLimit.z - position.z, positionLimit.x - position.x);
			}
		}
	}
}