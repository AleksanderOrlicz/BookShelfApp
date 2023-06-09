﻿using BookShelfApp.Entities;

namespace BookShelfApp.Repositories
{
    public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : class, IEntity
    {        
    }
}
