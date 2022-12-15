using System.Globalization;
using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

/// <summary>
///     The base entity exception class
/// </summary>
/// <seealso cref="ApiException" />
public abstract class BaseEntityException<TEntity> : ApiException where TEntity : BaseEntity
{
    /// <summary>
    ///     Initializes a new instance of the
    ///     <see>
    ///         <cref>BaseEntityException</cref>
    ///     </see>
    ///     class
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="statusCode">The status code</param>
    protected BaseEntityException(string message, int statusCode) : base(statusCode)
    {
        EntityName = typeof(TEntity).Name;

        var textInfo = CultureInfo.CurrentCulture.TextInfo;
        if (message.Contains("Entity"))
            Message = message.Replace("Entity", textInfo.ToTitleCase(EntityName));
        else if (message.Contains("entity"))
            Message = message.Replace("entity", textInfo.ToTitleCase(EntityName));
        else
            Message = message;
    }

    /// <summary>
    ///     Gets the value of the entity name
    /// </summary>
    public string EntityName { get; }

    /// <summary>
    ///     Gets the value of the message
    /// </summary>
    public override string Message { get; }
}