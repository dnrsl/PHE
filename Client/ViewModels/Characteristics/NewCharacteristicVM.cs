using Client.Models;

namespace Client.ViewModels.Characteristics
{
    public class NewCharacteristicVM
    {
        public Guid Guid { get; set; }
        public string BusinessField { get; set; }
        public string CompanyType { get; set; }

        public static implicit operator Characteristic(NewCharacteristicVM dto)
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
