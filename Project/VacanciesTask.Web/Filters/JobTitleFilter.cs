﻿using NewProject.Application;

namespace NewProject.API.Filters;

public class JobTitleFilter
{
    public int page { get; set; }
    public string? Name { get; set; } = String.Empty;
    public int Total { get; set; }
}