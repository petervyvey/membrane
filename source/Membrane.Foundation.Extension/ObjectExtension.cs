
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Membrane.Foundation.Extension
{
	/// <summary>
	/// Extension methods to handle extra functionality on objects
	/// </summary>
	public static class ObjectExtension
	{
		/// <summary>
		/// Checks if a given instance of an object has the default value for that objecttype.
		/// </summary>
		/// <param argumentName="value">The object to check.</param>
		/// <returns>TRUE if the object has the default value, FALSE otherwise.</returns>
		public static bool IsDefault<T>(this T value)
		{
            return Comparer<T>.Default.Compare(value, default(T)) == 0;
		}

#if !(SILVERLIGHT)

		/// <summary>
		/// Checks if the given object can be casted to an instance of type T.
		/// </summary>
		/// <typeparam name="T">The type to cast the object to.</typeparam>
		/// <param name="value">The object to cast.</param>
		/// <returns>TRUE if the object can be casted to type T, FALSE otherwise.</returns>
		public static bool CanCastTo<T>(this object value)
		{
			if (value != null)
			{
				Type type = typeof(T);
				TypeConverter converter = TypeDescriptor.GetConverter(value);

				if ((converter != null) && converter.CanConvertTo(type))
				{
					return true;
				}

				converter = TypeDescriptor.GetConverter(type);
				if ((converter != null) && converter.CanConvertFrom(value.GetType()))
				{
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Casts the object to an instance of type T.
		/// </summary>
		/// <typeparam name="T">The type to cast the object to.</typeparam>
		/// <param name="value">The object to cast.</param>
		/// <returns>The object to casted to an instance of type T.</returns>
		public static T CastTo<T>(this object value)
		{
			return value.CastTo<T>(default(T));
		}

		/// <summary>
		/// Casts the object to an instance of type T.
		/// </summary>
		/// <typeparam name="T">The type to cast the object to.</typeparam>
		/// <param name="value">The object to cast.</param>
		/// <param name="defaultValue">The default value if the object is null.</param>
		/// <returns>The object to casted to an instance of type T.</returns>
		public static T CastTo<T>(this object value, T defaultValue)
		{
			if (value != null)
			{
				Type type = typeof(T);
				TypeConverter converter = TypeDescriptor.GetConverter(value);

				if ((converter != null) && converter.CanConvertTo(type))
				{
					return (T)converter.ConvertTo(value, type);
				}

				converter = TypeDescriptor.GetConverter(type);
				if ((converter != null) && converter.CanConvertFrom(value.GetType()))
				{
					return (T)converter.ConvertFrom(value);
				}
			}

			return defaultValue;
		}

#endif

		/// <summary>
		/// Searches an array of values of type T for a given instance.
		/// </summary>
		/// <typeparam name="T">The type of </typeparam>
		/// <param name="instance">The instance of type T to search for.</param>
		/// <param name="values">The array of instances of type T.</param>
		/// <returns>TRUE if the array of values contains the given instance, FALSE otherwise.</returns>
		public static bool EqualsAny<T>(this T instance, params T[] values)
		{
			return (Array.IndexOf<T>(values, instance) != -1);
		}

		/// <summary>
		/// Searches an array of values of type T for a given instance.
		/// </summary>
		/// <typeparam name="T">The type of </typeparam>
		/// <param name="instance">The instance of type T to search for.</param>
		/// <param name="values">The array of instances of type T.</param>
		/// <returns>TRUE if the array of values does not contain the given instance, FALSE otherwise.</returns>
		public static bool EqualsNone<T>(this T instance, params T[] values)
		{
			return !instance.EqualsAny<T>(values);
		}

		/// <summary>
		/// Gets the value of the property specified by the name parameter.
		/// </summary>
		/// <typeparam name="T">The type of the property to retrieve.</typeparam>
		/// <param name="instance">The instance of an object having a property with the specified name.</param>
		/// <param name="name">The name of the property.</param>
		/// <returns>The value of the property.</returns>
		public static T GetProperty<T>(this object instance, string name)
		{
			object value = null;

			try
			{
				PropertyInfo info = instance.GetType().GetProperty(name);
				value = info.GetValue(instance, null);
			}
			catch {/* IGNORE */ }

			return (T)value;
		}

		/// <summary>
		/// Gets the value of the property specified by the name parameter.
		/// </summary>
		/// <typeparam name="T">The type of the property to retrieve.</typeparam>
		/// <param name="instance">The instance of an object having a property with the specified name.</param>
		/// <param name="path">The property path to resolve.</param>
		/// <returns>The value of the property.</returns>
		public static T ResolveProperty<T>(this object instance, string path)
		{
			instance.CatchNullArgument("instance");
			path.CatchNullOrEmptyArgument("path");

			object value = null;

			if (path.Split(".".ToCharArray()).Count() == 1)
			{
				value = instance.GetProperty<T>(path);
			}
			else
			{
				int index = path.IndexOf(".");
				string propertyName = path.Substring(0, index);
				string childPath = path.Substring(index + 1);

				value = instance.GetProperty<object>(propertyName).ResolveProperty<T>(childPath);
			}

			return (T)value;
		}
	}
}
