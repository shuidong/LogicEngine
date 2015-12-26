//==============================================================================
// Copyright (C) 2015 Peng Guang Hui
// All rights reserved
//
// Create by 彭光辉 at 2015/10/16 20:30:00
// Email: gh.peng@qq.com
//==============================================================================
using System;
using System.Collections;
using System.Collections.Generic;

namespace LogicEngine
{
    public static class UtilRandom
    {
        /// <summary>
        ///   <para>Returns a random point inside a sphere with radius 1 (Read Only).</para>
        /// </summary>
        public static Vector3f insideUnitSphere
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        ///   <para>Returns a random point inside a circle with radius 1 (Read Only).</para>
        /// </summary>
        public static Vector2f insideUnitCircle
        {
            get
            {
                return onUnitCircle * Range01();
            }
        }
        /// <summary>
        ///   <para>Returns a random point on the surface of a sphere with radius 1 (Read Only).</para>
        /// </summary>
        public static Vector2f onUnitCircle
        {
            get
            {
                var angle = Range(0f, 2f * UtilMath.PI);
                return new Vector2f(UtilMath.Cos(angle), UtilMath.Sin(angle));
            }
        }

        /// <summary>
        ///   <para>Returns a random point on the surface of a sphere with radius 1 (Read Only).</para>
        /// </summary>
        public static Vector3f onUnitSphere
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        ///// <summary>
        /////   <para>Returns a random rotation (Read Only).</para>
        ///// </summary>
        //public static Quaternion rotation
        //{
        //    get
        //    {
        //        Quaternion result;
        //        Random.INTERNAL_get_rotation(out result);
        //        return result;
        //    }
        //}

        ///// <summary>
        /////   <para>Returns a random rotation with uniform distribution (Read Only).</para>
        ///// </summary>
        //public static Quaternion rotationUniform
        //{
        //    get
        //    {
        //        Quaternion result;
        //        Random.INTERNAL_get_rotationUniform(out result);
        //        return result;
        //    }
        //}


        /// <summary>
        ///   <para>Generates a random color from HSV and alpha ranges.</para>
        /// </summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <param name="valueMin">Minimum value [0..1].</param>
        /// <param name="valueMax">Maximum value [0..1].</param>
        /// <param name="alphaMin">Minimum alpha [0..1].</param>
        /// <param name="alphaMax">Maximum alpha [0..1].</param>
        /// <returns>
        ///   <para>A random color with HSV and alpha values in the input ranges.</para>
        /// </returns>
        //public static Color ColorHSV()
        //{
        //    return Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f, 1f, 1f);
        //}

        /// <summary>
        ///   <para>Generates a random color from HSV and alpha ranges.</para>
        /// </summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <param name="valueMin">Minimum value [0..1].</param>
        /// <param name="valueMax">Maximum value [0..1].</param>
        /// <param name="alphaMin">Minimum alpha [0..1].</param>
        /// <param name="alphaMax">Maximum alpha [0..1].</param>
        /// <returns>
        ///   <para>A random color with HSV and alpha values in the input ranges.</para>
        /// </returns>
        //public static Color ColorHSV(float hueMin, float hueMax)
        //{
        //    return Random.ColorHSV(hueMin, hueMax, 0f, 1f, 0f, 1f, 1f, 1f);
        //}

        /// <summary>
        ///   <para>Generates a random color from HSV and alpha ranges.</para>
        /// </summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <param name="valueMin">Minimum value [0..1].</param>
        /// <param name="valueMax">Maximum value [0..1].</param>
        /// <param name="alphaMin">Minimum alpha [0..1].</param>
        /// <param name="alphaMax">Maximum alpha [0..1].</param>
        /// <returns>
        ///   <para>A random color with HSV and alpha values in the input ranges.</para>
        /// </returns>
        //public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax)
        //{
        //    return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, 0f, 1f, 1f, 1f);
        //}

        /// <summary>
        ///   <para>Generates a random color from HSV and alpha ranges.</para>
        /// </summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <param name="valueMin">Minimum value [0..1].</param>
        /// <param name="valueMax">Maximum value [0..1].</param>
        /// <param name="alphaMin">Minimum alpha [0..1].</param>
        /// <param name="alphaMax">Maximum alpha [0..1].</param>
        /// <returns>
        ///   <para>A random color with HSV and alpha values in the input ranges.</para>
        /// </returns>
        //public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax)
        //{
        //    return Random.ColorHSV(hueMin, hueMax, saturationMin, saturationMax, valueMin, valueMax, 1f, 1f);
        //}

        /// <summary>
        ///   <para>Generates a random color from HSV and alpha ranges.</para>
        /// </summary>
        /// <param name="hueMin">Minimum hue [0..1].</param>
        /// <param name="hueMax">Maximum hue [0..1].</param>
        /// <param name="saturationMin">Minimum saturation [0..1].</param>
        /// <param name="saturationMax">Maximum saturation[0..1].</param>
        /// <param name="valueMin">Minimum value [0..1].</param>
        /// <param name="valueMax">Maximum value [0..1].</param>
        /// <param name="alphaMin">Minimum alpha [0..1].</param>
        /// <param name="alphaMax">Maximum alpha [0..1].</param>
        /// <returns>
        ///   <para>A random color with HSV and alpha values in the input ranges.</para>
        /// </returns>
        //public static Color ColorHSV(float hueMin, float hueMax, float saturationMin, float saturationMax, float valueMin, float valueMax, float alphaMin, float alphaMax)
        //{
        //    float h = Mathf.Lerp(hueMin, hueMax, Random.value);
        //    float s = Mathf.Lerp(saturationMin, saturationMax, Random.value);
        //    float v = Mathf.Lerp(valueMin, valueMax, Random.value);
        //    Color result = Color.HSVToRGB(h, s, v, true);
        //    result.a = Mathf.Lerp(alphaMin, alphaMax, Random.value);
        //    return result;
        //}

        static Random mRandom = new Random(System.DateTime.Now.Millisecond);

        #region basic random functions
        /// <summary>
        /// [min, max)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int Range(int min, int max)
        {
            return mRandom.Next(min, max);
        }
        /// <summary>
        /// [min, max)
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static float Range(float min, float max)
        {
            return UtilMath.Lerp(min, max, (float)mRandom.NextDouble());
        }
        public static float Range01()
        {
            return (float)mRandom.NextDouble();
        }
        /// <summary>
        /// 正态分布
        /// </summary>
        /// <param name="mu"></param>
        /// <param name="sigma"></param>
        /// <returns></returns>
        public static float Gaussion(double mu = 0, double sigma = 1)
        {
            return (float)mRandom.NextGaussian(mu, sigma);
        }
        public static float PerlinNoise(float x)
        {
            return LogicEngine.PerlinNoise.Generate(x);
        }
        public static float PerlinNoise(float x, float y)
        {
            return LogicEngine.PerlinNoise.Generate(x, y);
        }
        public static float PerlinNoise(float x, float y, float z)
        {
            return LogicEngine.PerlinNoise.Generate(x, y, z);
        }
        #endregion
        #region 洗牌抽牌
        /// <summary>
        /// 洗牌
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        public static void Shuffle<T>(T[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                int r = Range(i, array.Length);
                T temp = array[i];
                array[i] = array[r];
                array[r] = temp;
            }
        }
        /// <summary>
        /// 洗牌
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(List<T> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int r = Range(i, list.Count);
                T temp = list[i];
                list[i] = list[r];
                list[r] = temp;
            }
        }
        public static T Select<T>(T[] array)
        {
            return array[Range(0, array.Length)];
        }
        public static T Select<T>(List<T> list)
        {
            return list[Range(0, list.Count)];
        }
        public static List<T> Select<T>(List<T> list, int count)
        {
            count = UtilMath.Clamp(count, 0, list.Count);
            List<T> result = new List<T>(count);
            return result;
        }
        /// <summary>
        /// Returns n unique random numbers in the range [1, n], inclusive. 
        /// This is equivalent to getting the first n numbers of some random permutation of the sequential numbers from 1 to max. 
        /// Runs in O(k^2) time.
        /// </summary>
        /// <param name="rand"></param>
        /// <param name="n">Maximum number possible.</param>
        /// <param name="k">How many numbers to return.</param>
        /// <returns></returns>
        public static int[] Permutation(this Random rand, int n, int k)
        {
            var result = new List<int>();
            var sorted = new HashSet<int>();

            for (var i = 0; i < k; i++)
            {
                var r = rand.Next(1, n + 1 - i);

                foreach (var q in sorted)
                {
                    if (r >= q) r++;
                }

                result.Add(r);
                sorted.Add(r);
            }
            return result.ToArray();
        }
        public static List<T> Generate<T>(Func<T> generator, int count)
        {
            List<T> list = new List<T>();
            while (count > 0)
            {
                count--;
                list.Add(generator());
            }
            return list;
        }
        //public static T[] Select<T>(T[] array, int count)
        //{
        //    if (count > array.Length)
        //    {
        //        count = array.Length;
        //    }
        //    T[] result = new T[count];
        //    for (int i = 0; i < array.Length - 1; i++)
        //    {
        //    }
        //    return result;
        //}
        #endregion

        #region unique id
        static SnowflakeID mSnowflakeID = new SnowflakeID(0);
        public static long GenID()
        {
            return mSnowflakeID.GenID();
        }
        public static T Dyc<T>() where T : Dyc, new()
        {
            T dyc = new T();
            dyc.id = GenID();
            return dyc;
        }
        public static T Dyc<T>(long id) where T : Dyc, new()
        {
            T dyc = new T();
            dyc.id = id;
            return dyc;
        }
        #endregion
    }
}