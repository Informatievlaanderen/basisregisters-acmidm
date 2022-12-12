namespace Be.Vlaanderen.Basisregisters.AcmIdm.AuthorizationHandlers
{
    using System.Threading.Tasks;

    public interface IOvoCodeValidator
    {
        Task<bool> Validate(OvoCode ovoCode);
    }
}
