using System.Globalization;
using Rusty.Template.Domain;

namespace Rusty.Template.Contracts.Exceptions.Entity;

public abstract class BaseEntityException<TEntity> : ApiException where TEntity : BaseEntity
{
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

    public string EntityName { get; }
    public override string Message { get; }
}