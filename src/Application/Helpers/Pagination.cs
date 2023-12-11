﻿using Domain.Entities;

namespace Application.Helpers;

public class Pagination<T> where T : BaseEntity
{
    public Pagination(int pageIndex, int pageSize, int count, IReadOnlyCollection<T> data)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        Count = count;
        Data = data;
    }

    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Count { get; set; }
    public IReadOnlyCollection<T> Data { get; set; }
}