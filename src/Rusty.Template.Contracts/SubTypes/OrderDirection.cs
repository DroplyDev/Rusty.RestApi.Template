#region

using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;
using System.Runtime.Serialization;


#endregion

namespace Rusty.Template.Contracts.SubTypes;


/// <summary>
/// Order Direction enum
/// </summary>
public enum OrderDirection
{
	/// <summary>The ascending direction</summary>
	Asc,
	/// <summary>The descending direction</summary>
	Desc
}