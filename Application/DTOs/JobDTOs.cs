namespace FreelancePlatform.Application.Dtos;
//using FreelancePlatform.Infrastructure.Services;
public class JobDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Budget { get; set; }
    public DateTime Deadline { get; set; }
    public string RequiredSkills { get; set; } = string.Empty;
}