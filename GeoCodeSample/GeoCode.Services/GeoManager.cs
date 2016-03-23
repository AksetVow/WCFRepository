using GeoCode.Contracts;
using GeoLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCode.Services
{
    public class GeoManager : IGeoService
    {
        IZipCodeRepository _zipCodeRepository = null;
        IStateRepository _stateRepository = null;


        public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository) 
        {
            _stateRepository = stateRepository ?? new StateRepository();
            _zipCodeRepository = zipCodeRepository ?? new ZipCodeRepository();
        }




        public ZipCodeData GetZipInfo(string zip)
        {
            ZipCodeData zipCodeData = null;

            ZipCode zipCodeEntity = _zipCodeRepository.GetByZip(zip);

            if (zipCodeEntity != null)
            {
                zipCodeData = new ZipCodeData()
                {
                    City = zipCodeEntity.City,
                    State = zipCodeEntity.State.Abbreviation,
                    ZipCode = zipCodeEntity.Zip
                };
            }

            return zipCodeData;
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            List<string> stateData = new List<string>();

            IEnumerable<State> states = _stateRepository.Get(primaryOnly);

            if (states != null)
            {
                stateData = states.Select(x => x.Abbreviation).ToList();
            }

            return stateData;
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            List<ZipCodeData> zips = new List<ZipCodeData>();

            var zipFromRepo = _zipCodeRepository.GetByState(state);
            if (zipFromRepo != null)
            {
                foreach (var zipCode in zipFromRepo)
                {
                    zips.Add(new ZipCodeData()
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }


            return zips;
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            List<ZipCodeData> zips = new List<ZipCodeData>();

            ZipCode zipEntity = _zipCodeRepository.GetByZip(zip);

            var zipFromRepo = _zipCodeRepository.GetZipsForRange(zipEntity, range);
            if (zipFromRepo != null)
            {
                foreach (var zipCode in zipFromRepo)
                {
                    zips.Add(new ZipCodeData()
                    {
                        City = zipCode.City,
                        State = zipCode.State.Abbreviation,
                        ZipCode = zipCode.Zip
                    });
                }
            }

            return zips;
        }
    }
}
