namespace Be.Vlaanderen.Basisregisters.AcmIdm.Abstractions.AuthorizationHandlers
{
    using System.Threading.Tasks;

    public interface IOvoCodeValidator
    {
        Task<bool> Validate(OvoCode ovoCode);
    }
}
