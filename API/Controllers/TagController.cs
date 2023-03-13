﻿using Core.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }
    
    [HttpGet]
    [Route("GetAllTags")]
    public string GetAllTags()
    {
        return "This will be a list of tags";
    }

    [HttpGet]
    [Route("CheckIfExistsATag")]
    public async Task<bool> CheckIfExistTags()
    {
        return await _tagService.CheckIfExistTags();
    }

    [HttpGet]
    [Route("TestAuth")]
    [Authorize]
    public string TestAuth()
    {
        return "You should see this only if you are authorized";
    }

}