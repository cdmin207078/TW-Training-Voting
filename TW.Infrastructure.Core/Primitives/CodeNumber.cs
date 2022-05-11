using System.Text.RegularExpressions;

namespace TW.Infrastructure.Core.Primitives;

public class CodeNumber : AbstractPrimitiveObject<string>
{
    /// <summary>
    /// 代码/编码/编号
    /// AA-01
    /// AA_01
    /// AA
    /// </summary>
    private static readonly string REGEX_CODENUMBER_SCHEME = @"^[A-Za-z0-9_\-]$";

    public CodeNumber(string value) : base(value) { }

    protected override string TryGetValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CodeNumber cannot be null or empty.");

        value = value.Trim();

        if (!Regex.IsMatch(value, REGEX_CODENUMBER_SCHEME))
            throw new ArgumentException("CodeNumber is illegal");

        return value;
    }
}