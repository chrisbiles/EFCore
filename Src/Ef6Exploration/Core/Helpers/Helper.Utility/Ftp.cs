using Helper.Utility.Enums;
using System.Net;
using System.Text;

namespace Helper.Utility;
public sealed class Ftp
{
	private const string FtpUriFormatWithPath = "ftp://{0}/{1}/{2}";
	private const string FtpUriFormatWithoutFileName = "ftp://{0}/{1}/";

	private readonly string _ftpUserName;
	private readonly string _ftpPassword;
	private readonly string _serverName;
	private string _serverUri;
	private const int BufferSize = 2048;

	public Ftp(string ftpUserName, string ftpPassword, string serverName)
	{
		_ftpUserName = ftpUserName;
		_ftpPassword = ftpPassword;
		_serverName = serverName;
	}

	public List<string> GetDirectoryList(string folderName)
	{
		var files = new List<string>();
		_serverUri = string.Format(FtpUriFormatWithoutFileName, _serverName, folderName);
		var request = FtpWebRequest(_serverUri, FileTransferMethod.ListDirectory);
		var response = (FtpWebResponse)request.GetResponse();
		var responseStream = response.GetResponseStream();
		if (responseStream == null) return null;

		using (var reader = new StreamReader(responseStream))
		{
			while (!reader.EndOfStream)
			{
				var line = reader.ReadLine();
				if (line != null && (line.Trim().Length > 2)) files.Add(line);
			}
		}

		response.Close();
		return files;
	}

	public long GetFileSize(string folder, string fileName)
	{
		_serverUri = string.Format(FtpUriFormatWithPath, _serverName, folder, fileName);
		var request = FtpWebRequest(_serverUri, FileTransferMethod.FileSize);
		var response = (FtpWebResponse)request.GetResponse();
		var responseStream = response.GetResponseStream();

		if (responseStream == null) return 0;

		var fileSize = response.ContentLength;
		responseStream.Close();
		response.Close();

		return fileSize;
	}

	public void UploadFile(string folder, string fileName, TextReader sourceStream)
	{
		_serverUri = string.Format(FtpUriFormatWithPath, _serverName, folder, fileName);

		var content = CreateContent(sourceStream);
		var request = FtpWebRequest(_serverUri, FileTransferMethod.Upload, content.LongLength);

		using (var strm = request.GetRequestStream())
		{
			strm.Write(content, 0, content.Length);
		}

		var response = (FtpWebResponse)request.GetResponse();
		response.Close();
	}

	public void DeleteFile(string folder, string fileName)
	{
		_serverUri = string.Format(FtpUriFormatWithPath, _serverName, folder, fileName);

		var request = FtpWebRequest(_serverUri, FileTransferMethod.Delete);
		var response = (FtpWebResponse)request.GetResponse();

		response.Close();
	}

	public void DownloadFileToLocalDirectory(string folder, string fileName, string filePath, string finalName)
	{
		var outputStream = new FileStream($"{filePath}\\{finalName}", FileMode.Create);

		_serverUri = string.Format(FtpUriFormatWithPath, _serverName, folder, fileName);

		var request = FtpWebRequest(_serverUri, FileTransferMethod.Download);
		var response = (FtpWebResponse)request.GetResponse();
		var ftpStream = response.GetResponseStream();
		var buffer = new byte[BufferSize];

		if (ftpStream != null)
		{
			var readCount = ftpStream.Read(buffer, 0, BufferSize);

			while (readCount > 0)
			{
				outputStream.Write(buffer, 0, readCount);
				readCount = ftpStream.Read(buffer, 0, BufferSize);
			}
		}

		ftpStream?.Close();
		outputStream.Close();
		response.Close();
	}

	private static byte[] CreateContent(TextReader source)
	{
		var sourceContent = Encoding.UTF8.GetBytes(source.ReadToEnd());

		source.Close();

		return sourceContent;
	}

	private FtpWebRequest FtpWebRequest(string serverUri, FileTransferMethod method, long contentLength = 0)
	{
		var request = (FtpWebRequest)WebRequest.Create(serverUri);

		request.Credentials = new NetworkCredential(_ftpUserName, _ftpPassword);
		request.KeepAlive = false;
		request.UseBinary = true;
		request.UsePassive = true;

		if (contentLength > 0) request.ContentLength = contentLength;

		request.Method = method switch
		{
			FileTransferMethod.Upload => WebRequestMethods.Ftp.UploadFile,
			FileTransferMethod.Download => WebRequestMethods.Ftp.DownloadFile,
			FileTransferMethod.Delete => WebRequestMethods.Ftp.DeleteFile,
			FileTransferMethod.ListDirectory => WebRequestMethods.Ftp.ListDirectory,
			FileTransferMethod.FileSize => WebRequestMethods.Ftp.GetFileSize,
			_ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
		};

		return request;
	}
}
