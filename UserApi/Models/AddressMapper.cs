﻿using System.Net.NetworkInformation;

namespace UserApi.Models
{
    public static class AddressMapper
    {
        public static Address ToAddress(this AddressDto address)
        {
            return new Address
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                Country = address.Country,
                ZipCode = address.ZipCode
            };
        }

        public static AddressDto ToAddressDto(this Address address)
        {
            return new AddressDto
            {
                Id = address.Id,
                Street = address.Street,
                City = address.City,
                State = address.State,
                Country = address.Country,
                ZipCode = address.ZipCode
            };
        }
    }
}
