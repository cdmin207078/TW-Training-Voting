using System.Text.RegularExpressions;
using TW.Infrastructure.Core.Exceptions;

namespace TW.Infrastructure.Core.Primitives;

public class CodeNumber : AbstractPrimitiveObject<string>
{
    /// <summary>
    /// 代码/编码/编号
    /// AA-01
    /// AA_01
    /// AA
    /// </summary>
    private static readonly string REGEX_CODENUMBER_SCHEME = @"^[A-Za-z0-9_\-]+$";

    public CodeNumber(string value) : base(value) { }

    protected override string TryGetValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new TWException("CodeNumber cannot be null or empty.");

        value = value.Trim();

        if (!Regex.IsMatch(value, REGEX_CODENUMBER_SCHEME))
            throw new TWException($"CodeNumber is illegal. can't set {value}");

        return value;
    }
}