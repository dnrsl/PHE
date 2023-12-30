using API.Models;

namespace API.DTOs.Characteristics
{
    public class NewCharacteristicDto
    {
        public Guid Guid { get; set; }
        public string BusinessField { get; set; }
        public string CompanyType { get; set; }

        public static implicit operator Characteristic(NewCharacteristicDto dto)
        {
            return new Characteristic
            {
                Guid = dto.Guid,
                BusinessField = dto.BusinessField,
                CompanyType = dto.CompanyType,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
