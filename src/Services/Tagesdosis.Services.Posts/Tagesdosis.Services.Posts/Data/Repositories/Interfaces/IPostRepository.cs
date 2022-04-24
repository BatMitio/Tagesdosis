﻿using Tagesdosis.Services.Posts.Data.Entities;

namespace Tagesdosis.Services.Posts.Data.Repositories.Interfaces;

public interface IPostRepository
{
    public Task<int> SavePostAsync(Post post);
    Task DeletePostAsync(int requestId);
    Task<Post> FindByIdAsync(int id);
}