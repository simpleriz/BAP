using System;
using UnityEngine;

public static class EnhancedRandom
{
    private static int _seed;

    // Константы для LCG
    private const int A = 1664525; // Множитель
    private const int C = 1013904223; // Прибавка
    private const int M = int.MaxValue; // Модуль

    // Статический конструктор
    static EnhancedRandom()
    {
        _seed = Environment.TickCount; // Инициализация семени текущим временем
    }

    // Метод для установки семени
    public static void SetSeed(int seed)
    {
        _seed = seed;
    }

    // Генерация случайного числа в заданном диапазоне
    public static int Next(int minValue, int maxValue)
    {
        _seed = (A * _seed + C) % M; // Генерация следующего числа
        return minValue + Mathf.Abs(_seed % (maxValue - minValue)); // Приведение к диапазону
    }

    // Генерация случайного числа с плавающей точкой
    public static float NextFloat()
    {
        _seed = (A * _seed + C) % M; // Генерация следующего числа
        return (float)_seed / M; // Приведение к диапазону [0.0, 1.0)
    }
}
