using System;
using System.IO;

namespace LootWizard;

public static class FileHelpers
{
    internal static string ResolveImagePath(string relativePath)
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(basePath, relativePath);
    }
}