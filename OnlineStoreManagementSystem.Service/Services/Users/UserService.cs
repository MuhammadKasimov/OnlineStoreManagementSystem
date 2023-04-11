using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineStoreManagementSystem.Data.IRepositories;
using OnlineStoreManagementSystem.Domain.Entitties.Carts;
using OnlineStoreManagementSystem.Domain.Entitties.Users;
using OnlineStoreManagementSystem.Domain.Enums;
using OnlineStoreManagementSystem.Service.DTOs.Users;
using OnlineStoreManagementSystem.Service.Exceptions;
using OnlineStoreManagementSystem.Service.Extensions;
using OnlineStoreManagementSystem.Service.Helpers;
using OnlineStoreManagementSystem.Service.Interfaces.Attachments;
using OnlineStoreManagementSystem.Service.Interfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineStoreManagementSystem.Service.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> userRepository;
        private readonly IAttachmentService attachmentService;
        private readonly IMapper mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper, IAttachmentService attachmentService)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.attachmentService = attachmentService;
        }

        public async ValueTask<bool> ChangePasswordAsync(UserForChangePasswordDTO dto)
        {
            var user = await userRepository.GetAsync(u => u.Id == HttpContextHelper.UserId);

            if (user == null)
                throw new HttpStatusCodeException(404, "User not found");

            if (user.Password != dto.OldPassword.Encrypt())
                throw new HttpStatusCodeException(400, "Password is incorrect");

            user.Password = dto.NewPassword.Encrypt();

            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<bool> ChangeRoleAsync(long userId, byte roleId)
        {
            var account = await userRepository.GetAsync(u => u.Id == userId
                                    && u.Role != UserRole.Admin);

            if (account is null)
                throw new HttpStatusCodeException(404, "User not found");

            if (roleId < 0 && roleId > 4)
                throw new HttpStatusCodeException(404, "Such role does not exist");

            account.Role = (UserRole)roleId;
            account.UpdatedAt = DateTime.UtcNow;

            await userRepository.SaveChangesAsync();

            return true;
        }

        public async ValueTask<UserForViewDTO> CreateAsync(UserForCreationDTO dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new HttpStatusCodeException(404, "Passwords are different");

            var existEmail = await userRepository.GetAsync(u => u.Username == dto.Username);

            if (existEmail != null)
                throw new HttpStatusCodeException(400, "This email is already taken");

            var createdUser = await userRepository.CreateAsync(mapper.Map<User>(dto));

            if (dto.FormFile != null)
            {
                var createdAttachment = await attachmentService.UploadAsync(dto.FormFile.ToAttachmentOrDefault());
                createdUser.AttachmentId = createdAttachment.Id;
            }

            createdUser.Password = createdUser.Password.Encrypt();
            createdUser.Cart = new Cart();
            await userRepository.SaveChangesAsync();

            return mapper.Map<UserForViewDTO>(createdUser);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            var isDeleted = await userRepository.DeleteAsync(id);

            if (!isDeleted)
                throw new HttpStatusCodeException(404, "User not found");

            await userRepository.SaveChangesAsync();
            return true;
        }

        public async ValueTask<IEnumerable<UserForViewDTO>> GetAllAsync(Expression<Func<User, bool>> expression = null)
        {
            var users = userRepository.GetAll(expression: expression, isTracking: false);

            return mapper.Map<List<UserForViewDTO>>(await users.ToListAsync());
        }

        public async ValueTask<UserForViewDTO> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await userRepository.GetAsync(expression);

            if (user is null)
                throw new HttpStatusCodeException(404, "User not found");

            return mapper.Map<UserForViewDTO>(user);
        }

        public async Task<UserForViewDTO> GetUserInfoAsync()
            => await GetAsync(u => u.Id == HttpContextHelper.UserId);

        public async ValueTask<UserForViewDTO> UpdateAsync(long id, UserForUpdateDTO dto)
        {
            var existUser = await userRepository.GetAsync(
                u => u.Id == id);

            if (existUser == null)
                throw new HttpStatusCodeException(404, "User not found");

            var alreadyExistUser = await userRepository.GetAsync(
                u => u.Username == dto.Username && u.Id != HttpContextHelper.UserId);

            if (alreadyExistUser != null)
                throw new HttpStatusCodeException(400, "User with such email already exists");


            existUser.UpdatedAt = DateTime.UtcNow;

            if (dto.FormFile != null)
            {
                if (existUser.AttachmentId != null)
                    await attachmentService.UpdateAsync((long)existUser.AttachmentId, dto.FormFile.ToAttachmentOrDefault().Stream);
                else
                    await attachmentService.UploadAsync(dto.FormFile.ToAttachmentOrDefault());
            }

            existUser = userRepository.Update(mapper.Map(dto, existUser));
            await userRepository.SaveChangesAsync();

            return mapper.Map<UserForViewDTO>(existUser);
        }
    }
}
