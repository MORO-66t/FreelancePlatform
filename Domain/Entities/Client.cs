//namespace cloud.Domain.Entities;

//public class Client : User
//{
//    public string CompanyName { get; private set; } = string.Empty; // Initialize to avoid nullability issue
//    private readonly List<Job> _jobs = new();
//    public IReadOnlyCollection<Job> Jobs => _jobs.AsReadOnly();

//    public Client(string firstName, string lastName, string email,
//        string passwordHash, string companyName)
//        : base(firstName, lastName, email, passwordHash)
//    {
//        CompanyName = companyName;
//    }

//    // Parameterless constructor for EF Core
//    private Client() : base()
//    {
//        CompanyName = string.Empty; // Ensure non-null value for EF Core initialization
//    }
//}
