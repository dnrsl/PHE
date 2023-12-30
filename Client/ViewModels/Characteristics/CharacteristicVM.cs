using Client.Models;

namespace Client.ViewModels.Characteristics
{
    public class CharacteristicVM
    {
        public Guid Guid { get; set; }
        public string BusinessField { get; set; }
        public string CompanyType { get; set; }

        public static implicit operator Characteristic(CharacteristicVM dto)
        {
            return new Characteristic
            {
                Guid = dto.Guid,
                BusinessField = dto.BusinessField,
                CompanyType = dto.CompanyType,
                ModifiedDate = DateTime.Now
            };
        }

        public static explicit operator CharacteristicVM(Characteristic characteristic)
        {
            return new CharacteristicVM
            {
                Guid = characteristic.Guid,
                BusinessField = characteristic.BusinessField,
                CompanyType = characteristic.CompanyType
            };
        }
    }
}
