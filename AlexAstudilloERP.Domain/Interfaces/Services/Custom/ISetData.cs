﻿using AlexAstudilloERP.Domain.Entities.Public;
using System;

namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

public interface ISetData
{
    //public void SetAddressData(Address address, bool update = false)
    //{
    //    address.MainStreet = address.MainStreet.Trim().ToUpperInvariant();
    //    address.SecondaryStreet = address.SecondaryStreet?.Trim().ToUpperInvariant();
    //}

    public void SetCompanyData(Company company, bool update = false)
    {
        company.Tradename = company.Tradename.Trim().ToUpperInvariant();
        company.Description = company.Description?.Trim().ToUpperInvariant();
        if (company.Person != null) SetPersonData(company.Person, update);
    }

    public void SetCustomerData(Customer customer, bool update = false)
    {
        customer.Birthdate = customer.Birthdate?.ToUniversalTime();
        customer.FirstName = customer.JuridicalPerson ? null : customer.FirstName?.Trim().ToUpperInvariant();
        customer.LastName = customer.JuridicalPerson ? null : customer.LastName?.Trim().ToUpperInvariant();
        customer.SocialReason = customer.JuridicalPerson ? customer.SocialReason?.Trim().ToUpperInvariant() : null;
        if (customer.Person != null) SetPersonData(customer.Person, update);
    }

    public void SetEstablishmentData(Establishment establishment, bool update = false)
    {
        establishment.Name = establishment.Name.Trim().ToUpperInvariant();
        establishment.Description = establishment.Description?.Trim()?.ToUpperInvariant();
    }

    public void SetEmailData(Email email, bool update = false)
    {
        email.Mail = email.Mail.Trim();
        if (!update) email.Verified = false;
    }

    public void SetPersonData(Person person, bool update = false)
    {
        person.IdCard = person.IdCard.Trim().ToUpper();
        person.FirstName = person.JuridicalPerson ? null : person.FirstName?.Trim().ToUpperInvariant();
        person.LastName = person.JuridicalPerson ? null : person.LastName?.Trim().ToUpperInvariant();
        person.SocialReason = person.JuridicalPerson ? person.SocialReason?.Trim().ToUpperInvariant() : null;
    }

    public void SetRoleData(Role role, bool update = false)
    {
        role.Name = role.Name.Trim().ToUpperInvariant();
        role.Description = role.Description?.Trim().ToUpperInvariant();
    }

    public void SetUserData(User user, bool update = false)
    {
        //user.Username = user.Username.Trim();
        if (!update)
        {
            //user.Establishments = new List<Establishment>();
            //user.Roles = new List<Role>();
            //user.Password = BCrypt.BCrypt.HashPassword(user.Password.Trim(), BCrypt.BCrypt.GenSalt());
            //user.AccountNonExpired = true;
            //user.AccountNonLocked = true;
            //user.CredentialsNonExpired = true;
            //user.Enabled = true;
        }
    }
}
