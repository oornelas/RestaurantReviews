namespace AwesomeFood.Contracts.Repositories
{
    public interface IPaginationParameters
    {
        int MaximumRecords { get; set; }
        int Offset  { get; set; }
    }
}