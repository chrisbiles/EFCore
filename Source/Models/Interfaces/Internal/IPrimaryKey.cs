using System;

namespace Models.Interfaces.Internal
{
	public interface IPrimaryKey : IBase
	{
		Guid Id { get; set; }
	}
}