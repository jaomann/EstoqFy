﻿using EstoqFy.Core.Contracts.Repository;
using EstoqFy.Core.Contracts.Services;
using EstoqFy.Core.Entities;

namespace EstoqFy.Services
{
    public class ItemService : BaseService<Item>, IItemService
    {
        private readonly IItemRepository _repository;
        public ItemService(IBaseRepository<Item> repository, IItemRepository itemRepository) : base(repository)
        {
            _repository = itemRepository;
        }

        public int Count(string EAN) => _repository.Count(EAN);
        
    }
}