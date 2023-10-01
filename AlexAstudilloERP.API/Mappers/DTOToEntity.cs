using AlexAstudilloERP.API.DTOs;
using AlexAstudilloERP.API.DTOs.Requests;
using AlexAstudilloERP.Domain.Entities.Public;

namespace AlexAstudilloERP.API.Mappers;

public static class DTOToEntity
{
    public static Customer CustomerRequestDTOToCustomer(CustomerRequestDTO dto)
    {
        //List<Address> addresses = new();
        //List<Phone> phones = new();
        List<Email> emails = new();
        if (dto.Email != null) emails.Add(new() { Mail = dto.Email });
        // Check if exists address.
        if (dto.Address != null)
        {
            //addresses.Add(new()
            //{
            //    PoliticalDivisionId = dto.Address.PoliticalDivisionId,
            //    MainStreet = dto.Address.MainStreet,
            //    SecondaryStreet = dto.Address.SecondaryStreet,
            //    HouseNumber = dto.Address.HouseNumber,
            //    PostalCode = dto.Address.PostalCode,
            //    Latitude = dto.Address.Latitude,
            //    Longitude = dto.Address.Longitude,
            //});
        }
        // Check if exists phone
        //if (dto.Phone != null)
        //{
        //    phones.Add(new()
        //    {
        //        DialInCodeId = dto.Phone.DialInCodeId,
        //        PhoneNumber = dto.Phone.PhoneNumber,
        //    });
        //}
        // Return a customer entity.
        return new()
        {
            //CompanyCustomers = new List<CompanyCustomer>()
            //{
            //    new() { CompanyId = dto.CompanyId }
            //},
            Person = new()
            {
                //Addresses = addresses,
                //DocumentTypeId = dto.Person.DocumentTypeId,
                //Emails = emails,
                GenderId = dto.Person.GenderId,
                IdCard = dto.Person.IdCard,
                FirstName = dto.Person.FirstName,
                LastName = dto.Person.LastName,
                //Phones = phones,
                SocialReason = dto.Person.SocialReason,
                Birthdate = dto.Person.Birthdate,
                JuridicalPerson = dto.Person.JuridicalPerson,
            }
        };
    }

    public static Role RoleRequestDTOToRole(RoleRequestDTO dto)
    {
        List<Permission> permissions = new();
        foreach (short permissionId in dto.PermissionIds) permissions.Add(new() { Id = permissionId });
        return new()
        {
            CompanyId = dto.CompanyId,
            Name = dto.Name,
            Description = dto.Description,
            Permissions = permissions,
            Active = dto.Active,
        };
    }

    public static Customer CustomerDTOToCustomer(CustomerDTO dto)
    {
        return new()
        {
            Birthdate = dto.Birthdate,
            Code = dto.Code,
            CompanyId = dto.CompanyId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            JuridicalPerson = dto.JuridicalPerson,
            Person = new()
            {
                Birthdate = dto.Birthdate,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                IdCard = dto.IdCard,
                JuridicalPerson = dto.JuridicalPerson,
                PersonDocumentTypeId = dto.PersonDocumentTypeId,
                SocialReason = dto.SocialReason,
            },
            SocialReason = dto.SocialReason,
        };
    }
}
