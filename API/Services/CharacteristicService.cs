using API.Contracts;
using API.DTOs.Characteristics;
using API.Models;

namespace API.Services
{
    public class CharacteristicService
    {
        private readonly ICharacteristicRepository _characteristicRepository;

        public CharacteristicService(ICharacteristicRepository characteristicRepository)
        {
            _characteristicRepository = characteristicRepository;
        }

        public IEnumerable<CharacteristicDto> GetAll()
        {
            var characteristics = _characteristicRepository.GetAll();
            if (!characteristics.Any())
            {
                return Enumerable.Empty<CharacteristicDto>();
            }

            var characteristicDtos = new List<CharacteristicDto>();
            foreach (var characteristic in characteristics)
            {
                characteristicDtos.Add((CharacteristicDto)characteristic);
            }

            return characteristicDtos;
        }

        public CharacteristicDto? GetByGuid(Guid guid)
        {
            var characteristicDto = _characteristicRepository.GetByGuid(guid);
            if (characteristicDto is null)
            {
                return null;
            }
            return (CharacteristicDto)characteristicDto;
        }

        public CharacteristicDto? Create(NewCharacteristicDto newCharacteristicDto)
        {
            var characteristicDto = _characteristicRepository.Create(newCharacteristicDto);
            if(characteristicDto is null)
            {
                return null;
            }

            return (CharacteristicDto)characteristicDto;
        }

        public int Update(CharacteristicDto characteristicDto)
        {
            var characteristic = _characteristicRepository.GetByGuid(characteristicDto.Guid);
            if (characteristic is null)
            {
                return -1;
            }

            Characteristic toUpdate = characteristicDto;
            toUpdate.CreatedDate = characteristic.CreatedDate;
            var result = _characteristicRepository.Update(toUpdate);

            return result ? 1 : 0;
        }

        public int Delete (Guid guid)
        {
            var characteristic = _characteristicRepository.GetByGuid(guid);
            if( characteristic is null)
            {
                return -1;
            }

            var result = _characteristicRepository.Delete(characteristic);
            return result ? 1 : 0;
        }
    }
}
