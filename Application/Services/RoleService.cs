using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Abstractions.RepositoryInterfaces;
using Application.Abstractions.ServiceInterfaces;
using Application.Dtos;
using Domain.Entity;
using Microsoft.AspNetCore.Http;

namespace Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IHttpContextAccessor _httpAccessor;

        public RoleService(IRoleRepository roleRepository, IHttpContextAccessor httpAccessor)
        {
            _roleRepository = roleRepository;
            _httpAccessor = httpAccessor;
        }

        public async Task<BaseResponse<RoleDto>> CreateAsync(CreateRoleRequestModel model)
        {
            var loginId = _httpAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var roleExist = _roleRepository.GetAsync(a => a.RoleName == model.RoleName);
            if (roleExist == null)
            {
                Role role = new();
                role.RoleName = model.RoleName;
                role.RoleDescription = model.RoleDescription;
                role.CreatedBy = loginId;

                await _roleRepository.CreateAsync(role);
                await _roleRepository.SaveAsync();

                return new BaseResponse<RoleDto>
                {
                    Message = "Role Successfully Created",
                    Status = true,
                    Data = new RoleDto
                    {
                        Id = role.Id,
                        RoleName = role.RoleName,
                        RoleDescription = role.RoleDescription
                    }
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "Role Already Exists!",
                Status = false
            };

        }



        public async Task<BaseResponse<RoleDto>> DeleteAsync(Guid id)
        {
            var role = await _roleRepository.GetAsync(id);
            if (role is null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "The role does not exist",
                    Status = false
                };
            }
            role.IsDeleted = true;

            _roleRepository.Update(role);
            await _roleRepository.SaveAsync();
            return new BaseResponse<RoleDto>
            {
                Message = "Role Deleted Successfully",
                Status = true
            };

        }


        public async Task<BaseResponse<RoleDto>> GetAsync(Guid id)
        {
            var role = await _roleRepository.GetAsync(id);
            if (role == null)
            {
                return new BaseResponse<RoleDto>
                {
                    Message = "Role is Not found",
                    Status = false,
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "Found",
                Status = true,
                Data = new RoleDto
                {
                    Id = role.Id,
                    RoleName = role.RoleName,
                    RoleDescription = role.RoleDescription
                }
            };
        }



        public async Task<BaseResponse<IEnumerable<RoleDto>>> GetAllAsync()
        {
            var role = await _roleRepository.GetAllAsync();
            if (role.Count() == 0)
            {
                return new BaseResponse<IEnumerable<RoleDto>>
                {
                    Message = "Not found",
                    Status = false,
                };
            }
            return new BaseResponse<IEnumerable<RoleDto>>
            {
                Message = "Found",
                Status = true,
                Data = role.Select(c => new RoleDto
                {
                    Id = c.Id,
                    RoleName = c.RoleName,
                    RoleDescription = c.RoleDescription
                })
            };
        }



        public async Task<BaseResponse<RoleDto>> UpdateAsync(Guid id, UpdateRoleRequestModel model)
        {
            var role = await _roleRepository.GetAsync(a => a.Id == id);
            if (role is not null)
            {

                role.RoleName = model.RoleName;
                role.RoleDescription = model.RoleDescription;

                _roleRepository.Update(role);
                await _roleRepository.SaveAsync();

                return new BaseResponse<RoleDto>
                {
                    Message = "Role Updated Successfully",
                    Status = true,
                    Data = new RoleDto
                    {
                        RoleName = role.RoleName,
                        RoleDescription = role.RoleDescription,
                        Id = role.Id,
                    }
                };
            }
            return new BaseResponse<RoleDto>
            {
                Message = "Unable to Update!",
                Status = false,
            };
        }


    }
}