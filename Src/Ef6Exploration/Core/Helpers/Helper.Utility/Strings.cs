using Helper.Utility.Enums;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace Helper.Utility;

public static class Strings
{
    private static readonly Regex InvalidCharsRegex = new Regex(@"[^\u0000-\u007F]", RegexOptions.Compiled);
    private static readonly Regex InvalidRulesRegex = new Regex(@"\.{2,}", RegexOptions.Compiled);
    private static readonly Regex AlphaOnlyRegex = new Regex(@"[^A-Za-z ]", RegexOptions.Compiled);
    private static readonly Regex StartEndRegex = new Regex(@"^[\. ]|[\. ]$", RegexOptions.Compiled);
    private static readonly Regex ExtraSpacesRegex = new Regex(" {2,}", RegexOptions.Compiled);

    public static string Pluralize(this string text)
    {
        //pluralize text
        if (text.Substring(text.Length - 1, 1).ToLower() == "y")
            text = $"{text.Substring(0, text.Length - 1)}ies";

        if (text.Substring(text.Length - 1, 1).ToLower() != "s")
            text += "s";

        return text;
    }

    public static string RemoveSpecialChar(this string item)
    {
        // remove invalid characters and some initial replacements
        var txtResult = ExtraSpacesRegex.Replace(
            InvalidRulesRegex.Replace(
            InvalidCharsRegex.Replace(
            item, string.Empty).Trim()
            , ".")
            , " ");

        // finally, check beginning and end for periods and spaces
        while (StartEndRegex.IsMatch(txtResult))
            txtResult = StartEndRegex.Replace(
            txtResult, string.Empty);

        return txtResult;
    }

    public static string ReplaceAccents(this string item)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var tempBytes = Encoding.GetEncoding("ISO-8859-8").GetBytes(item);
        var asciiStr = Encoding.UTF8.GetString(tempBytes);

        return asciiStr;
    }

    public static string AlphaOnly(this string item)
    {
        var txtResult = AlphaOnlyRegex.Replace(ReplaceAccents(item), string.Empty).Trim();

        while (StartEndRegex.IsMatch(txtResult))
	        txtResult = StartEndRegex.Replace(
		        txtResult, string.Empty);

        return txtResult;
    }

    public static string EncodeBase64(this string item)
    {
        var toEncodeAsBytes = Encoding.ASCII.GetBytes(item);
        return Convert.ToBase64String(toEncodeAsBytes);
    }

    public static SecureString ConvertToSecureString(this string clearString)
    {
        var secureStr = new SecureString();
        if (clearString.Length <= 0) return secureStr;
        foreach (var c in clearString.ToCharArray()) secureStr.AppendChar(c);
        return secureStr;
    }

    public static string NormalizeString(this string item, NormalizationCase normalizationCase = NormalizationCase.Upper)
    {
        return normalizationCase switch
        {
            NormalizationCase.Upper => item.ToUpper().Trim(),
            NormalizationCase.Lower => item.ToLower().Trim(),
            _ => item.Trim()
        };
    }

    public static string GenerateLoremIpsum(int numParagraphs = 1)
    {
        const string generatorUri = "http://loripsum.net/api/{0}";
        return Json.Get(string.Format(generatorUri, numParagraphs));
    }

    public static string ToHexString(this IReadOnlyCollection<byte> bytes)
    {
        var hex = new StringBuilder(bytes.Count * 2);

        foreach (var b in bytes)
        {
            hex.AppendFormat("{0:x2}", b);
        }

        return hex.ToString();
    }

    public static string EnsureValidEmail(this string emailAddress, AllowNulls allowNulls)
    {
	    if (string.IsNullOrWhiteSpace(emailAddress))
	    {
		    if (allowNulls == AllowNulls.True)
		    {
			    return null;
            }

		    throw new ArgumentNullException(nameof(emailAddress), "Email address cannot be null or empty.");
	    }

	    var mailAddress = new MailAddress(emailAddress);
	    return mailAddress.Address;
    }

	public static string EnsureValidUsPhone(this string phoneNumber, AllowNulls allowNulls)
	{
		if (string.IsNullOrWhiteSpace(phoneNumber))
		{
			if (allowNulls == AllowNulls.True)
			{
				return null;
			}

			throw new ArgumentNullException(nameof(phoneNumber), "Phone number cannot be null or empty.");
        }

        var number = Regex.Replace(phoneNumber, "[^0-9]", "");

        if (number.Length != 7 && number.Length != 10)
        {
            throw new ArgumentException("US phone number must be 7 or 10 digits.", nameof(phoneNumber));
        }

        return number;
	}

	public static string ValidateMaxSize(this string data, uint size)
	{
		if (string.IsNullOrWhiteSpace(data))
		{
			return null;
		}

		if (data.Length > size)
		{
			throw new ArgumentException($"String is too large.  Max allowed size is: {size} characters.", nameof(data));
		}

		return data;
	}

	public static string ValidIp(this string ipAddress)
	{
		if (string.IsNullOrWhiteSpace(ipAddress))
		{
			return null;
		}

		try
		{
			return IPAddress.Parse(ipAddress).ToString();
		}
		catch (FormatException)
		{
			throw new ArgumentException("IP Address supplied is not in a valid format.", nameof(ipAddress));
        }
	}
}