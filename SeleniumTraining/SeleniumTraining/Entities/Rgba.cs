using System;
using System.Text.RegularExpressions;

namespace SeleniumTraining.Entities
{
    /// <summary>
    /// Формат цвета в Rgba
    /// Пример:
    /// rgba(51, 51, 51, 1)
    /// </summary>
    internal class Rgba
    {
        internal int Red { get; }
        internal int Green { get; }
        internal int Blue { get; }
        internal double Transparency { get; }

        internal Rgba(string rgba)
        {
            Regex rgx = new Regex(@"rgb(a)?\(\d+\, \d+\, \d+(\, \d\.?\d*)?\)");
            if (!rgx.IsMatch(rgba))
            {
                throw new ArgumentException($"Некорректная строка - {rgba}. Формат цвета должен быть в виде rgba(51, 51, 51, 1)");
            }

            int start = rgba.IndexOf("(") + 1;
            int end = rgba.Length - 1 - start;
            string[] parameters = rgba.Substring(start, end).Split(',');

            Red = int.Parse(parameters[0]);
            Green = int.Parse(parameters[1]);
            Blue = int.Parse(parameters[2]);
            Transparency = parameters.Length > 3 ? double.Parse(parameters[3]) : 1;
        }

        /// <summary>
        /// Цвет серый?
        /// можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B
        /// </summary>
        /// <returns></returns>
        internal bool IsGrey()
        {
            return (Red == Green) && (Red == Blue) && (Red > 0);
        }

        /// <summary>
        /// Цвет красный?
        /// можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения
        /// </summary>
        /// <returns></returns>
        internal bool IsRed()
        {
            return (Red > 0) && (Blue == 0) && (Green == 0);
        }
    }
}