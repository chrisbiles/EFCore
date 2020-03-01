using System;

namespace Models.Interfaces.Internal
{
	public interface IBase
	{
		DateTime Created { get; set; }
		DateTime LastModifiedDateTime { get; set; }
	}
}