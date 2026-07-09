
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DayFlow.Api.Controllers.Identity;

[ApiController]
[Route("api/users")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 取得所有使用者
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    /// <summary>
    /// 依 Id 取得使用者
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    /// <summary>
    /// 建立使用者
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreateUserRequest request)
    {
        var result = await _userService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Id },
            result);
    }

    /// <summary>
    /// 更新使用者
    /// </summary>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateUserRequest request)
    {
        var success = await _userService.UpdateAsync(id, request);

        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// 修改密碼
    /// </summary>
    [HttpPut("{id:guid}/password")]
    public async Task<IActionResult> ChangePassword(
        Guid id,
        ChangePasswordRequest request)
    {
        var result = await _userService.ChangePasswordAsync(id, request);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    /// <summary>
    /// 啟用帳號
    /// </summary>
    [HttpPatch("{id:guid}/activate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Activate(Guid id)
    {
        var success = await _userService.ActivateAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// 停用帳號
    /// </summary>
    [HttpPatch("{id:guid}/deactivate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        var success = await _userService.DeactivateAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// 刪除使用者
    /// </summary>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _userService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// 取得目前登入者資訊
    /// </summary>
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var user = await _userService.GetCurrentUserAsync(User);
        return Ok(user);
    }

    ///// <summary>
    ///// 更新自己的資料
    ///// </summary>
    //[HttpPut("me")]
    //public async Task<IActionResult> UpdateMe(UpdateProfileRequest request)
    //{
    //    var success = await _userService.UpdateProfileAsync(User, request);

    //    if (!success)
    //        return BadRequest();

    //    return NoContent();
    //}
}