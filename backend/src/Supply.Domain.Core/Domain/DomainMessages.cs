namespace Supply.Domain.Core.Domain
{
    public class DomainMessages
    {
        public static DomainMessage CommitFailed => new DomainMessage("Houve um erro ao salvar os dados.");
        public static DomainMessage RequiredField => new DomainMessage("Por favor, certifique-se de informar o campo {0}.");
        public static DomainMessage AlreadyInUse => new DomainMessage("O campo {0} informado já está em uso.");
        public static DomainMessage InvalidFormat => new DomainMessage("O campo {0} informado é inválido.");
        public static DomainMessage NotFound => new DomainMessage("O campo {0} informado não foi encontrado.");
        public static DomainMessage InUseByAnotherEntity => new DomainMessage("O campo {0} informado é usado em {1}.");
    }
}
