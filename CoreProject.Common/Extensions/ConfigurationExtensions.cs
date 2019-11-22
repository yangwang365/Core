using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
namespace CoreProject.Common.Extensions
{
    public static class ConfigurationExtensions
    {

        public static string GetSections([NotNull]this IConfiguration configuration, params string[] sections)
        {
            try
            {
                var val = string.Empty;
                for (int i = 0; i < sections.Length; i++)
                {
                    val += sections[i] + ":";
                }

                return configuration[val.TrimEnd(':')];
            }
            catch (Exception)
            {
                return "";
            }
        }

       
    }
}
