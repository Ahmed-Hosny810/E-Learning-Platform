using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Helpers
{
    public class SlugHelper
    {
        public static string Generate(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return string.Empty;

            source = source.ToLowerInvariant().Trim();

            // Replace invalid chars with hyphens
            var sb = new StringBuilder();
            foreach (var c in source)
            {
                if (char.IsLetterOrDigit(c))
                    sb.Append(c);
                else
                    sb.Append('-');
            }

            // Collapse multiple hyphens
            var slug = Regex.Replace(sb.ToString(), "-+", "-").Trim('-');

            return slug;
        }
    }
}
