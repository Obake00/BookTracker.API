[HttpPost("login")]
public async Task<IActionResult> Login(LoginRequest request)
{
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    var user = await _context.Users
        .FirstOrDefaultAsync(u => u.Email == request.Email);

    if (user == null)
        return Unauthorized("Dados Invalidos");

    var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

    if (result == PasswordVerificationResult.Failed)
        return Unauthorized("Dados Invalidos");

    var token = GenerateJwtToken(user);

    return Ok(new { token });
}