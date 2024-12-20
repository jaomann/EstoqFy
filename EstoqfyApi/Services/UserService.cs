﻿using EstoqFy.Core.Contracts.Repository;
using EstoqFy.Core.Contracts.Services;
using EstoqFy.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace EstoqFy.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IBaseRepository<User> repository, IUserRepository userRepository) : base(repository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> ExistsAsync(string email, string cnpj) => _userRepository.ExistsAsync(email, cnpj);

        public Task<User> GetByEmailAsync(string email) => _userRepository.GetByEmailAsync(email);

        public Task<User> GetByIdAsync(Guid Id) => _userRepository.GetByIdAsync(Id);
    }
}
