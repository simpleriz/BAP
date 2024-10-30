using System;
using UnityEngine;

public static class EnhancedRandom
{
    private static int _seed;

    // ��������� ��� LCG
    private const int A = 1664525; // ���������
    private const int C = 1013904223; // ��������
    private const int M = int.MaxValue; // ������

    // ����������� �����������
    static EnhancedRandom()
    {
        _seed = Environment.TickCount; // ������������� ������ ������� ��������
    }

    // ����� ��� ��������� ������
    public static void SetSeed(int seed)
    {
        _seed = seed;
    }

    // ��������� ���������� ����� � �������� ���������
    public static int Next(int minValue, int maxValue)
    {
        _seed = (A * _seed + C) % M; // ��������� ���������� �����
        return minValue + Mathf.Abs(_seed % (maxValue - minValue)); // ���������� � ���������
    }

    // ��������� ���������� ����� � ��������� ������
    public static float NextFloat()
    {
        _seed = (A * _seed + C) % M; // ��������� ���������� �����
        return (float)_seed / M; // ���������� � ��������� [0.0, 1.0)
    }
}
