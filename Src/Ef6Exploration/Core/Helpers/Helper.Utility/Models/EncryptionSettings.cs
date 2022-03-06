namespace Helper.Utility.Models;

public sealed class EncryptionSettings
{
	public string EncryptedKey { get; set; }
	public byte[] Key { get; set; }
	public string PasswordHash { get; set; }
	public string SaltKey { get; set; }
	public string ViKey { get; set; }
}