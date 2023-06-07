using System;
using System.Security.Cryptography;
using System.Text;

namespace SalesmanCore.Helpers;

/// <summary>
/// Помощник MD5
/// </summary>
public static class HashHelper
{
    #region Методы

    public static bool Compare(string value, string hash)
    {
        return 0 == StringComparer.OrdinalIgnoreCase.Compare(GetHash(value), hash);
    }

    public static string GetHash(string value)
    {
        if (value == null)
        {
            value = string.Empty;
        }

        using var md5 = MD5.Create();
        var inputBytes = Encoding.ASCII.GetBytes(value);
        var hashBytes = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();

        foreach (var b in hashBytes)
        {
            sb.Append(b.ToString("X2"));
        }

        return sb.ToString();
    }

    #endregion
}