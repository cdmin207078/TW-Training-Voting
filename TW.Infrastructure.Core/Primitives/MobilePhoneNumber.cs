using System.Text.RegularExpressions;

namespace TW.Infrastructure.Core.Primitives;

public class MobilePhoneNumber : AbstractPrimitiveObject<string>
{
    /// <summary>
    /// 手机号码
    /// 移动: 134(0-8), 135, 136, 137, 138, 139, 147, 150, 151, 152, 157, 158, 159, 178, 182, 183, 184, 187, 188, 198</p>
    /// 联通: 130, 131, 132, 145, 155, 156, 166, 171, 175, 176, 185, 186</p>
    /// 电信: 133, 153, 173, 177, 180, 181, 189, 199</p>
    /// global star: 1349</p>
    /// virtual operator: 170</p>
    /// </summary>
    private static readonly string REGEX_MOBILE_SCHEME =
        @"^((13[0-9])|(14[5,7,9])|(15[0-3,5-9])|(16[6])|(17[0,1,3,5-8])|(18[0-9])|(19[8,9]))\d{8}$";

    public MobilePhoneNumber(string value) : base(value)
    {
    }

    protected override string TryGetValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("MobilPhoneNumber cannot be null or empty.");

        value = value.Trim();

        if (!Regex.IsMatch(value, REGEX_MOBILE_SCHEME))
            throw new ArgumentException("MobilPhoneNumber is illegal");

        return value;
    }
}

