using Helper.Utility.Enums;
using Helper.Utility.Models;
using System.Security.Cryptography;
using System.Text;

namespace Helper.Utility;

public sealed class Encryption
{
	private readonly byte[] _key;
	private readonly string _passwordHash;
	private readonly string _saltKey;
	private readonly string _viKey;

	public Encryption(byte[] key, string passwordHash, string saltKey, string viKey)
	{
		_key = key ?? throw new ArgumentNullException(nameof(key));
		_passwordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
		_saltKey = saltKey ?? throw new ArgumentNullException(nameof(saltKey));
		_viKey = viKey ?? throw new ArgumentNullException(nameof(viKey));
	}

	public Encryption(EncryptionSettings encryptionSettings)
	{
		_key = encryptionSettings.Key ?? throw new ArgumentNullException(nameof(encryptionSettings.Key));
		_passwordHash = encryptionSettings.PasswordHash ?? throw new ArgumentNullException(nameof(encryptionSettings.PasswordHash));
		_saltKey = encryptionSettings.SaltKey ?? throw new ArgumentNullException(nameof(encryptionSettings.SaltKey));
		_viKey = encryptionSettings.ViKey ?? throw new ArgumentNullException(nameof(encryptionSettings.ViKey));
	}

	public string Encrypt(string item, EncryptionMode mode)
	{
		item = mode switch
		{
			EncryptionMode.ECB => EncryptEcb(item),
			EncryptionMode.CBC => EncryptCbc(item),
			_ => item
		};

		return item;
	}

	public string Decrypt(string item, EncryptionMode mode)
	{
		item = mode switch
		{
			EncryptionMode.ECB => DecryptEcb(item),
			EncryptionMode.CBC => DecryptCbc(item),
			_ => item
		};

		return item;
	}

	#region CBC
	private string EncryptCbc(string item)
	{
		var plainTextBytes = Encoding.UTF8.GetBytes(item);
		var keyBytes = new Rfc2898DeriveBytes(_passwordHash, Encoding.ASCII.GetBytes(_saltKey)).GetBytes(256 / 8);
		var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
		var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(_viKey));

		byte[] cipherTextBytes;

		using var memoryStream = new MemoryStream();

		using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
		{
			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
			cryptoStream.FlushFinalBlock();
			cipherTextBytes = memoryStream.ToArray();
			cryptoStream.Close();
		}

		memoryStream.Close();

		return Convert.ToBase64String(cipherTextBytes);
	}

	private string DecryptCbc(string item)
	{
		var cipherTextBytes = Convert.FromBase64String(item);
		var keyBytes = new Rfc2898DeriveBytes(_passwordHash, Encoding.ASCII.GetBytes(_saltKey)).GetBytes(256 / 8);
		var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };
		var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(_viKey));
		var memoryStream = new MemoryStream(cipherTextBytes);
		var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
		var plainTextBytes = new byte[cipherTextBytes.Length];
		var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

		memoryStream.Close();
		cryptoStream.Close();

		return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
	}
	#endregion

	#region ECB
	private string EncryptEcb(string item)
	{
		if (string.IsNullOrWhiteSpace(item))
			return string.Empty;

		var toEncryptArray = Encoding.UTF8.GetBytes(item);
		var tdes = new TripleDESCryptoServiceProvider { Key = _key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
		var cTransform = tdes.CreateEncryptor();
		var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

		tdes.Clear();

		return Convert.ToBase64String(resultArray, 0, resultArray.Length);
	}

	private string DecryptEcb(string item)
	{
		if (string.IsNullOrWhiteSpace(item))
			return string.Empty;

		var toEncryptArray = Convert.FromBase64String(item);
		var tdes = new TripleDESCryptoServiceProvider { Key = _key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 };
		var cTransform = tdes.CreateDecryptor();
		var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

		tdes.Clear();

		return Encoding.UTF8.GetString(resultArray);
	}
	#endregion
}