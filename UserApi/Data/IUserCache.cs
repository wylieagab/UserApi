﻿using UserApi.Models.Entities;

namespace UserApi.Data
{
    public interface IUserCache
    {
        User Get(int id);
        void Remove(int id);
        void Set(User user);
    }
}
