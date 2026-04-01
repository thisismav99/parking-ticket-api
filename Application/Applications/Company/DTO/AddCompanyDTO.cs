using Application.DTO;

namespace Application.Applications.Company.DTO
{
    public class AddCompanyDTO : BaseDTO
    {
        public required string Name { get; set; }

        public string? Description { get; set; }
    }
}
