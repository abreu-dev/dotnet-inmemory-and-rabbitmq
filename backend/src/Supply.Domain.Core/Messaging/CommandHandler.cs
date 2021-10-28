using FluentValidation.Results;
using Supply.Domain.Core.Data;
using Supply.Domain.Core.Domain;
using System.Threading.Tasks;

namespace Supply.Domain.Core.Messaging
{
    public abstract class CommandHandler
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        protected async Task<bool> Commit(IUnitOfWork uow)
        {
            if (!await uow.Commit())
            {
                AddError(DomainMessages.CommitFailed.Message);
                return false;
            }

            return true;
        }
    }
}
