using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileUploadServiceForWWWRoot _fileUploadServiceForWWWRoot;

        public CustomerService(ICustomerRepository customerRepository, IRoleRepository roleRepository, IUserRepository userRepository, IFileUploadServiceForWWWRoot fileUploadServiceForWWWRoot)
        {
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _fileUploadServiceForWWWRoot = fileUploadServiceForWWWRoot;
        }

        public async Task<BaseResponse<CustomerDto>> CreateAsync(CreateCustomerRequestModel model)
        {
            var customerExist = await _customerRepository.GetAsync(c => c.User.Email == model.Email);
            if (customerExist != null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = $"Email '{customerExist.User.Email}' already exists",
                    Status = false,
                };
            }
            var phoneNumer = await _customerRepository.GetAsync(c => c.User.PhoneNumber == model.PhoneNumber);
            if (phoneNumer != null)
            {
                return new BaseResponse<CustomerDto>
                {
                    Message = $"phone Numer '{phoneNumer.User.PhoneNumber}' already exists",
                    Status = false,
                };
            }

            var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);

            var role = await _roleRepository.GetAsync(a => a.RoleName.ToLower() == "customer".ToLower());

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                // ConfirmPassword = BCrypt.Net.BCrypt.HashPassword(model.ConfirmPassword),
                Address = model.Address,
                Email = model.Email,
                ProfilePicture = profilePicture,
                Gender = model.Gender,
                Role = role,
                RoleId = role.Id,
                //CreatedBy = loginId,
            };

            await _userRepository.CreateAsync(user);

            var customer = new Customer
            {
                //CreatedBy = loginId,
                RegistrationNumber = await GeneratecustomerRegNumAsync(),
                UserId = user.Id,
                User = user,
            };

            await _customerRepository.CreateAsync(customer);
            await _customerRepository.SaveAsync();

            var userFirstLetterOfFirstNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.FirstName)}";
            var userFirstLetterOfLastNameToUpperCase = $"{CultureInfo.CurrentCulture.TextInfo.ToTitleCase(user.LastName)}";
            var fullName = userFirstLetterOfFirstNameToUpperCase + " " + userFirstLetterOfLastNameToUpperCase;

            return new BaseResponse<CustomerDto>
            {
                Message = $"Dear {fullName}, you have been successfully registered as a Customer",
                Status = true,
                Data = null,

                // Data = new CustomerDto
                // {
                //     Id = customer.Id,
                //     RegistrationNumber = customer.RegistrationNumber,
                //     FirstName = customer.User.FirstName,
                //     LastName = customer.User.LastName,
                //     Email = customer.User.Email,
                //     PhoneNumber = customer.User.PhoneNumber,
                //     ProfilePicture = customer.User.ProfilePicture,
                // }
            };
        }



        public async Task<BaseResponse<CustomerDto>> DeleteAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(d => d.Id == id);
            if (customer != null)
            {
                customer.IsDeleted = true;
                customer.User.IsDeleted = true;
                _customerRepository.Update(customer);
                await _customerRepository.SaveAsync();

                return new BaseResponse<CustomerDto>
                {
                    Message = "Successful",
                    Status = true
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "customer does not exist",
                Status = false
            };
        }



        public async Task<BaseResponse<CustomerDto>> GetAsync(Guid id)
        {
            var customer = await _customerRepository.GetAsync(g => g.Id == id || g.UserId == id);
            if (customer != null)
            {

                return new BaseResponse<CustomerDto>
                {
                    Message = "successful",
                    Status = true,
                    Data = new CustomerDto
                    {
                        Id = customer.Id,
                        RegistrationNumber = customer.RegistrationNumber,
                        FirstName = customer.User.FirstName,
                        LastName = customer.User.LastName,
                        Email = customer.User.Email,
                        PhoneNumber = customer.User.PhoneNumber,
                        ProfilePicture = customer.User.ProfilePicture,
                        Address = customer.User.Address
                    },
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "customer is not found",
                Status = false
            };
        }

        public async Task<BaseResponse<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            if (!customers.Any())
            {
                return new BaseResponse<IEnumerable<CustomerDto>>
                {
                    Message = "No customer found",
                    Status = false
                };
            }

            return new BaseResponse<IEnumerable<CustomerDto>>
            {
                Message = "Successful",
                Status = true,
                Data = customers.Select(m => new CustomerDto
                {
                    Id = m.Id,
                    RegistrationNumber = m.RegistrationNumber,
                    FirstName = m.User.FirstName,
                    LastName = m.User.LastName,
                    Email = m.User.Email,
                    PhoneNumber = m.User.PhoneNumber,
                    Address = m.User.Address,
                    // ProfilePicture = m.User.ProfilePicture,
                })
            };
        }



        public async Task<BaseResponse<CustomerDto>> UpdateAsync(Guid id, UpdateCustomerRequestModel model)
        {
            var customer = await _customerRepository.GetAsync(a => a.Id == id);
            if (customer is not null)
            {
                if (model.ProfilePicture != null)
                {
                    var profilePicture = await _fileUploadServiceForWWWRoot.UploadFileAsync(model.ProfilePicture);
                    customer.User.ProfilePicture = profilePicture;
                }

                customer.User.Address = model.Address;
                customer.User.PhoneNumber = model.PhoneNumber;
                customer.User.Password = model.Password;


                _customerRepository.Update(customer);
                await _customerRepository.SaveAsync();

                return new BaseResponse<CustomerDto>
                {
                    Message = "Profile Updated Successfully",
                    Status = true,
                    Data = new CustomerDto
                    {
                        ProfilePicture = customer.User.ProfilePicture,
                        PhoneNumber = customer.User.PhoneNumber,
                        Address = customer.User.Address,
                        Password = customer.User.Password,
                    }
                };
            }
            return new BaseResponse<CustomerDto>
            {
                Message = "Unable to Update",
                Status = false,
            };
        }




        private async Task<string> GeneratecustomerRegNumAsync()
        {
            var count = (await _customerRepository.GetAllAsync()).Count();
            return "EP/CUS/" + $"{count + 1}";
        }


    }
}